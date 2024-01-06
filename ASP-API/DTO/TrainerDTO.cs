using ASP_API.Model.Staff;
using ASP_API.Model.Student;

namespace ASP_API.DTO
{
    public class TrainerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;        
        public string Profile { get; set; } = string.Empty;
        public SpecializedIn SpecializedIn { get; set; } = SpecializedIn.OTHERS;
        public Status Status { get; set; } = Status.PENDING;
    }
}

namespace ASP_API.DTO
{
    public class TrainerProfileDTO
    {
        public int Id { get; set; }
        public IFormFile Profile { get; set; }
    }
}
