using ConfigManagerLib;
using EspejoAnalysis.Model;
using EspejoAnalysis.ViewModel;
using MessageDialogManagerLib;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EspejoAnalysis.Tests
{
    public class EsterolesViewModelTests
    {
        private readonly Mock<IConfigManager<Config>> _mockConfig;
        private readonly Mock<IMessageDialogManager> _mockDialog;
        private readonly Mock<IEsterolesLogic> _mockEsterolesLogic;
        private readonly Mock<IFileSystem> _mockFileSystem;

        public EsterolesViewModelTests()
        {
            _mockConfig = new Mock<IConfigManager<Config>>();
            _mockDialog = new Mock<IMessageDialogManager>();
            _mockEsterolesLogic = new Mock<IEsterolesLogic>();
            _mockFileSystem = new Mock<IFileSystem>();
        }

        [Fact]
        public void CloseCommandShouldSaveAnalysisType()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());

            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.Close();

            // Assert
            _mockConfig.Verify(c => c.Save(), Times.Once);
            Assert.Equal(nameof(EsterolesViewModel), _mockConfig.Object.Config.LastAnalysis);
        }

        [Fact]
        public void SeleccionaDirectorioCommandShouldSelectADirectory()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _mockDialog.SetupGet(d => d.FolderPath)
                .Returns(@"C:\Example");
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);

            // Assert
            _mockDialog.Verify(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
            Assert.Equal(@"C:\Example", esterolesViewModel.TextDirectorio);
        }

        [Fact]
        public void GenerarCommandShouldExecute()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _mockDialog.SetupGet(d => d.FolderPath)
                .Returns(@"C:\Example");
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);

            // Assert
            Assert.True(esterolesViewModel.Generar.CanExecute(null));
        }

        [Fact]
        public void GenerarCommandShouldNotExecute()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);

            // Assert
            Assert.False(esterolesViewModel.Generar.CanExecute(null));
        }

        [Fact]
        public void GenerarCommandShouldSetSelectedDirectory()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _mockDialog.SetupGet(d => d.FolderPath)
                .Returns(@"C:\Example");
            _mockFileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>()))
                .Returns(true);
            _mockFileSystem.Setup(f => f.Path.GetDirectoryName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);
            esterolesViewModel.Generar.Execute(null);

            // Assert
            Assert.Equal(@"C:\Example", esterolesViewModel.SelectedDirectorio);
        }

        [Fact]
        public void GenerarCommandShouldInsertDirectoryInEmptyHistory()
        {
            // Arrange
            _mockConfig.SetupGet(config => config.Config)
                .Returns(new Config());
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _mockDialog.SetupGet(d => d.FolderPath)
                .Returns(@"C:\Example");
            _mockFileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>()))
                .Returns(true);
            _mockFileSystem.Setup(f => f.Path.GetDirectoryName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);
            esterolesViewModel.Generar.Execute(null);

            // Assert
            Assert.Collection(esterolesViewModel.HistoricoDirectorios,
                item => Assert.Equal(@"C:\Example", item));
        }

        [Fact]
        public void GenerarCommandShouldInsertDirectoryFirstItemInHistoryWithItems()
        {
            // Arrange
            Config config = new Config();
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example1");
            _mockConfig.SetupGet(c => c.Config)
                .Returns(config);
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _mockDialog.SetupGet(d => d.FolderPath)
                .Returns(@"C:\Example2");
            _mockFileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>()))
                .Returns(true);
            _mockFileSystem.Setup(f => f.Path.GetDirectoryName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);
            esterolesViewModel.Generar.Execute(null);

            // Assert
            Assert.Collection(esterolesViewModel.HistoricoDirectorios,
                item => Assert.Equal(@"C:\Example2", item),
                item => Assert.Equal(@"C:\Example1", item));
        }

        [Fact]
        public void GenerarCommandShouldInsertExistingDirectoryFirstItemInHistoryWithoutRepetition()
        {
            // Arrange
            Config config = new Config();
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example1");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example2");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example3");
            _mockConfig.SetupGet(c => c.Config)
                .Returns(config);
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _mockDialog.SetupGet(d => d.FolderPath)
                .Returns(@"C:\Example2");
            _mockFileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>()))
                .Returns(true);
            _mockFileSystem.Setup(f => f.Path.GetDirectoryName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);
            esterolesViewModel.Generar.Execute(null);

            // Assert
            Assert.Collection(esterolesViewModel.HistoricoDirectorios,
                item => Assert.Equal(@"C:\Example2", item),
                item => Assert.Equal(@"C:\Example1", item),
                item => Assert.Equal(@"C:\Example3", item));
        }

        [Fact]
        public void GenerarCommandShouldInsertDirectoryFirstItemInHistoryWithTenItemsAndDeleteLastItem()
        {
            // Arrange
            Config config = new Config();
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example1");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example2");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example3");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example4");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example5");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example6");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example7");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example8");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example9");
            config.Esteroles.HistoricoDirectorios.Add(@"C:\Example10");
            _mockConfig.SetupGet(c => c.Config)
                .Returns(config);
            _mockDialog.Setup(d => d.ShowFolderBrowser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _mockDialog.SetupGet(d => d.FolderPath)
                .Returns(@"C:\Example11");
            _mockFileSystem.Setup(f => f.Directory.Exists(It.IsAny<string>()))
                .Returns(true);
            _mockFileSystem.Setup(f => f.Path.GetDirectoryName(It.IsAny<string>()))
                .Returns(It.IsAny<string>());
            // Act
            var esterolesViewModel = new EsterolesViewModel(_mockDialog.Object, _mockConfig.Object,
                _mockEsterolesLogic.Object, _mockFileSystem.Object);
            esterolesViewModel.SeleccionaDirectorio.Execute(null);
            esterolesViewModel.Generar.Execute(null);

            // Assert
            Assert.Collection(esterolesViewModel.HistoricoDirectorios,
                item => Assert.Equal(@"C:\Example11", item),
                item => Assert.Equal(@"C:\Example1", item),
                item => Assert.Equal(@"C:\Example2", item),
                item => Assert.Equal(@"C:\Example3", item),
                item => Assert.Equal(@"C:\Example4", item),
                item => Assert.Equal(@"C:\Example5", item),
                item => Assert.Equal(@"C:\Example6", item),
                item => Assert.Equal(@"C:\Example7", item),
                item => Assert.Equal(@"C:\Example8", item),
                item => Assert.Equal(@"C:\Example9", item));
        }
    }
}
