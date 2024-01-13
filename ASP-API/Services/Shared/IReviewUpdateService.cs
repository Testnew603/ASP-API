using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IReviewUpdateService
    {
        public Task<IEnumerable<ReviewUpdates>> GetReviewDetailsList();
        public Task<ReviewUpdates> GetReviewDetailsById(int id);
        public Task<ReviewUpdates> AddReviewDetails(ReviewUpdates reviewUpdates);
        public Task<ReviewUpdates> UpdateReviewDetails(ReviewUpdates reviewUpdates);
        public Task<bool> DeleteReviewDetails(int? id);


        // review summary crud
        public Task<IEnumerable<ReviewSummary>> GetReviewSummaryList();
        public Task<ReviewSummary> GetReviewSummaryById(int id);
        public Task<ReviewSummary> AddReviewSummary(ReviewSummary reviewSummary);
        public Task<ReviewSummary> UpdateReviewSummary(ReviewSummary reviewSummary);
        public Task<bool> DeleteReviewSummary(int? id);
    }
}