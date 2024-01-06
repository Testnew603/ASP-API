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
            CreateMap<GeneralManager, GeneralManagerDTO>().ReverseMap();
            CreateMap<CondonationFees, CondonationFees>();
            CreateMap<StudentAgreement, StudentAgreement>();
        }
    }
}
