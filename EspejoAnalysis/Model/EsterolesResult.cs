namespace EspejoAnalysis.Model
{
    public class EsterolesResult
    {
        public string Name { get; set; }
        public double Colesterol { get; set; }
        public bool ToleranceColesterol { get; set; }
        public double Brasicasterol { get; set; }
        public bool ToleranceBrasicasterol { get; set; }
        public double Campesterol { get; set; }
        public bool ToleranceCampesterol { get; set; }
        public double Estigmasterol { get; set; }
        public bool ToleranceEstigmasterol { get; set; }
        public double βSitosterol { get; set; }
        public bool ToleranceβSitosterol { get; set; }
        public double δ7Estigmastenol { get; set; }
        public bool Toleranceδ7Estigmastenol { get; set; }
        public double EritrodiolPlusUvaol { get; set; }
        public bool ToleranceEritrodiolPlusUvaol { get; set; }
        public double EsterolesAbsoluto { get; set; }
        public bool ToleranceEsterolesAbsoluto { get; set; }
        public double EritrodiolAbsoluto { get; set; }
        public double UvaolAbsoluto { get; set; }
        public double EritrodiolPlusUvaolAbs
        {
            get
            {
                return EritrodiolAbsoluto + UvaolAbsoluto;
            }
        }

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
            output += $"\tEsteroles abs:\t {EsterolesAbsoluto} mg/Kg \n";
            output += $"\tEritrodiol abs:\t {EritrodiolAbsoluto} mg/Kg \n";
            output += $"\tUvaol abs:\t {UvaolAbsoluto} mg/Kg \n";
            output += $"\tEritrodiol + Uvaol abs:\t {EritrodiolPlusUvaolAbs} mg/Kg \n";
            return output;
        }
    }
}
