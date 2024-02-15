using ASP_API.Model.Public;

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
        public StaffStatus Status { get; set; }
        public Domain Domain { get; set; }
    }
        
    public enum StaffStatus
    {
        PENDING, ACTIVE, RESIGNED, BLOCKED
    }   

}

namespace ASP_API.Model.Staff
{
    public class AdvisorDTO1
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string qualification { get; set; }
        public string password { get; set; }
        public string domainId { get; set; }
    }
}
