using kgrlic_zadaca_3.Places.Iterator;

namespace kgrlic_zadaca_3.Places.Algorithms
{
    class SequentialAlgorithm : Algorithm
    {
        public SequentialAlgorithm(Foi foi) : base(foi) { }

        public override void Run(int threadCycleDuration)
        {
            IIterator sequentialIterator = Foi.Places.CreateIterator(IteratorType.Sequential);

            Place place = sequentialIterator.First();

            while (place != null)
            {
                CheckPlace(place, threadCycleDuration);
                place = sequentialIterator.Next();
            }
        }
    }
}
