using ASP_API.Model.Student;

namespace ASP_API.Model.Staff
{
    public class GeneralManager
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
        public StaffStatus Status { get; set; } = StaffStatus.PENDING;
    }
}
