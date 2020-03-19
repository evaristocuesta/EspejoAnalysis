/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SampleMvvmLightProject"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using EspejoAnalysis.Model;
using EspejoAnalysis.View.Services;
using GalaSoft.MvvmLight.Ioc;

namespace EspejoAnalysis.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<ConfigManager>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IAnalysis>(() => new EsterolesViewModel(SimpleIoc.Default.GetInstance<IDialogService>(),
                SimpleIoc.Default.GetInstance<ConfigManager>()), 
                nameof(EsterolesViewModel));
            SimpleIoc.Default.Register<IAnalysis>(() => new MoshMoahViewModel(SimpleIoc.Default.GetInstance<IDialogService>(),
                SimpleIoc.Default.GetInstance<ConfigManager>()), 
                nameof(MoshMoahViewModel));
        }

        public MainViewModel Main
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
        }

        public EsterolesViewModel Esteroles
        {
            get
            {
                return SimpleIoc.Default.GetInstance<EsterolesViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}