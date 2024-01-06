using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using Microsoft.EntityFrameworkCore;
using Type = ASP_API.Model.Public.Type;

namespace ASP_API
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }
        public DbSet<StudentDetails> Students { get; set; }
        public DbSet<Domain> Domains { get; set; }    
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<AllocatedCourse> AllocatedCourses { get; set; }
        public DbSet<Advisor> Advisors { get; set; }
        public DbSet<CourseFee> CourseFees { get; set; }
        public DbSet<CondonationFees> CondonationFees { get; set; }
        public DbSet<StudentAgreement> StudentAgreement { get; set; }
        public DbSet<HRManager> HRManager { get; set; }
        public DbSet<GeneralManager> GeneralManager { get; set; }
        public DbSet<Reviewer> Reviewer { get; set; }   
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<Fees> Fees { get; set; }
        public DbSet<AllocStudentToAdvisor> AllocStudentToAdvisor  { get; set; }
        public DbSet<AllocBatchToAdvisor> AllocBatchToAdvisors { get; set; }
        public DbSet<AllocBatchToAdvisor> BatchToAdvisors { get; set; }
        public DbSet<AllocBatchBranchToStudent> AllocBatchBranchToStudent { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Status>().HaveConversion<string>();   
            configurationBuilder.Properties<AdvisorStatus>().HaveConversion<string>();
            configurationBuilder.Properties<Type>().HaveConversion<string>();
            configurationBuilder.Properties<FineType>().HaveConversion<string>();
            configurationBuilder.Properties<FineStatus>().HaveConversion<string>(); 
            configurationBuilder.Properties<AgreementStatus>().HaveConversion<string>();
            configurationBuilder.Properties<FineStatus>().HaveConversion<string>();
            configurationBuilder.Properties<StudentToAdvisorStatus>().HaveConversion<string>();
            configurationBuilder.Properties<BatchToAdvisorStatus>().HaveConversion<string>();
            configurationBuilder.Properties<BatchBranchToStudentStatus>().HaveConversion<string>();
            configurationBuilder.Properties<SpecializedIn>().HaveConversion<string>(); 
        }
    }
}
