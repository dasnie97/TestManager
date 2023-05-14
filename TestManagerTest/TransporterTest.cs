using Moq;
using TestEngineering.Interfaces;
using TestEngineering.Models;
using TestManager.Helpers;
using TestManager.Interfaces;
using TestManager.Services;

namespace TestManagerTest;

public class TransporterTest
{
    public class TransporterFactoryTests
    {
        private readonly ITransporterFactory _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();
        private readonly Mock<IStatistics> _statisticsMock = new Mock<IStatistics>();
        private readonly Mock<IWebAdapter> _webMock = new Mock<IWebAdapter>();
        private readonly Mock<ITestReportTracker> _tracker = new Mock<ITestReportTracker>();


        public TransporterFactoryTests()
        {
            _sut = new TransporterFactory(_fileProcessorMock.Object, _statisticsMock.Object, _webMock.Object, _tracker.Object);
        }

        [Theory]
        [InlineData(true, 0, nameof(PassedFilesTransporter))]
        [InlineData(true, 1, nameof(AllFilesRemover))]
        [InlineData(true, 2, nameof(AllFilesTransporter))]
        [InlineData(false, 2, nameof(NoFilesTransporter))]
        public void GetTransporter_ShouldReturnCorrectTransporterDependingOnTransferOption(bool dataTransferEnabled, int transferOption, string expectedTransporter)
        {
            _sut.IsDataTransferEnabled = dataTransferEnabled;
            _sut.TransferOption = transferOption;

            var transporter = _sut.GetTransporter();

            Assert.Equal(expectedTransporter, transporter.GetType().Name);
        }

        [Theory]
        [InlineData(true, 0, nameof(PassedFilesTransporter))]
        [InlineData(true, 1, nameof(AllFilesRemover))]
        [InlineData(true, 2, nameof(AllFilesTransporter))]
        public void GetTransporter_ShouldResetTransferOptionAndDataTransferFlag(bool dataTransferEnabled, int transferOption, string expectedTransporter)
        {
            _sut.IsDataTransferEnabled = dataTransferEnabled;
            _sut.TransferOption = transferOption;
            var expectedTransferOption = 2;
            var expectedTransferFlagValue = true;

            _sut.GetTransporter();

            Assert.Equal(expectedTransferOption, _sut.TransferOption);
            Assert.Equal(expectedTransferFlagValue, _sut.IsDataTransferEnabled);
        }

        [Fact]
        public void GetTransporter_ShouldNotResetWhenNoFilesTransporterIsReturned()
        {
            _sut.TransferOption = 2;
            _sut.IsDataTransferEnabled = false;
            var expectedTransferOption = 2;
            var expectedTransferFlagValue = false;

            _sut.GetTransporter();

            Assert.Equal(expectedTransferOption, _sut.TransferOption);
            Assert.Equal(expectedTransferFlagValue, _sut.IsDataTransferEnabled);
        }

        [Fact]
        public void GetTransporter_ShouldThrowAnExceptionWhenInvalidTransferOption()
        {
            _sut.IsDataTransferEnabled = true;
            _sut.TransferOption = -1;

            Action getTransporter = () => _sut.GetTransporter();

            Assert.Throws<InvalidOperationException>(getTransporter);
        }
    }

    public class PassedFilesTransporterTests
    {
        private readonly PassedFilesTransporter _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();
        private readonly Mock<IStatistics> _statisticsMock = new Mock<IStatistics>();
        private readonly Mock<IWebAdapter> _webMock = new Mock<IWebAdapter>();
        private readonly Mock<ITestReportTracker> _tracker = new Mock<ITestReportTracker>();

        public PassedFilesTransporterTests()
        {
            _sut = new PassedFilesTransporter(_fileProcessorMock.Object, _statisticsMock.Object, _webMock.Object, _tracker.Object);
        }

        [Fact]
        public void TransportTestReports_ShouldMoveAndCopyOnlyPassedTestReportsAndDeleteFailedOnes()
        {
            var testReports = CreateTestReports();
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.LoadFiles()).Returns(testReports);

            _sut.TransportTestReports();

