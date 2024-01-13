using ASP_API.DTO;
using ASP_API.Model.Public;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Dynamic.Core;
using System.Security.AccessControl;

namespace ASP_API.Services.Shared
{
    public class FeeService : IFeeService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly ICondonationFeeService _condonationFee;

        public FeeService(Context context, IMapper mapper, ICondonationFeeService condonationFee)
        {
            _context = context;
            _mapper = mapper;
            _condonationFee = condonationFee;

        }

        public async Task<IEnumerable<Fees>> GetFeeList()
        {
            return await _context.Fees.ToListAsync();
        }
        public async Task<Fees> GetFeeById(int? id)
        {
            return await _context.Fees.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Fees> AddFee(Fees fees)
        {
            try
            {
                int totalfine = 0;

                var oldBalance = _context.Fees.Where(fee => fee.StudentId == fees.StudentId)
                    .Sum(fee => fee.Balance);

                var oldFeeStatus = _context.Fees
                   .Where(x => x.StudentId == fees.StudentId).ToList().LastOrDefault();

                var studentAdvisor = _context.AllocStudentToAdvisor
                    .Where(x => x.StudentId == fees.StudentId).Select(x => x.AdvisorId).FirstOrDefault();

                var minAmount = fees.SpaceRent - 500;
              
                //apply late fee penalty based on the last date of payment and min rent amount
                if (fees.CreatedAt > fees.LastDate && fees.PaidAmount > minAmount)
                {                    
                    var penaltyCount = fees.CreatedAt.Day - fees.LastDate.Day;

                    for (int i = 0; i < penaltyCount; i++)
                    {
                        totalfine = totalfine + Convert.ToInt32(Math.Round(fees.SpaceRent * 1.5 / 100));
                    }
                    var student = _context.Students.FirstOrDefault(x => x.Id == fees.StudentId);
                    if (student != null)
                    {
                        var penalty = new CondonationFees
                        {
                            StudentId = student.Id,
                            FineType = FineType.PENALTY,
                            FineAmount = totalfine,
                            Status = FineStatus.NOTPAID,
                            CreatedAt = fees.CreatedAt,
                            Reason = $"late by {penaltyCount} days",
                        };
                        await _condonationFee.AddPenalty(penalty);
                    }
                }

                var newFee = _mapper.Map<Fees>(fees);
               
                    newFee.Month = fees.Month.ToUpper();                                                       
                    newFee.AdvisorId = studentAdvisor;
                    newFee.Balance = newFee.SpaceRent - newFee.PaidAmount;

                    if (newFee.Balance == 0)
                        newFee.status = FeeStatus.PAID;
                    else
                        newFee.status = FeeStatus.BALANCE;
                                
                _context.Fees.Add(newFee);
                await _context.SaveChangesAsync();
                return fees;
            } catch (Exception ex)
            {
                throw new Exception("error occured" + ex);
            }
        }
        public async Task<Fees> UpdateFee(Fees fees)
        {
            try
            {
                var feeExist = _context.Fees.FirstOrDefault(x => x.Id == fees.Id);
                if (feeExist == null) { throw new Exception(); }

                var studentAdvisor = await _context.AllocStudentToAdvisor
                    .FirstOrDefaultAsync(x => x.StudentId == fees.StudentId);

                var feeDetails = _context.Fees.Where(x => x.Id == fees.Id).FirstOrDefault();

                var updatedFee = new Fees
                {
                    StudentId = fees.StudentId,
                    Month = fees.Month,
                    SpaceRent = fees.SpaceRent,
                    PaidAmount = fees.PaidAmount,
                    PaidThrough = fees.PaidThrough,
                    CreatedAt = fees.CreatedAt,
                    LastDate = fees.LastDate,                                       
                };
                    updatedFee.AdvisorId = studentAdvisor.AdvisorId;
                    updatedFee.Balance = updatedFee.SpaceRent - updatedFee.PaidAmount;            

                if(updatedFee.Balance ==  0)
                    updatedFee.status = FeeStatus.PAID;
                else
                    updatedFee.status = FeeStatus.BALANCE;

                _context.Fees.Update(updatedFee);
                await _context.SaveChangesAsync();
                return fees;

            } catch (Exception ex)
            {
                throw new Exception("invalid credentials" + ex);
            }
        }

        public async Task<bool> DeleteFee(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var feeIdExist = _context.Fees.FirstOrDefault(x => x.Id == id);
                if (feeIdExist == null) { throw new Exception($"Fee Id {id} not found."); }
                var result = _context.Remove(feeIdExist);
                await _context.SaveChangesAsync();
                return result != null ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<FeeDTO> BalanceFeePayment(FeeDTO feeDTO)
        {
            try
            {
                var studentAdvisor = _context.AllocStudentToAdvisor
                    .Where(x => x.StudentId == feeDTO.StudentId).Select(x => x.AdvisorId).FirstOrDefault();

                var monthExist = _context.Fees
                    .Where(x => x.StudentId == feeDTO.StudentId)
                    .Where(x => x.Month == feeDTO.Month).ToList().LastOrDefault();
                if (monthExist != null && monthExist.Balance != 0)
                {
                    Fees balanceUpdate = new Fees
                    {
                        StudentId = feeDTO.StudentId,
                        AdvisorId = studentAdvisor,
                        Month = feeDTO.Month,
                        SpaceRent = 0,
                        PaidAmount = feeDTO.PaidAmount,
                        PaidThrough = feeDTO.PaidThrough,
                        LastDate = monthExist.LastDate,
                        CreatedAt = feeDTO.CreatedAt,
                        Balance = feeDTO.PaidAmount - monthExist.Balance,
                    };
                    balanceUpdate.status = balanceUpdate.Balance == 0 ? FeeStatus.PAID : FeeStatus.BALANCE;

                    _context.Fees.Add(balanceUpdate);
                    await _context.SaveChangesAsync();
                    return feeDTO;
                } else { throw new Exception(); }
            } catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
