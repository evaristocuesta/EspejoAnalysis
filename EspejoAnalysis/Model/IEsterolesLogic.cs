using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspejoAnalysis.Model
{
    public interface IEsterolesLogic
    {
        EsterolesResult Calculate(string path);
    }
}
