using ASP_API.Model.Public;
using ASP_API.Model.Student;

namespace ASP_API.Model.Staff
{
    public class Advisor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Profile { get; set; } = string.Empty;
        public string Documents { get; set; } = string.Empty;
        public int DomainId { get; set; }
        public AdvisorStatus Status { get; set; } = AdvisorStatus.PENDING;
        public Domain Domain { get; set; }
    }
        
    public enum AdvisorStatus
    {
        PENDING, ACTIVE, RESIGNED, BLOCKED
    }
 
}
