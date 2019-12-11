using EspejoAnalysis.Const;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EspejoAnalysis
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //CultureInfo.CurrentCulture = new CultureInfo("es-ES");
            //CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = Constants.DECIMAL_SEPARATOR;
            //CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator = Constants.GROUP_SEPARATOR;
        }
    }
}
