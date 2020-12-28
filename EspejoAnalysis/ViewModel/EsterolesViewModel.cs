using ConfigManagerLib;
using EspejoAnalysis.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MessageDialogManagerLib;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows.Input;

namespace EspejoAnalysis.ViewModel
{
    public class EsterolesViewModel : ViewModelBase, IAnalysis
    {
        private readonly IMessageDialogManager _dialogService;
        private readonly IConfigManager<Config> _configManager;
        private readonly IEsterolesLogic _esterolesLogic;
        private readonly IFileSystem _fileSystem;

        public EsterolesViewModel(IMessageDialogManager dialogService, 
            IConfigManager<Config> configManager, 
            IEsterolesLogic esterolesLogic,
            IFileSystem fileSystem)
        {
            _dialogService = dialogService;
            _configManager = configManager;
            _esterolesLogic = esterolesLogic;
            _fileSystem = fileSystem;
            Results = new ObservableCollection<EsterolesResult>();
            Generar = new RelayCommand(GenerarExecute, GenerarCanExecute);
            SeleccionaDirectorio = new RelayCommand(SeleccionaDirectorioExecute);
            Exportar = new RelayCommand(ExportarExecute, ExportarCanExecute);
            HistoricoDirectorios = new ObservableCollection<string>(_configManager.Config.Esteroles.HistoricoDirectorios);
            SelectedDirectorio = _configManager.Config.Esteroles.HistoricoDirectorios.Count > 0 ? _configManager.Config.Esteroles.HistoricoDirectorios[0] : "";
            TextDirectorio = SelectedDirectorio;
        }

        public ICommand Generar { get; private set; }

        public ICommand SeleccionaDirectorio { get; private set; }

        public ICommand Exportar { get; private set; }

        private string _selectedDirectorio;
        public string SelectedDirectorio
        {
            get { return _selectedDirectorio; }
            set { Set(ref _selectedDirectorio, value); }
        }

        private string _textDirectorio;
        public string TextDirectorio
        {
            get { return _textDirectorio; }
            set { Set(ref _textDirectorio, value); }
        }

        public ObservableCollection<string> HistoricoDirectorios { get; set; }

        public ObservableCollection<EsterolesResult> Results { get; set; }

        private string _output;
        public string Output
        {
            get
            {
                return _output;
            }
            set
            {
                Set(ref _output, value);
            }
        }

        private void GenerarExecute()
        {
            if (!_fileSystem.Directory.Exists(_fileSystem.Path.GetDirectoryName(TextDirectorio)))
            {
                Output += $"{DateTime.Now.ToShortTimeString()} Error: No existe el directorio {TextDirectorio}\n";
                _dialogService.ShowInfoDialogAsync("Error", $"No existe el directorio {TextDirectorio}");
            }
            else
            {
                try
                {
                    string[] files = _fileSystem.Directory.GetFiles(TextDirectorio, "*.csv", SearchOption.TopDirectoryOnly);
                    Results.Clear();
                    if (files.Length > 0)
                    {
                        InsertaEnHistorico(TextDirectorio);
                        foreach (string file in files)
                        {
                            if (!_fileSystem.File.Exists(file))
                            {
                                Output += $"{DateTime.Now.ToShortTimeString()} Error: No existe el fichero {file}\n";
                                _dialogService.ShowInfoDialogAsync("Error", $"No existe el fichero {file}");
                            }
                            else
                            {
                                try
                                {
                                    Results.Add(_esterolesLogic.Calculate(file));
                                    Output += $"{ DateTime.Now.ToShortTimeString()} - {Results.Last()}\n";
                                }
                                catch (Exception ex)
                                {
                                    Output += $"{DateTime.Now.ToShortTimeString()} Error: {ex.Message}\n";
                                    _dialogService.ShowInfoDialogAsync("Error", $"Error: {ex.Message}");
                                }
                            }
                        }
                    }
                    else
                    {
                        _dialogService.ShowInfoDialogAsync("Info", "No hay análisis en este directorio");
                    }
                    Output += $"{DateTime.Now.ToShortTimeString()} Info: Hecho\n";
                }
                catch (Exception ex)
                {
                    Output += $"{DateTime.Now.ToShortTimeString()} Error: {ex.Message}\n";
                    _dialogService.ShowInfoDialogAsync("Error", $"Error: {ex.Message}");
                }
            }
        }

        private bool GenerarCanExecute()
        {
            return !string.IsNullOrEmpty(TextDirectorio);
        }

        private void SeleccionaDirectorioExecute()
        {
            if (_dialogService.ShowFolderBrowser("Seleccione un directorio", SelectedDirectorio))
            {
                TextDirectorio = _dialogService.FolderPath;
            }
        }

        private bool ExportarCanExecute()
        {
            return Results.Count > 0;
        }

        private void ExportarExecute()
        {
            if (_dialogService.ShowSaveFileDialog("Exportar CSV", "", "export.csv", ".csv", "CSV documents (.csv)|*.csv"))
            {
                try
                {
                    _esterolesLogic.Export(_dialogService.FilePathToSave, Results);
                }
                catch (Exception ex)
                {
                    _dialogService.ShowInfoDialogAsync("Error", $"Error: {ex.Message}");
                }
            }
        }

        private void InsertaEnHistorico(string directorio)
        {
            if (HistoricoDirectorios.Contains(directorio))
            {
                HistoricoDirectorios.Remove(directorio);
            }
            if (HistoricoDirectorios.Count >= 10)
            {
                HistoricoDirectorios.RemoveAt(HistoricoDirectorios.Count - 1);
            }
            HistoricoDirectorios.Insert(0, directorio);
            SelectedDirectorio = directorio;
        }

        public void Initialize()
        {
            Output = "";
            Results.Clear();
        }

        public void Close()
        {
            _configManager.Config.Esteroles.HistoricoDirectorios = HistoricoDirectorios.ToList();
            _configManager.Config.LastAnalysis = nameof(EsterolesViewModel);
            _configManager.Save();
        }
    }
}
