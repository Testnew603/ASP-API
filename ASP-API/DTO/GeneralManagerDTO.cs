using ASP_API.Model.Student;

namespace ASP_API.DTO
{
    public class GeneralManagerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;                        
        public Status Status { get; set; } = Status.PENDING;
    }
}

namespace ASP_API.DTO
{
    public class GeneralManagerProfileDTO
    {
        public int Id { get; set; }
        public IFormFile Profile { get; set; }
    }
}

namespace ASP_API.DTO
{
    public class GeneralManagerDocumentDTO
    {
        public int Id { get; set; }
        public IFormFile Document { get; set; }
    }
}
