using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspejoAnalysis.Model
{
    public class EsterolesResult
    {
        public string Name { get; set; }
        public double Colesterol { get; set; }
        public double Brasicasterol { get; set; }
        public double Campesterol { get; set; }
        public double Estigmasterol { get; set; }
        public double βSitosterol { get; set; }
        public double δ7Estigmastenol { get; set; }
        public double EritrodiolPlusUvaol { get; set; }
        public double Patron { get; set; }
        public double EritrodiolAbsoluto { get; set; }
        public double UvaolAbsoluto { get; set; }

        public override string ToString()
        {
            string output = "";
            output += $"Análisis {Name}\n";
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
    }
}
