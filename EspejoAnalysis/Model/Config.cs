using System.Collections.Generic;
using System.Xml.Serialization;

namespace EspejoAnalysis.Model
{
    public class Config
    {
        public Config()
        {
            HistoricoDirectorios = new List<string>();
        }

        [XmlArray("HistoricoDirectorios")]
        [XmlArrayItem("Directorio")]
        public List<string> HistoricoDirectorios { get; set; }
    }
}
