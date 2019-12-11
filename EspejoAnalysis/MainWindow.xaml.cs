using EspejoAnalysis.ViewModel;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Forms;

namespace EspejoAnalysis
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
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
                ((MainViewModel)DataContext).SelectedDirectorio = cmbDirectorioSeleccionado.Text;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((MainViewModel)DataContext).CierraAplicacion();
        }
    }
}
