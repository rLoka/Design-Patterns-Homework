using kgrlic_zadaca_2.Places.Iterator;

namespace kgrlic_zadaca_2.Places.Algorithms
{
    class DescendingIdentifierAlgorithm : Algorithm
    {
        public DescendingIdentifierAlgorithm(Foi foi) : base(foi) { }

        public override void Run(int threadCycleDuration)
        {
            IIterator descendingIterator = Foi.Places.CreateIterator(IteratorType.DescendingValue);

            Place place = descendingIterator.First();

            while (place != null)
            {
                CheckPlace(place, threadCycleDuration);
                place = descendingIterator.Next();
            }
        }
    }
}
