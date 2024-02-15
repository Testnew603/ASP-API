using ASP_API.Model.Staff;
using ASP_API.Model.Student;

namespace ASP_API.DTO
{
    public class HRManagerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;          
        public StaffStatus Status { get; set; }
    }
}

namespace ASP_API.DTO
{
    public class HRManagerProfileDTO
    {
        public int Id { get; set; }
        public IFormFile Profile { get; set; }
    }
}

namespace ASP_API.DTO
{
    public class HRManagerDocumentDTO
    {
        public int Id { get; set; }
        public IFormFile Document { get; set; }
    }
}
