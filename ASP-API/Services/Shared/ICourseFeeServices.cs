using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface ICourseFeeServices
    {
        public Task<IEnumerable<CourseFee>> GetCourseFeeList();
        public Task<CourseFee> GetCourseFeeById(int? id);
        public Task<CourseFee> AddCourseFee(CourseFee courseFee);
        public Task<CourseFee> UpdateCourseFee(CourseFee courseFee);
        public Task<bool> DeleteCourseFee(int? id);
    }
}