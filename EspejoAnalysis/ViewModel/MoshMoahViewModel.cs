﻿using EspejoAnalysis.Model;
using GalaSoft.MvvmLight;
using MessageDialogManagerLib;

namespace EspejoAnalysis.ViewModel
{
    public class MoshMoahViewModel : ViewModelBase, IAnalysis
    {
        private IMessageDialogManager _dialogService;
        private ConfigManager _configManager;

        public MoshMoahViewModel(IMessageDialogManager dialogService, ConfigManager configManager)
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
