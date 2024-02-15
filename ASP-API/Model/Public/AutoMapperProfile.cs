using ASP_API.DTO;
using ASP_API.Model.Staff;
using AutoMapper;

namespace ASP_API.Model.Public
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AdvisorDTO, Advisor>().ReverseMap();
            CreateMap<AdvisorStatusUpdateDTO, Advisor>().ReverseMap();
            CreateMap<GeneralManager, GeneralManagerDTO>().ReverseMap();
            CreateMap<CondonationFees, CondonationFees>();
            CreateMap<StudentAgreement, StudentAgreement>();
            CreateMap<Fees, Fees>();
            CreateMap<AllocStudentToAdvisor, AllocStudentToAdvisor>();
            CreateMap<AllocStudentToAdvisor, AllocStudentToAdvisorDTO>();            
            CreateMap<Fees, FeeDTO>();
            CreateMap<ReviewUpdates, ReviewUpdates>();
            CreateMap<StudentAttendance, StudAttendanceEntryDTO>().ReverseMap();
            CreateMap<StudentAttendance, StudAttendanceExitDTO>().ReverseMap();
        }
    }
}
