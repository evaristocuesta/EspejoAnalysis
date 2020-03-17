using EspejoAnalysis.Helper;
using EspejoAnalysis.Model;
using EspejoAnalysis.View.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace EspejoAnalysis.ViewModel
{
    public class EsterolesViewModel : ViewModelBase, IAnalysis
    {
        private const string PATH_CONFIG = @".\Config.xml";
        private Config _config;
        private IDialogService _dialogService;

        public EsterolesViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Results = new ObservableCollection<EsterolesResult>();
            Generar = new RelayCommand(GenerarExecute, GenerarCanExecute);
            if (!File.Exists(Path.GetDirectoryName(PATH_CONFIG)))
                _config = Serializer.Deserialize<Config>(System.IO.File.ReadAllText(PATH_CONFIG));
            else
                _config = new Config();
            HistoricoDirectorios = new ObservableCollection<string>(_config.HistoricoDirectorios);
            SelectedDirectorio = _config.HistoricoDirectorios.Count > 0 ? _config.HistoricoDirectorios[0] : "";
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
        }

        public void Close()
        {
            try
            {
                _config.HistoricoDirectorios = HistoricoDirectorios.ToList();
                if (!Directory.Exists(Path.GetDirectoryName(PATH_CONFIG)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(PATH_CONFIG));
                }
                File.WriteAllText(PATH_CONFIG, Serializer.Serialize(_config));
            }
            catch (Exception ex)
            {
                Output = ex.Message;
            }
        }
    }
}
