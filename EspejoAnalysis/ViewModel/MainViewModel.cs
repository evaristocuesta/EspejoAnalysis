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
        private IAnalysis _analysis;

        public MainViewModel()
        {
            ShowAnalysisCommand = new RelayCommand<Type>(ShowAnalysisExecute);
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

        private void ShowAnalysisExecute(Type type)
        {
            Analysis?.Close();
            IAnalysis analysis = SimpleIoc.Default.GetInstance<IAnalysis>(type.Name);
            analysis.Initialize();
            Analysis = analysis;
        }

    }
}
