using kgrlic_zadaca_1.Places;
using System.Linq;

namespace kgrlic_zadaca_1.Algorithms
{
    class AlphabeticAlgorithm : Algorithm
    {
        public AlphabeticAlgorithm(FOI foi) : base(foi) { }

        public override void OrderPlaces()
        {
            OrderedPlaces = Foi.Places.OrderBy(pla => pla.Name).ToList();
        }
    }
}
