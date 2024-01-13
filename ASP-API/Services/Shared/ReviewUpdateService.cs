using ASP_API.Model.Public;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_API.Services.Shared
{
    public class ReviewUpdateService : IReviewUpdateService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ReviewUpdateService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReviewUpdates>> GetReviewDetailsList()
        {
            try
            {
                return await _context.ReviewUpdates.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<ReviewUpdates> GetReviewDetailsById(int id)
        {
            try
            {
                var reviewdetails = await _context.ReviewUpdates.FirstOrDefaultAsync(x => x.Id == id);
                if (reviewdetails == null)
                {
                    throw new NullReferenceException("Review not found");
                }
                return reviewdetails;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching review", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching review", ex);
            }
        }
        public async Task<ReviewUpdates> AddReviewDetails(ReviewUpdates reviewUpdates)
        {
            try
            {
                var domainMatch =
                    (from r in _context.Reviewer.Where(x => x.Id == reviewUpdates.ReviewerId)
                    join a in _context.Advisors.Where(x => x.Id == reviewUpdates.AdvisorId)
                    on r.DomainId equals a.DomainId
                    join s in _context.Students.Where(x => x.Id == reviewUpdates.StudentId)
                    on a.DomainId equals s.DomainId
                    select s.DomainId).FirstOrDefault();

                if (domainMatch == 0) { throw new NullReferenceException(); }

                var previousReview =
                    (from w in _context.ReviewUpdates
                    orderby w.WeekNo
                    select new { w.WeekNo, w.ReviewStatus }).ToList().LastOrDefault();
                                              
                if (previousReview.ReviewStatus == ReviewStatus.PASSED)
                    reviewUpdates.WeekNo = previousReview.WeekNo + 1;
                else
                    reviewUpdates.WeekNo = previousReview.WeekNo;

                reviewUpdates.PostponedDate = reviewUpdates.ReviewDate;
                reviewUpdates.ReviewStatus = ReviewStatus.PENDING;
                reviewUpdates.PostponeStatus = PostponeStatus.PENDING;
                var result = _context.ReviewUpdates.Add(reviewUpdates);
                
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return reviewUpdates;
        }
        public async Task<ReviewUpdates> UpdateReviewDetails(ReviewUpdates reviewUpdates)
        {
            try
            {
                var result = _context.ReviewUpdates.FirstOrDefault(x => x.Id == reviewUpdates.Id);

                if(result == null) { throw new NullReferenceException(); }

                var domainMatch =
                    (from r in _context.Reviewer.Where(x => x.Id == reviewUpdates.ReviewerId)
                     join a in _context.Advisors.Where(x => x.Id == reviewUpdates.AdvisorId)
                     on r.DomainId equals a.DomainId
                     join s in _context.Students.Where(x => x.Id == reviewUpdates.StudentId)
                     on a.DomainId equals s.DomainId
                     select s.DomainId).FirstOrDefault();

                if (domainMatch == 0) { throw new NullReferenceException(); }
                
                var reviewSummaryStatus =
                    (from st in _context.ReviewSummary
                    .Where(x => x.ReviewId == reviewUpdates.Id)
                    select st.Status).FirstOrDefault();

                if(reviewSummaryStatus == null) 
                    { reviewUpdates.ReviewStatus = ReviewStatus.PENDING; }
                else if(reviewSummaryStatus.Equals(SummaryStatus.VERYGOOD) || reviewSummaryStatus.Equals(SummaryStatus.GOOD))
                    { reviewUpdates.ReviewStatus = ReviewStatus.PASSED; }
                else if(reviewSummaryStatus.Equals(SummaryStatus.AVERAGE))
                    { reviewUpdates.ReviewStatus = ReviewStatus.WEEKREPEAT; }
                else if (reviewSummaryStatus.Equals(SummaryStatus.BELOWAVERAGE))
                    { reviewUpdates.ReviewStatus = ReviewStatus.WEEKBACK; }
                else
                    reviewUpdates.ReviewStatus = ReviewStatus.PENDING;
               
                _mapper.Map(reviewUpdates, result);      
                
                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteReviewDetails(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var reviewIdExist = _context.ReviewUpdates.FirstOrDefault(x => x.Id == id);
                if (reviewIdExist == null) { throw new Exception($"Review Id {id} not found."); }
                {
                    var result = _context.ReviewUpdates.Remove(reviewIdExist);
                    await _context.SaveChangesAsync();
                    return result != null ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        // Review summary crud
        public async Task<IEnumerable<ReviewSummary>> GetReviewSummaryList()
        {
            try
            {
                return await _context.ReviewSummary.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        public async Task<ReviewSummary> GetReviewSummaryById(int id)
        {
            try
            {
                var reviewsummary = await _context.ReviewSummary.FirstOrDefaultAsync(x => x.Id == id);
                if (reviewsummary == null)
                {
                    throw new NullReferenceException("Review summary not found");
                }
                return reviewsummary;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching review summary", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching review summary", ex);
            }
        }

        public async Task<ReviewSummary> AddReviewSummary(ReviewSummary reviewSummary)
        {
            try
            {
                var presentReview =
                    from r in _context.ReviewUpdates
                    where r.ReviewStatus == ReviewStatus.PENDING
                    select r;

                var matchingReview = presentReview.FirstOrDefault(x => x.Id == reviewSummary.ReviewId);
                if (matchingReview != null)
                {
                    if (reviewSummary.Marks < 50)
                    {
                        reviewSummary.Status = SummaryStatus.BELOWAVERAGE;
                    }
                    else if (reviewSummary.Marks >= 50 && reviewSummary.Marks < 70)
                    {
                        reviewSummary.Status = SummaryStatus.AVERAGE;
                    }
                    else if (reviewSummary.Marks >= 70 && reviewSummary.Marks < 90)
                    {
                        reviewSummary.Status = SummaryStatus.GOOD;
                    }
                    else
                    {
                        reviewSummary.Status = SummaryStatus.VERYGOOD;
                    }

                    if(matchingReview.PostponeStatus == PostponeStatus.PENDING || 
                        matchingReview.PostponeStatus == PostponeStatus.REJECTED)
                    {
                        reviewSummary.CreatedAt = matchingReview.ReviewDate;
                    } else
                    {
                        reviewSummary.CreatedAt = matchingReview.PostponedDate;
                    }
                }

                var result = _context.ReviewSummary.Update(reviewSummary);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return reviewSummary;
        }

        public async Task<ReviewSummary> UpdateReviewSummary(ReviewSummary reviewSummary)
        {
            try
            {
                var result = _context.ReviewSummary.FirstOrDefault(x => x.Id == reviewSummary.Id);
                if (result == null) { throw new NullReferenceException(); }

                var presentReview =
                   from r in _context.ReviewUpdates
                   where r.ReviewStatus == ReviewStatus.PENDING
                   select r;

                var matchingReview = presentReview.FirstOrDefault(x => x.Id == reviewSummary.ReviewId);
                if (matchingReview != null)
                {
                    if (reviewSummary.Marks < 50)
                    {
                        reviewSummary.Status = SummaryStatus.BELOWAVERAGE;
                    }
                    else if (reviewSummary.Marks >= 50 && reviewSummary.Marks < 70)
                    {
                        reviewSummary.Status = SummaryStatus.AVERAGE;
                    }
                    else if (reviewSummary.Marks >= 70 && reviewSummary.Marks < 90)
                    {
                        reviewSummary.Status = SummaryStatus.GOOD;
                    }
                    else
                    {
                        reviewSummary.Status = SummaryStatus.VERYGOOD;
                    }

                    if (matchingReview.PostponeStatus == PostponeStatus.PENDING ||
                        matchingReview.PostponeStatus == PostponeStatus.REJECTED)
                    {
                        reviewSummary.CreatedAt = matchingReview.ReviewDate;
                    }
                    else
                    {
                        reviewSummary.CreatedAt = matchingReview.PostponedDate;
                    }
                }
               
                _mapper.Map(reviewSummary, result);
                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }

        public async Task<bool> DeleteReviewSummary(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var summaryIdExist = _context.Batches.FirstOrDefault(x => x.BatchId == id);
                if (summaryIdExist == null) { throw new Exception($"Review summary Id {id} not found."); }
                {
                    var result = _context.Batches.Remove(summaryIdExist);
                    await _context.SaveChangesAsync();
                    return result != null ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
