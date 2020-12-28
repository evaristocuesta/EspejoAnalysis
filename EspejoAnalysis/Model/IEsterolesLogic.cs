using System.Collections.ObjectModel;

namespace EspejoAnalysis.Model
{
    public interface IEsterolesLogic
    {
        EsterolesResult Calculate(string path);

        void Export(string path, ObservableCollection<EsterolesResult> results);
    }
}
