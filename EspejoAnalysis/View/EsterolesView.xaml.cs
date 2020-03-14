using EspejoAnalysis.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

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
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = false;
            dlg.Description = "Seleccione un directorio";
            dlg.SelectedPath = cmbDirectorioSeleccionado.Text;
            DialogResult result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                cmbDirectorioSeleccionado.Text = dlg.SelectedPath;
                ((EsterolesViewModel)DataContext).SelectedDirectorio = cmbDirectorioSeleccionado.Text;
            }
        }
    }
}
