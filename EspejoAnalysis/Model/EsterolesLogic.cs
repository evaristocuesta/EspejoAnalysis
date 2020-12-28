using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Abstractions;
using System.Linq;

namespace EspejoAnalysis.Model
{
    public class EsterolesLogic : IEsterolesLogic
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFileSystem _fileSystem;

        public EsterolesLogic(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public EsterolesResult Calculate(string path)
        {
            EsterolesResult Result = new EsterolesResult();
            List<double> ListAreas = new List<double>();
            List<double> PercentAreas = new List<double>();
            double Area;
            double Area1_16;
            double Area1_16Without2;
            double Area1_18Without2;
            double PercentAreaEritrodiol;
            double PercentAreaUvaol;

            log.Info("-----------------------------------------");
            log.Info($"Calculate path {path}");
            string[] split = new string[3];
            IEnumerable<string> filas = _fileSystem.File.ReadLines(path);
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
                    if (split.Length != 3)
                    {
                        log.Error("La fila no tiene el formato correcto");
                        throw new Exception($"El archivo {path} no tiene el formato correcto\n");
                    }
                    if (double.TryParse(split[2], NumberStyles.Float, new CultureInfo("en-US"), out double numero))
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

            Result.Name = _fileSystem.Path.GetFileNameWithoutExtension(path);
            Result.Colesterol = PercentAreas[0];
            Result.ToleranceColesterol = InTolerance(Result.Colesterol, double.MinValue, 0.5);
            Result.Brasicasterol = PercentAreas[2];
            Result.ToleranceBrasicasterol = InTolerance(Result.Brasicasterol, double.MinValue, 0.2);
            Result.Campesterol = PercentAreas[4];
            Result.ToleranceCampesterol = InTolerance(Result.Campesterol, double.MinValue, 4);
            Result.Estigmasterol = PercentAreas[6];
            Result.ToleranceEstigmasterol = Result.Estigmasterol <= Result.Campesterol;
            Result.βSitosterol = PercentAreas[8] + PercentAreas[9] + PercentAreas[10] + PercentAreas[11] + PercentAreas[12] + PercentAreas[13];
            Result.ToleranceβSitosterol = InTolerance(Result.βSitosterol, 93, double.MaxValue);
            Result.δ7Estigmastenol = PercentAreas[14];
            Result.Toleranceδ7Estigmastenol = InTolerance(Result.δ7Estigmastenol, double.MinValue, 0.5);
            Result.EritrodiolPlusUvaol = PercentAreas[16] + PercentAreas[17];
            Result.ToleranceEritrodiolPlusUvaol = InTolerance(Result.EritrodiolPlusUvaol, double.MinValue, 4.5);
            Result.EsterolesAbsoluto = (100 - PercentAreas[1]) * 200 / PercentAreas[1];
            Result.ToleranceEsterolesAbsoluto = InTolerance(Result.EsterolesAbsoluto, 1000, double.MaxValue);

            PercentAreaEritrodiol = (ListAreas[1] * 100) / (ListAreas[1] + ListAreas[16]);
            Result.EritrodiolAbsoluto = (100 - PercentAreaEritrodiol) * 200 / PercentAreaEritrodiol;

            PercentAreaUvaol = (ListAreas[1] * 100) / (ListAreas[1] + ListAreas[17]);
            Result.UvaolAbsoluto = (100 - PercentAreaUvaol) * 200 / PercentAreaUvaol;

            return Result;
        }

        private bool InTolerance(double value, double min, double max)
        {
            return value >= min && value <= max;
        }
    }
}
