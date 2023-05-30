using TestEngineering.DTO;
using AutoMapper;
using TestManager.Models;
using TestEngineering.Models;

namespace TestManager.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RemoteWorkstation, CreateWorkstationDTO>();
        CreateMap<TestReport, CreateTestReportDTO>().ForMember(dest => dest.Workstation, opt => opt.MapFrom(src => src.Workstation.Name));
        //CreateMap<TestReport, TestReportDTO>().ForMember(dest=>dest.Workstation, opt=>opt.MapFrom(src=>src.Workstation.Name));
        //CreateMap<CreateTestReportDTO, TestReport>().ForMember(dest => dest.Workstation, opt=>opt.MapFrom(src=>new Workstation(src.Workstation, "")));
        //CreateMap<UpdateTestReportDTO, TestReport>().ForMember(dest => dest.Workstation, opt => opt.MapFrom(src => new Workstation(src.Workstation, "")));
        //CreateMap<TestReportFilterDTO, TestReportFilter>();
        //CreateMap<WorkstationFilterDTO, WorkstationFilter>();
        //CreateMap<Workstation, WorkstationDTO>();
        //CreateMap<CreateWorkstationDTO, Workstation>();
        //CreateMap<WorkstationDTO, Workstation>();
    }
}