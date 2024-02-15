using ASP_API.Model.Public;
using ASP_API.Model.Student;

namespace ASP_API.Model.Staff
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public int DomainId { get; set; }
        public StaffStatus Status { get; set; } = StaffStatus.PENDING;
        public Domain Domain { get; set; }
    }
}
