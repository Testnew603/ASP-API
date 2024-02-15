using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;

namespace ASP_API.DTO
{
    public class ReviewerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;    
        public int DomainId { get; set; }
        public Domain? Domain { get; set; }
        public StaffStatus Status { get; set; } = StaffStatus.PENDING;
    }
}

namespace ASP_API.DTO
{
    public class ReviwerProfileDTO
    {
        public int Id { get; set; }
        public IFormFile Profile { get; set; }
    }
}

namespace ASP_API.DTO
{
    public class ReviwerDocumentDTO
    {
        public int Id { get; set; }
        public IFormFile Document { get; set; }
    }
}
