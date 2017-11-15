using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_2.IO;
using kgrlic_zadaca_2.Places;

namespace kgrlic_zadaca_2.Algorithms
{
    class RandomAlgorithm : Algorithm
    {
        public RandomAlgorithm(FOI foi) : base(foi) { }

        public override void OrderPlaces()
        {
            RandomGenerator randomGenerator = RandomGenerator.GetInstance();
            List<Place> tempList = Foi.Places.ToList();
            OrderedPlaces = new List<Place>();

            while (tempList.Count > 0)
            {
                Place randomPlace = tempList[randomGenerator.GetRandomInteger(0, tempList.Count)];
                OrderedPlaces.Add(randomPlace);

                tempList.Remove(randomPlace);
            }
        }
    }
}
