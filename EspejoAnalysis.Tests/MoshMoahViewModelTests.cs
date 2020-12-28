using ConfigManagerLib;
using EspejoAnalysis.Model;
using EspejoAnalysis.ViewModel;
using MessageDialogManagerLib;
using Moq;
using Xunit;

namespace EspejoAnalysis.Tests
{
    public class MoshMoahViewModelTests
    {
        private readonly Mock<IConfigManager<Config>> _mockConfig;
        private readonly Mock<IMessageDialogManager> _mockDialog;

        public MoshMoahViewModelTests()
        {
            _mockConfig = new Mock<IConfigManager<Config>>();
            _mockDialog = new Mock<IMessageDialogManager>();
        }

        [Fact]
        public void CloseCommandShouldSaveAnalysisType()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());

            // Act
            var moshMoahViewModel = new MoshMoahViewModel(_mockDialog.Object, _mockConfig.Object);
            moshMoahViewModel.Close();

            // Assert
            _mockConfig.Verify(c => c.Save(), Times.Once);
            Assert.Equal(nameof(MoshMoahViewModel), _mockConfig.Object.Config.LastAnalysis);
        }
    }
}
