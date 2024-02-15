using ASP_API.Model.Public;

namespace ASP_API.Model.Student
{
    public class StudentDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public string Documents { get; set; } = string.Empty;
        public int DomainId { get; set; }
        public string Password { get; set; } = string.Empty;    
        public string Profile { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.PENDING;
        public Domain? Domain { get; set; }
    }

    public enum Status
    {
        PENDING, ACTIVE, BLOCKED, PLACED, TERMINATED
    }
}
