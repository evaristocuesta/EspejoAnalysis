using ConfigManagerLib;
using EspejoAnalysis.Model;
using EspejoAnalysis.ViewModel;
using MessageDialogManagerLib;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using Xunit;

namespace EspejoAnalysis.Tests
{
    public class MainViewModelTests
    {
        private readonly Mock<IConfigManager<Config>> _mockConfig;
        private readonly Mock<IMessageDialogManager> _mockDialog;
        private readonly Mock<IFileSystem> _mockFileSystem;
        private readonly Dictionary<string, IAnalysis> _analysisViewModels;
        private readonly Dictionary<string, Mock<IAnalysis>> _mockAnalysisViewModels;
        private readonly Mock<IAnalysis> _mockEsterolesViewModel;
        private readonly Mock<IEsterolesLogic> _esterolesLogic;
        private readonly Mock<IAnalysis> _mockMoshMoahViewModel;

        public MainViewModelTests()
        {
            _mockConfig = new Mock<IConfigManager<Config>>();
            _mockDialog = new Mock<IMessageDialogManager>();
            _mockFileSystem = new Mock<IFileSystem>();
            _esterolesLogic = new Mock<IEsterolesLogic>();
            _analysisViewModels = new Dictionary<string, IAnalysis>();
            _mockAnalysisViewModels = new Dictionary<string, Mock<IAnalysis>>();
            _mockEsterolesViewModel = new Mock<IAnalysis>();
            _mockMoshMoahViewModel = new Mock<IAnalysis>();
            _analysisViewModels.Add(nameof(EsterolesViewModel), 
                _mockEsterolesViewModel.Object);
            _analysisViewModels.Add(nameof(MoshMoahViewModel), 
                _mockMoshMoahViewModel.Object);
            _mockAnalysisViewModels.Add(nameof(EsterolesViewModel),
                _mockEsterolesViewModel);
            _mockAnalysisViewModels.Add(nameof(MoshMoahViewModel),
                _mockMoshMoahViewModel);
        }

        [Theory]
        [InlineData(typeof(EsterolesViewModel))]
        [InlineData(typeof(MoshMoahViewModel))]
        public void ShowAnalysisCommandShouldCallInitializeOnce(Type type)
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());

            // Act
            var mainViewModel = new MainViewModel(_mockConfig.Object, _analysisViewModels);
            mainViewModel.ShowAnalysisCommand.Execute(type);

            // Assert
            _mockAnalysisViewModels[type.Name].Verify(e => e.Initialize(), Times.Once);
        }

        [Theory]
        [InlineData(typeof(EsterolesViewModel))]
        [InlineData(typeof(MoshMoahViewModel))]
        public void CloseCommandShouldCallCloseOnce(Type type)
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());

            // Act
            var mainViewModel = new MainViewModel(_mockConfig.Object, _analysisViewModels);
            mainViewModel.ShowAnalysisCommand.Execute(type);
            mainViewModel.CloseCommand.Execute(null);

            // Assert
            _mockAnalysisViewModels[type.Name].Verify(e => e.Initialize(), Times.Once);
            _mockAnalysisViewModels[type.Name].Verify(e => e.Close(), Times.Once);
        }

        [Fact]
        public void ChangeFromEsterolesToMoshMoahShouldCallEsterolesCloseAndMoshMoahInitialize()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());

            // Act
            var mainViewModel = new MainViewModel(_mockConfig.Object, _analysisViewModels);
            mainViewModel.ShowAnalysisCommand.Execute(typeof(EsterolesViewModel));
            mainViewModel.ShowAnalysisCommand.Execute(typeof(MoshMoahViewModel));

            // Assert
            _mockAnalysisViewModels[nameof(EsterolesViewModel)]
                .Verify(e => e.Initialize(), Times.Once);
            _mockAnalysisViewModels[nameof(EsterolesViewModel)]
                .Verify(e => e.Close(), Times.Once);
            _mockAnalysisViewModels[nameof(MoshMoahViewModel)]
                .Verify(e => e.Initialize(), Times.Once);
        }
    }
}
