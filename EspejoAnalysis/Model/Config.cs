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

        public string LastAnalysis { get; set; }
    }
}
