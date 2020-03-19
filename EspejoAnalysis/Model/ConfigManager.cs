using EspejoAnalysis.Helper;
using EspejoAnalysis.View.Services;
using System;
using System.IO;

namespace EspejoAnalysis.Model
{
    public class ConfigManager
    {
        private const string PATH_CONFIG = @".\Config.xml";
        private Config _config;
        private IDialogService _dialogService;

        public ConfigManager(IDialogService dialogService)
        {
            _dialogService = dialogService;
            try
            {
                if (File.Exists(PATH_CONFIG))
                    _config = Serializer.Deserialize<Config>(System.IO.File.ReadAllText(PATH_CONFIG));
                else
                    _config = new Config();
            }
            catch (Exception ex)
            {
                _dialogService.ShowInfoDialogAsync("Error", $"Error de lectura del archivo de configuración: {ex.Message}");
            }
        }

        public Config Config 
        { 
            get
            {
                return _config;
            }
            private set
            {
                _config = value;
            }
        }

        public void Save()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(PATH_CONFIG)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(PATH_CONFIG));
                }
                File.WriteAllText(PATH_CONFIG, Serializer.Serialize(_config));
            }
            catch (Exception ex)
            {
                _dialogService.ShowInfoDialogAsync("Error", $"Error guardando el archivo de configuración: {ex.Message}");
            }
        }
    }
}
