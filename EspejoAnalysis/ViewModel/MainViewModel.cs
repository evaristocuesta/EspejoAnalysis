using ConfigManagerLib;
using EspejoAnalysis.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;

namespace EspejoAnalysis.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IConfigManager<Config> _configManager;
        private Dictionary<string, IAnalysis> _analysisViewModels;
        private IAnalysis _analysisSelected;

        public MainViewModel(IConfigManager<Config> configManager,
            Dictionary<string, IAnalysis> analysisViewModels)
        {
            _configManager = configManager;
            _analysisViewModels = analysisViewModels;
            ShowAnalysisCommand = new RelayCommand<Type>(ShowAnalysisExecute);
            CloseCommand = new RelayCommand(CloseExecute);
            if (!string.IsNullOrEmpty(_configManager.Config.LastAnalysis))
                ShowAnalysis(_configManager.Config.LastAnalysis);
        }

        public IAnalysis AnalysisSelected
        {
            get
            {
                return _analysisSelected;
            }
            set
            {
                Set(ref _analysisSelected, value);
            }
        }

        public string TitleVersion
        {
            get { return $"Espejo Analysis - Ver. {Assembly.GetExecutingAssembly().GetName().Version}"; }
        }

        public ICommand ShowAnalysisCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        private void ShowAnalysisExecute(Type type)
        {
            ShowAnalysis(type.Name);
        }

        private void ShowAnalysis(string analysisName)
        {
            AnalysisSelected?.Close();
            IAnalysis analysis = _analysisViewModels[analysisName];
            analysis.Initialize();
            AnalysisSelected = analysis;
        }

        private void CloseExecute()
        {
            AnalysisSelected?.Close();
        }
    }
}
