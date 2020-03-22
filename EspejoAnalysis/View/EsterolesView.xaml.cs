using EspejoAnalysis.ViewModel;
using System.Windows;
using WPFFolderBrowser;

namespace EspejoAnalysis.View
{
    /// <summary>
    /// Interaction logic for EsterolesView.xaml
    /// </summary>
    public partial class EsterolesView : System.Windows.Controls.UserControl
    {
        public EsterolesView()
        {
            InitializeComponent();
        }

        private void btnSeleccionarDirectorio_Click(object sender, RoutedEventArgs e)
        {
            WPFFolderBrowserDialog dlg = new WPFFolderBrowserDialog();
            dlg.InitialDirectory = cmbDirectorioSeleccionado.Text;
            if (dlg.ShowDialog().Value)
            {
                cmbDirectorioSeleccionado.Text = dlg.FileName;
                ((EsterolesViewModel)DataContext).SelectedDirectorio = cmbDirectorioSeleccionado.Text;
            }
        }
    }
}
