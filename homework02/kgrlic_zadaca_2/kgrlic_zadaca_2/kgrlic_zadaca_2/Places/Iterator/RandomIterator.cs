using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_2.IO;

namespace kgrlic_zadaca_2.Places.Iterator
{
    class RandomIterator : IIterator
    {
        private readonly Aggregate _aggregate;
        private RandomGeneratorFacade _randomGeneratorFacade = new RandomGeneratorFacade();
        private List<int> _usedIndexes = new List<int>();

        private int _currentIndex => _usedIndexes.Last();

        private int _nextIndex
        {
            get
            {
                int[] possibleIndexes = Enumerable.Range(0, _aggregate.Count).ToArray();
                int[] unusedIndexes = possibleIndexes.Except(_usedIndexes).ToArray();
                int randomIndex = _randomGeneratorFacade.GiveRandomNumber(0, _aggregate.Count - 1, unusedIndexes, 1);

                _usedIndexes.Add(randomIndex);
                return randomIndex;
            }
        }

        public RandomIterator(Aggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public Place First()
        {
            if (_usedIndexes.Count == 0)
            {
                _usedIndexes.Add(_randomGeneratorFacade.GiveRandomNumber(0, _aggregate.Count - 1));
            }
            
            return _aggregate[0];
        }

        public Place Next()
        {
            if (_usedIndexes.Count < _aggregate.Count)
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
            return _usedIndexes.Count >= _aggregate.Count;
        }
    }
}
