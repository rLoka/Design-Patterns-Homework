using System.Linq;
using kgrlic_zadaca_2.Places;

namespace kgrlic_zadaca_2.Algorithms
{
    class NumericAlgorithm : Algorithm
    {
        public NumericAlgorithm(FOI foi) : base(foi) { }

        public override void OrderPlaces()
        {
            OrderedPlaces = Foi.Places.OrderBy(pla => pla.Type).ToList();
        }
    }
}
