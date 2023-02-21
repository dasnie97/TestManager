using Moq;
using ProductTest.Common;
using ProductTest.Models;
using ProductTestTest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.ConfigHelpers;
using TestManager.FileHelpers;
using TestManager.Helpers;
using TestManager.Transporters;

namespace TestManagerTest;

public class TransporterTest
{
    public class TransporterFactoryTests
    {
        private readonly TransporterFactory _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();
        private readonly Mock<IStatistics> _statisticsMock = new Mock<IStatistics>();

        public TransporterFactoryTests()
        {
            _sut = new TransporterFactory(_fileProcessorMock.Object, _statisticsMock.Object);
        }

        [Theory]
        [InlineData(true, 0, nameof(PassedFilesTransporter))]
        [InlineData(true, 1, nameof(AllFilesRemover))]
        [InlineData(true, 2, nameof(AllFilesTransporter))]
        [InlineData(false, 2, nameof(NoFilesTransporter))]
        public void GetTransporter_ShouldReturnCorrectTransporterDependingOnTransferOption(bool dataTransferEnabled, int transferOption, string expectedTransporter)
        {
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.IsDataTransferEnabled).Returns(dataTransferEnabled);
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.TransferOption).Returns(transferOption);

            var transporter = _sut.GetTransporter();

            Assert.Equal(expectedTransporter, transporter.GetType().Name);
        }

        [Fact]
        public void GetTransporter_ShouldThrowAnExceptionWhenInvalidTransferOption()
        {
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.IsDataTransferEnabled).Returns(true);
            _fileProcessorMock.Setup(fileProcessor => fileProcessor.TransferOption).Returns(-1);

            Action getTransporter = () => _sut.GetTransporter();

            Assert.Throws<InvalidOperationException>(getTransporter);
        }
    }

    public class PassedFilesTransporterTests
    {
        private readonly PassedFilesTransporter _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();
        private readonly Mock<IStatistics> _statisticsMock = new Mock<IStatistics>();

        public PassedFilesTransporterTests()
        {
            _sut = new PassedFilesTransporter(_fileProcessorMock.Object, _statisticsMock.Object);
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
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.Reset(), Times.Once);
            _fileProcessorMock.VerifyNoOtherCalls();
            _statisticsMock.Verify(statistics => statistics.Add(It.IsAny<TrackedTestReport>()), Times.Once);
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
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.Reset(), Times.Once);
            _fileProcessorMock.VerifyNoOtherCalls();
        }
    }

    public class AllFilesTransporterTests
    {
        private readonly AllFilesTransporter _sut;
        private readonly Mock<IFileProcessor> _fileProcessorMock = new Mock<IFileProcessor>();
        private readonly Mock<IStatistics> _statisticsMock = new Mock<IStatistics>();

        public AllFilesTransporterTests()
        {
            _sut = new AllFilesTransporter(_fileProcessorMock.Object, _statisticsMock.Object);
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
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.Reset(), Times.Once);
            _fileProcessorMock.VerifyNoOtherCalls();
            _statisticsMock.Verify(statistics => statistics.Add(It.IsAny<TrackedTestReport>()), Times.Exactly(2));
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
            _fileProcessorMock.Verify(fileProcessor => fileProcessor.Reset(), Times.Never);
            _fileProcessorMock.VerifyNoOtherCalls();
            _statisticsMock.Verify(statistics => statistics.Add(It.IsAny<TrackedTestReport>()), Times.Never);
            _statisticsMock.VerifyNoOtherCalls();
        }
    }

    private static List<FileTestReport> CreateTestReports()
    {
        var serialNumber = Guid.NewGuid().ToString();
        var workstation = Guid.NewGuid().ToString();
        var passedTestSteps = new List<TestStepBase>()
            {
                TestStep.Create("Test1", DateTime.Now, TestStatus.Passed)
            };
        var failedTestSteps = new List<TestStepBase>()
            {
                TestStep.Create("Test2", DateTime.Now, TestStatus.Failed)
            };
        FileTestReport failedFileTestReport = FileTestReport.Create(serialNumber, workstation, failedTestSteps);
        FileTestReport passedFileTestReport = FileTestReport.Create(serialNumber, workstation, passedTestSteps);

        return new List<FileTestReport>() { passedFileTestReport, failedFileTestReport };
    }
}