using ASP_API.DTO;
using ASP_API.Model.Public;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net.NetworkInformation;

namespace ASP_API.Services.Shared
{
    public class StudentAttendanceService : IStudentAttendanceService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public StudentAttendanceService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StudentAttendance>> GetStudentAttendanceList()
        {
            try
            {
                return await _context.StudentAttendanceReport.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<StudentAttendance> GetStudentAttendance(int id)
        {
            try
            {
                var attendance = await _context.StudentAttendanceReport.FirstOrDefaultAsync(x => x.Id == id);
                if (attendance == null)
                {
                    throw new NullReferenceException("Attendance not found");
                }
                return attendance;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching attendance", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching attendance", ex);
            }
        }
        public async Task<StudAttendanceEntryDTO> AddStudentAttendanceEntry(StudAttendanceEntryDTO attendanceEntryDTO)
        {
            var searchResultIpAddress = GetLocalIpAddress();

            string[] allowedIpAddress =
            {
                "192.168.0.122",
                "192.168.1.71",
                "192.168.1.102",
                "192.168.0.126",
            };
            List<string> ipAddressRange = new List<string>(allowedIpAddress);

            try
            {
                if (searchResultIpAddress != null)
                {
                    if(!ipAddressRange.Contains(searchResultIpAddress.Result))
                    {
                        throw new Exception("IP address not matching");
                    }                
                }

                var matchingWithAdvisorStud = (from alloc in _context.AllocStudentToAdvisor
                                               where alloc.StudentId == attendanceEntryDTO.StudentId                                              
                                               select new
                                               {
                                                  alloc.AdvisorId,
                                               }).SingleOrDefault();
           
                if (matchingWithAdvisorStud == null)
                    throw new NullReferenceException();

                DateTime officeEntryTime =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 0);
                DateTime officeExitTime =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 30, 0);

                if(attendanceEntryDTO.EntryTime <= DateTime.Now)
                    attendanceEntryDTO.EntryTime = DateTime.Now;

                if (attendanceEntryDTO.EntryTime > officeEntryTime)
                {
                    attendanceEntryDTO.Status = AttendanceStatus.PENDING;
                    attendanceEntryDTO.LateReason = attendanceEntryDTO.LateReason + " " +
                        $"Late by {attendanceEntryDTO.EntryTime - officeEntryTime} mins/hrs";
                } else
                  {
                    attendanceEntryDTO.Status = AttendanceStatus.PENDING;
                    attendanceEntryDTO.LateReason = attendanceEntryDTO.LateReason;
                  }

            var mapResult = _mapper.Map<StudentAttendance>(attendanceEntryDTO);
                mapResult.AdvisorId = matchingWithAdvisorStud.AdvisorId;
                mapResult.ExitTime = officeExitTime;       
                mapResult.CreatedAt = DateTime.Parse($"{DateTime.Now:g}");

                _context.StudentAttendanceReport.Add(mapResult);                
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return attendanceEntryDTO;
        }

        public async Task<StudAttendanceExitDTO> AddStudentAttendanceExit(StudAttendanceExitDTO attendanceExitDTO)
        {
            var searchResultIpAddress = GetLocalIpAddress();

            string[] allowedIpAddress =
            {
                "192.168.0.122",
                "192.168.1.71",
                "192.168.1.102",
                "192.168.0.126",
            };
            List<string> ipAddressRange = new List<string>(allowedIpAddress);

            try
            {
                if (searchResultIpAddress != null)
                {
                    if (!ipAddressRange.Contains(searchResultIpAddress.Result))
                    {
                        throw new Exception("IP address not matching");
                    }
                }

                var createdAt = DateTime.Parse($"{DateTime.Now:d}");
                var attendance = (from entry in _context.StudentAttendanceReport
                                  where entry.StudentId == attendanceExitDTO.StudentId &&
                                  entry.CreatedAt == createdAt && entry.Status == AttendanceStatus.PENDING
                                  select new
                                  {
                                      entry.Id,                                    
                                  }).SingleOrDefault();
                var attendanceExit = _context.StudentAttendanceReport.FirstOrDefault(
                    exit => exit.Id == attendance.Id
                    );

                if (attendance == null)
                {
                    throw new Exception("Invalid entry");
                }
                
                DateTime officeExitTime =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0);
                if (attendanceExitDTO.ExitTime  >= DateTime.Now)
                    attendanceExitDTO.ExitTime = DateTime.Now;

                if (attendanceExitDTO.ExitTime < officeExitTime)
                    attendanceExitDTO.LeavingReason = attendanceExitDTO.LeavingReason;
                else
                    attendanceExitDTO.LeavingReason = "Invalid Reason!";
           
                attendanceExit.ExitTime = attendanceExitDTO.ExitTime;
                attendanceExit.LeavingReason = attendanceExitDTO.LeavingReason;
                attendanceExit.Status = AttendanceStatus.PRESENT;
                               
                _context.StudentAttendanceReport.Update(attendanceExit);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return attendanceExitDTO;
        }
        public async Task<StudentAttendance> UpdateStudentAttendance(StudentAttendance studentAttendance)
        {
            try
            {
                var result = _context.StudentAttendanceReport.FirstOrDefault(x => x.Id == studentAttendance.Id);                
                if (result == null) { throw new NullReferenceException(); }

                _mapper.Map(studentAttendance, result);

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteStudentAttendance(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var attendanceIdExist = _context.StudentAttendanceReport.FirstOrDefault(x => x.Id == id);
                if (attendanceIdExist == null) { throw new Exception($"Attendance Id {id} not found."); }
                {
                    var result = _context.StudentAttendanceReport.Remove(attendanceIdExist);
                    await _context.SaveChangesAsync();
                    return result != null ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }



        public async Task<string> GetLocalIpAddress()
        {
            await Task.Delay(0); // Placeholder for asynchronous operation
            return GetLocalIpAddress1();
        }

        private static string GetLocalIpAddress1()
        {
            string localIpAddress = string.Empty;

            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            localIpAddress = ip.Address.ToString();
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(localIpAddress))
                    {
                        break;
                    }
                }
            }
            return localIpAddress;
        }
    }
}
