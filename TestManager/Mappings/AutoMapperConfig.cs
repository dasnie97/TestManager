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
        CreateMap<RemoteWorkstation, WorkstationDTO>();
        CreateMap<TestReport, CreateTestReportDTO>().ForMember(dest => dest.Workstation, opt => opt.MapFrom(src => src.Workstation.Name));
        CreateMap<DowntimeReport, CreateDowntimeReportDTO>();
    }
}