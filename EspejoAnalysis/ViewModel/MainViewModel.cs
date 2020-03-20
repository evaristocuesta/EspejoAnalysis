using EspejoAnalysis.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Reflection;
using System.Windows.Input;

namespace EspejoAnalysis.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ConfigManager _configManager;
        private IAnalysis _analysis;

        public MainViewModel(ConfigManager configManager)
        {
            _configManager = configManager;
            ShowAnalysisCommand = new RelayCommand<Type>(ShowAnalysisExecute);
            CloseCommand = new RelayCommand<Type>(CloseExecute);
            if (!string.IsNullOrEmpty(_configManager.Config.LastAnalysis))
                ShowAnalysis(_configManager.Config.LastAnalysis);
        }

        public IAnalysis Analysis
        {
            get
            {
                return _analysis;
            }
            set
            {
                Set(ref _analysis, value);
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
            Analysis?.Close();
            IAnalysis analysis = SimpleIoc.Default.GetInstance<IAnalysis>(analysisName);
            analysis.Initialize();
            Analysis = analysis;
        }

        private void CloseExecute(Type obj)
        {
            Analysis?.Close();
        }
    }
}
