using ASP_API.Model.Staff;

namespace ASP_API.DTO
{
    public class AdvisorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public int DomainId { get; set; }
        public AdvisorStatus Status { get; set; }        
    }
}

namespace ASP_API.DTO
{
    public class AdvisorProfileDTO
    {
        public int Id { get; set; }
        public IFormFile Profile { get; set; }
    }
}

namespace ASP_API.DTO
{
    public class AdvisorDocumentDTO
    {
        public int Id { get; set; }
        public IFormFile Document { get; set; }
    }
}


