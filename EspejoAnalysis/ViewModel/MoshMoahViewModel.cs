using EspejoAnalysis.Model;
using EspejoAnalysis.View.Services;
using GalaSoft.MvvmLight;

namespace EspejoAnalysis.ViewModel
{
    public class MoshMoahViewModel : ViewModelBase, IAnalysis
    {
        private IDialogService _dialogService;
        private ConfigManager _configManager;

        public MoshMoahViewModel(IDialogService dialogService, ConfigManager configManager)
        {
            _dialogService = dialogService;
            _configManager = configManager;
        }

        public void Close()
        {
            _configManager.Config.LastAnalysis = nameof(MoshMoahViewModel);
            _configManager.Save();
        }

        public void Initialize()
        {
            
        }
    }
}
