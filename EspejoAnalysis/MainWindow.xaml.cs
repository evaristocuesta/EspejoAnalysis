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

        private void Window_Closed(object sender, EventArgs e)
        {
            ((MainViewModel)DataContext).Analysis.Close();
        }
    }
}
