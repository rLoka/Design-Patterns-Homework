using kgrlic_zadaca_3.Places.Iterator;

namespace kgrlic_zadaca_3.Places.Algorithms
{
    class AscendingIdentifierAlgorithm : Algorithm
    {
        public AscendingIdentifierAlgorithm(Foi foi) : base(foi) { }
        public override void Run(int threadCycleDuration)
        {
            IIterator ascendingIterator = Foi.Places.CreateIterator(IteratorType.AscendingValue);

            Place place = ascendingIterator.First();

            while (place != null)
            {
                CheckPlace(place, threadCycleDuration);
                place = ascendingIterator.Next();
            }
        }
    }
}
