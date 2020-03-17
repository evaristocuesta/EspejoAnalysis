using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EspejoAnalysis.Model
{
    public class EsterolesLogic
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EsterolesLogic()
        {
            ListAreas = new List<double>();
            PercentAreas = new List<double>();
            Result = new EsterolesResult();
        }

        public EsterolesResult Result { get; set; }

        public List<double> ListAreas { get; set; }

        public List<double> PercentAreas { get; set; }

        public double Area { get; set; }

        public double Area1_16 { get; set; }

        public double Area1_16Without2 { get; set; }

        public double Area1_18Without2 { get; set; }

        public double PercentAreaEritrodiol { get; set; }

        public double PercentAreaUvaol { get; set; }

        public EsterolesResult Calculate(string path)
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

            Result.Name = Path.GetFileNameWithoutExtension(path);
            Result.Colesterol = Math.Round(PercentAreas[0], 1);
            Result.Brasicasterol = Math.Round(PercentAreas[2], 1);
            Result.Campesterol = Math.Round(PercentAreas[4], 1);
            Result.Estigmasterol = Math.Round(PercentAreas[6], 1);
            Result.βSitosterol = Math.Round(PercentAreas[8] + PercentAreas[9] + PercentAreas[10] + PercentAreas[11] + PercentAreas[12] + PercentAreas[13], 1);
            Result.δ7Estigmastenol = Math.Round(PercentAreas[14], 1);
            Result.EritrodiolPlusUvaol = Math.Round(PercentAreas[16] + PercentAreas[17], 1);
            Result.Patron = Math.Round((100 - PercentAreas[1]) * 200 / PercentAreas[1], 1);

            PercentAreaEritrodiol = (ListAreas[1] * 100) / (ListAreas[1] + ListAreas[16]);
            Result.EritrodiolAbsoluto = Math.Round((100 - PercentAreaEritrodiol) * 200 / PercentAreaEritrodiol, 1);

            PercentAreaUvaol = (ListAreas[1] * 100) / (ListAreas[1] + ListAreas[17]);
            Result.UvaolAbsoluto = Math.Round((100 - PercentAreaUvaol) * 200 / PercentAreaUvaol, 1);

            return Result;
        }
    }
}
