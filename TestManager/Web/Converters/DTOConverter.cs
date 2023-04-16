using ProductTest.DTO;
using ProductTest.Models;

namespace TestManager.Web.Converters;

public static class DTOConverter
{
    public static CreateTestReportDTO ToCreateTestReportDTO(TestReport source)
    {
        CreateTestReportDTO dto = new CreateTestReportDTO();

        dto.Workstation = source.Workstation.Name;
        dto.SerialNumber= source.SerialNumber;
        dto.Status = source.Status.ToString();
        dto.TestDateTimeStarted= source.TestDateTimeStarted;
        dto.TestingTime = source.TestingTime;
        dto.FixtureSocket = source.FixtureSocket;
        dto.Failure = source.Failure;

        return dto;
    }

    public static CreateWorkstationDTO ToCreateWorkstationDTO(RemoteWorkstation source)
    {
        CreateWorkstationDTO dto = new CreateWorkstationDTO();

        dto.Name = source.Name;
        dto.OperatorName = source.OperatorName;
        dto.ProcessStep = source.ProcessStep;

        return dto;
    }
}
