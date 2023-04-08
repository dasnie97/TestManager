using ProductTest.DTO;
using TestManager.Features.Analysis;

namespace TestManager.Web.Converters;

public static class DTOConverter
{
    public static CreateTestReportDTO ToCreateTestReportDTO(TrackedTestReport source)
    {
        CreateTestReportDTO dto = new CreateTestReportDTO();

        dto.Workstation = source.Workstation.Name;
        dto.SerialNumber= source.SerialNumber;
        dto.Status = source.Status.ToString();
        dto.TestDateTimeStarted= source.TestDateTimeStarted;
        dto.TestingTime = source.TestingTime;
        dto.FixtureSocket = source.FixtureSocket;
        dto.Failure = source.Failure;
        dto.ProcessStep = source.ProcessStep;

        return dto;
    }
}
