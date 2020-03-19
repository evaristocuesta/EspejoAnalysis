using EspejoAnalysis.Model;
using EspejoAnalysis.View.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace EspejoAnalysis.ViewModel
{
    public class EsterolesViewModel : ViewModelBase, IAnalysis
    {
        private IDialogService _dialogService;
        private ConfigManager _configManager;

        public EsterolesViewModel(IDialogService dialogService, ConfigManager configManager)
        {
            _dialogService = dialogService;
            _configManager = configManager;
            Results = new ObservableCollection<EsterolesResult>();
            Generar = new RelayCommand(GenerarExecute, GenerarCanExecute);

            HistoricoDirectorios = new ObservableCollection<string>(_configManager.Config.Esteroles.HistoricoDirectorios);
            SelectedDirectorio = _configManager.Config.Esteroles.HistoricoDirectorios.Count > 0 ? _configManager.Config.Esteroles.HistoricoDirectorios[0] : "";
        }

        public ICommand Generar { get; private set; }

        private string _selectedDirectorio;
        public string SelectedDirectorio
        {
            get
            {
                return _selectedDirectorio;
            }
            set
            {
                Set(ref _selectedDirectorio, value);
            }
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
            InsertaEnHistorico(SelectedDirectorio);
            if (!Directory.Exists(Path.GetDirectoryName(SelectedDirectorio)))
            {
                Output += $"{DateTime.Now.ToShortTimeString()} Error: No existe el directorio {SelectedDirectorio}\n";
                _dialogService.ShowInfoDialogAsync("Error", $"No existe el directorio {SelectedDirectorio}");
            }
            else
            {
                try
                {
                    string[] files = Directory.GetFiles(SelectedDirectorio, "*.csv", SearchOption.TopDirectoryOnly);
                    Results.Clear();
                    if (files.Length > 0)
                    {
                        foreach (string file in files)
                        {
                            if (!File.Exists(file))
                            {
                                Output += $"{DateTime.Now.ToShortTimeString()} Error: No existe el fichero {file}\n";
                                _dialogService.ShowInfoDialogAsync("Error", $"No existe el fichero {file}");
                            }
                            else
                            {
                                EsterolesLogic analysis = new EsterolesLogic();
                                try
                                {
                                    Results.Add(analysis.Calculate(file));
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
                }

            }
        }

        private bool GenerarCanExecute()
        {
            return !string.IsNullOrEmpty(SelectedDirectorio);
        }

        private void InsertaEnHistorico(string directorio)
        {
            if (HistoricoDirectorios.Contains(directorio))
            {
                HistoricoDirectorios.Remove(directorio);
            }
            if (HistoricoDirectorios.Count > 10)
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
