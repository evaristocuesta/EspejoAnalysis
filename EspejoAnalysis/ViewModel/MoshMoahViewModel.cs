using ConfigManagerLib;
using EspejoAnalysis.Model;
using GalaSoft.MvvmLight;
using MessageDialogManagerLib;

namespace EspejoAnalysis.ViewModel
{
    public class MoshMoahViewModel : ViewModelBase, IAnalysis
    {
        private readonly IMessageDialogManager _dialogService;
        private readonly IConfigManager<Config> _configManager;

        public MoshMoahViewModel(IMessageDialogManager dialogService, IConfigManager<Config> configManager)
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
