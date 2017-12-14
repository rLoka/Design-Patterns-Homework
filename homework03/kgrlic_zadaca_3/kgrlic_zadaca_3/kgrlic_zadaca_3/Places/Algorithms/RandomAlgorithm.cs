using kgrlic_zadaca_3.Places.Iterator;

namespace kgrlic_zadaca_3.Places.Algorithms
{
    class RandomAlgorithm : Algorithm
    {
        public RandomAlgorithm(Foi foi) : base(foi) { }

        public override void Run(int threadCycleDuration)
        {
            IIterator randomIterator = Foi.Places.CreateIterator(IteratorType.Random);

            Place place = randomIterator.First();

            while (place != null)
            {
                CheckPlace(place, threadCycleDuration);
                place = randomIterator.Next();
            }
        }
    }
}