            _fileProcessorMock.Verify(fileProcessor => fileProcessor.DeleteFile(It.IsAny<FileTestReport>()), Times.Once);
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.CopyFile(It.IsAny<FileTestReport>()), Times.Once);
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.MoveFile(It.IsAny<FileTestReport>()), Times.Once);
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.LoadFiles(), Times.Once);
            _fileProcessorMock.VerifyNoOtherCalls();
            _statisticsMock.Verify(statistics => statistics.Add(It.IsAny<ITrackedTestReport>()), Times.Once);
            _statisticsMock.VerifyNoOtherCalls();
        }
    }

    public class AllFilesRemoverTests
    {
        private readonly AllFilesRemover _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();

        public AllFilesRemoverTests()
        {
            _sut = new AllFilesRemover(_fileProcessorMock.Object);
        }

        [Fact]
        public void TransportTestReports_ShouldDeleteAllFiles()
        {
            var testReports = CreateTestReports();
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.LoadFiles()).Returns(testReports);

            _sut.TransportTestReports();

            _fileProcessorMock.Verify(fileProcessor => fileProcessor.DeleteFile(It.IsAny<FileTestReport>()), Times.Exactly(2));
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.LoadFiles(), Times.Once);
            _fileProcessorMock.VerifyNoOtherCalls();
        }
    }

    public class AllFilesTransporterTests
    {
        private readonly AllFilesTransporter _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();
        private readonly Mock<IStatistics> _statisticsMock = new Mock<IStatistics>();
        private readonly Mock<IWebAdapter> _webMock = new Mock<IWebAdapter>();
        private readonly Mock<ITestReportTracker> _tracker = new Mock<ITestReportTracker>();

        public AllFilesTransporterTests()
        {
            _sut = new AllFilesTransporter(_fileProcessorMock.Object, _statisticsMock.Object, _webMock.Object, _tracker.Object);
        }

        [Fact]  
        public void TransportTestReports_ShouldMoveAndCopyAllTestReports()
        {
            var testReports = CreateTestReports();
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.LoadFiles()).Returns(testReports);

            _sut.TransportTestReports();

            _fileProcessorMock.Verify(fileProcessor => fileProcessor.DeleteFile(It.IsAny<FileTestReport>()), Times.Never);
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.CopyFile(It.IsAny<FileTestReport>()), Times.Exactly(2));
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.MoveFile(It.IsAny<FileTestReport>()), Times.Exactly(2));
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.LoadFiles(), Times.Once);
            _fileProcessorMock.VerifyNoOtherCalls();
            _statisticsMock.Verify(statistics => statistics.Add(It.IsAny<ITrackedTestReport>()), Times.Exactly(2));
            _statisticsMock.VerifyNoOtherCalls();
        }
    }
    
    public class NoFilesTransporterTests
    {
        private readonly NoFilesTransporter _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();
        private readonly Mock<IStatistics> _statisticsMock = new Mock<IStatistics>();

        public NoFilesTransporterTests()
        {
            _sut = new NoFilesTransporter();
        }

        [Fact]
        public void TransportTestReports_ShouldNotTransportAnything()
        {
            var testReports = CreateTestReports();
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.LoadFiles()).Returns(testReports);

            _sut.TransportTestReports();

            _fileProcessorMock.Verify(fileProcessor => fileProcessor.DeleteFile(It.IsAny<FileTestReport>()), Times.Never);
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.CopyFile(It.IsAny<FileTestReport>()), Times.Never);
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.MoveFile(It.IsAny<FileTestReport>()), Times.Never);
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.LoadFiles(), Times.Never);
            _fileProcessorMock.VerifyNoOtherCalls();
            _statisticsMock.Verify(statistics => statistics.Add(It.IsAny<ITrackedTestReport>()), Times.Never);
            _statisticsMock.VerifyNoOtherCalls();
        }
    }

    private static List<FileTestReport> CreateTestReports()
    {
        var serialNumber = Guid.NewGuid().ToString();
        var workstation = new TestEngineering.Models.Workstation(Guid.NewGuid().ToString());
        var passedTestSteps = new List<TestStep>()
            {
                new TestStep("Test1", DateTime.Now, TestStatus.Passed)
            };
        var failedTestSteps = new List<TestStep>()
            {
                new TestStep("Test2", DateTime.Now, TestStatus.Failed)
            };
        FileTestReport failedFileTestReport = FileTestReport.Create(serialNumber, workstation, failedTestSteps);
        FileTestReport passedFileTestReport = FileTestReport.Create(serialNumber, workstation, passedTestSteps);

        return new List<FileTestReport>() { passedFileTestReport, failedFileTestReport };
    }
}