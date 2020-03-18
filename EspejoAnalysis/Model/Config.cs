using System.Collections.Generic;
using System.Xml.Serialization;

namespace EspejoAnalysis.Model
{
    public class Config
    {
        public Config()
        {
            Esteroles = new ConfigEsteroles();
        }

        [XmlElement("Esteroles")]
        public ConfigEsteroles Esteroles { get; set; }
    }
}
