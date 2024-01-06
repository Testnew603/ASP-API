using ASP_API.Model.Public;

namespace ASP_API.Model.Public
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
