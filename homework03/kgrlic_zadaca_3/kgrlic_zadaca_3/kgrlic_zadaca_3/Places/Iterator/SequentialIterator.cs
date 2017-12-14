namespace kgrlic_zadaca_3.Places.Iterator
{
    class SequentialIterator : IIterator
    {
        private readonly Aggregate _aggregate;
        private int _currentIndex;
        private int _nextIndex => ++_currentIndex;

        public SequentialIterator(Aggregate aggregate)
        {
            _aggregate = aggregate;
            _currentIndex = 0;
        }

        public Place First()
        {
            return _aggregate[0];
        }

        public Place Next()
        {
            if (_currentIndex < _aggregate.Count - 1)
            {
               return _aggregate[_nextIndex];
            }

            return null;
        }

        public Place CurrentItem()
        {
            return _aggregate[_currentIndex];
        }

        public bool IsDone()
        {
            return _currentIndex >= _aggregate.Count - 1;
        }
    }
}
