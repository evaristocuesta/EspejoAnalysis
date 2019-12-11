using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EspejoAnalysis.Model
{
    public class Esteroles
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Esteroles()
        {
            ListAreas = new List<double>();
            PercentAreas = new List<double>();
        }

        public List<double> ListAreas { get; set; }

        public List<double> PercentAreas { get; set; }

        public double Area { get; set; }

        public double Area1_16 { get; set; }

        public double Area1_16Without2 { get; set; }

        public double Area1_18Without2 { get; set; }

        public double PercentAreaEritrodiol { get; set; }

        public double PercentAreaUvaol { get; set; }

        public string Calculate(string path)
        {
            log.Info("-----------------------------------------");
            log.Info($"Calculate path {path}");
            string output = "";
            string[] split = new string[3];
            IEnumerable<string> filas = File.ReadLines(path);
            if (filas.Count(f => true) != 18)
            {
                log.Error($"El archivo {path} no tiene el formato correcto");
                throw new Exception($"El archivo {path} no tiene el formato correcto\n");
            }
            else
            {
                foreach (string fila in filas)
                {
                    log.Info($"Fila: {fila}");
                    split = fila.Split('\t');
                    double numero = 0;
                    log.Info($"split[2]: {split[2]}");
                    if (double.TryParse(split[2], NumberStyles.Float, new CultureInfo("en-US"), out numero))
                    {
                        log.Info($"Convierte el número: {numero}");
                        ListAreas.Add(numero);
                    }
                    else
                    {
                        log.Error("No puede convertir el número");
                        throw new Exception($"El archivo {path} no tiene el formato correcto\n");
                    }
                }

            }

            Area = ListAreas.Sum();
            Area1_16 = Area - ListAreas[16] - ListAreas[17];
            Area1_18Without2 = ListAreas.Sum() - ListAreas[1];
            Area1_16Without2 = Area1_18Without2 - ListAreas[16] - ListAreas[17];
            for (int i = 0; i < ListAreas.Count - 2; i++)
            {
                PercentAreas.Add(ListAreas[i] * 100 / Area1_16Without2);
            }
            PercentAreas[1] = ListAreas[1] * 100 / Area1_16;
            PercentAreas.Add(ListAreas[16] * 100 / Area1_18Without2);
            PercentAreas.Add(ListAreas[17] * 100 / Area1_18Without2);

            Colesterol = Math.Round(PercentAreas[0], 1);
            Brasicasterol = Math.Round(PercentAreas[2], 1);
            Campesterol = Math.Round(PercentAreas[4], 1);
            Estigmasterol = Math.Round(PercentAreas[6], 1);
            βSitosterol = Math.Round(PercentAreas[8] + PercentAreas[9] + PercentAreas[10] + PercentAreas[11] + PercentAreas[12] + PercentAreas[13], 1);
            δ7Estigmastenol = Math.Round(PercentAreas[14], 1);
            EritrodiolPlusUvaol = Math.Round(PercentAreas[16] + PercentAreas[17], 1);
            Patron = Math.Round((100 - PercentAreas[1]) * 200 / PercentAreas[1], 1);

            PercentAreaEritrodiol = (ListAreas[1] * 100) / (ListAreas[1] + ListAreas[16]);
            EritrodiolAbsoluto = Math.Round((100 - PercentAreaEritrodiol) * 200 / PercentAreaEritrodiol, 1);

            PercentAreaUvaol = (ListAreas[1] * 100) / (ListAreas[1] + ListAreas[17]);
            UvaolAbsoluto = Math.Round((100 - PercentAreaUvaol) * 200 / PercentAreaUvaol, 1);

            output += $"Análisis {Path.GetFileNameWithoutExtension(path)}\n";
            output += $"\tColesterol:\t {Colesterol} %\n";
            output += $"\tBrasicasterol:\t {Brasicasterol} %\n";
            output += $"\tCampesterol:\t {Campesterol} %\n";
            output += $"\tEstigmasterol:\t {Estigmasterol} %\n";
            output += $"\tβ-Sitosterol:\t {βSitosterol} %\n";
            output += $"\tδ7-Estigmastenol:\t {δ7Estigmastenol} %\n";
            output += $"\tEritrodiol + Uvaol:\t {EritrodiolPlusUvaol} %\n";
            output += $"\tEsteroles abs:\t {Patron} mg/Kg \n";
            output += $"\tEritrodiol abs:\t {EritrodiolAbsoluto} mg/Kg \n";
            output += $"\tUvaol abs:\t {UvaolAbsoluto} mg/Kg \n";
            output += $"\tEritrodiol + Uvaol abs:\t {EritrodiolAbsoluto + UvaolAbsoluto} mg/Kg \n";
            return output;
        }

        public double Colesterol { get; set; }
        public double Brasicasterol { get; set; }
        public double Campesterol { get; set; }
        public double Estigmasterol { get; set; }
        public double βSitosterol { get; set; }
        public double δ7Estigmastenol { get; set; }
        public double EritrodiolPlusUvaol {get; set;}
        public double Patron { get; set; }
        public double EritrodiolAbsoluto { get; set; }
        public double UvaolAbsoluto { get; set; }
    }
}
