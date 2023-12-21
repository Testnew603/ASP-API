using ASP_API.Model.Public;
using ASP_API.Model.Student;
using Microsoft.VisualBasic.FileIO;

namespace ASP_API.DTO
{
    public class StudentDTO
    {

    }
}


namespace ASP_API.DTO
{
    public class StudentProfileDTO
    {
        public int Id { get; set; }
        public string Profile { get; set; } = string.Empty;


        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public FileType FileType { get; set; }
    }
        public enum FileType
        {
            PDF = 1,
            DOCX = 2
        }
}

namespace ASP_API.DTO
{
    public class StudentBasicDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public int DomainId { get; set; }
        public Status Status { get; set; } = Status.PENDING;
        public Domain? Domain { get; set; }
    }
}
