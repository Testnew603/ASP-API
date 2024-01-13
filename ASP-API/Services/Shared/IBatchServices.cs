using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IBatchServices
    {
        public Task<IEnumerable<Batch>> GetBatchList();
        public Task<Batch> GetBatchById(int? id);
        public Task<Batch> AddBatch(Batch batch);
        public Task<Batch> UpdateBatch(Batch batch);
        public Task<bool> DeleteBatch(int? id);

        //Batch allocation to advisor crud
        public Task<IEnumerable<AllocBatchToAdvisor>> GetAllocBatchToAdvisorList();
        public Task<AllocBatchToAdvisor> GetAllocBatchToAdvisorById(int? id);
        public Task<AllocBatchToAdvisor> AllocBatchToAdvisor(AllocBatchToAdvisor allocBatchToAdvisor);
        public Task<AllocBatchToAdvisor> UpdateAllocBatchToAdvisor(AllocBatchToAdvisor allocBatchToAdvisor);
        public Task<bool> DeleteAllocBatchToAdvisor(int? id);

        //Batch allocation to advisor crud
        public Task<IEnumerable<AllocBatchBranchToStudent>> GetAllocBatchToStudList();
        public Task<AllocBatchBranchToStudent> GetAllocBatchToStudById(int? id);
        public Task<AllocBatchBranchToStudent> AllocBatchToStud(AllocBatchBranchToStudent allocBatchBranchToStudent);
        public Task<AllocBatchBranchToStudent> UpdateAllocBatchToStud(AllocBatchBranchToStudent allocBatchBranchToStudent);
        public Task<bool> DeleteAllocBatchToStud(int? id);

        //Batch allocation to trainer crud
        public Task<IEnumerable<AllocBatchToTrainer>> GetAllocBatchToTrainerList();
        public Task<AllocBatchToTrainer> GetAllocBatchToTrainerById(int? id);
        public Task<AllocBatchToTrainer> AllocBatchToTrainer(AllocBatchToTrainer allocBatchToTrainer);
        public Task<AllocBatchToTrainer> UpdateAllocBatchToTrainer(AllocBatchToTrainer allocBatchToTrainer);
        public Task<bool> DeleteAllocBatchToTrainer(int? id);


    }
}