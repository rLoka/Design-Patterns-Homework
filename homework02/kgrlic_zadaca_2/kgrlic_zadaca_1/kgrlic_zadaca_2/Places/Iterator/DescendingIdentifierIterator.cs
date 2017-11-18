using System.Collections.Generic;

namespace kgrlic_zadaca_2.Places.Iterator
{
    class DescendingIdentifierIterator : IIterator
    {
        private readonly Aggregate _aggregate;
        private int _currentIndex;
        private int _nextIndex
        {
            get
            {
                List<int> tempIndexList = new List<int>();

                for (int i = 0; i < _aggregate.Count; i++)
                {
                    if (_aggregate[i].UniqueIdentifier < _aggregate[_currentIndex].UniqueIdentifier && i != _currentIndex)
                    {
                        tempIndexList.Add(i);
                    }
                }

                int maxIndex = tempIndexList[0];

                for (int i = 0; i < tempIndexList.Count; i++)
                {
                    if (_aggregate[tempIndexList[i]].UniqueIdentifier >= _aggregate[maxIndex].UniqueIdentifier)
                    {
                        maxIndex = tempIndexList[i];
                    }
                }

                _currentIndex = maxIndex;
                return _currentIndex;
            }
        }

        public DescendingIdentifierIterator(Aggregate aggregate)
        {
            _aggregate = aggregate;
            _currentIndex = 0;
        }

        public Place First()
        {
            int minimalValue = _aggregate[0].UniqueIdentifier;

            for (int i = 0; i < _aggregate.Count; i++)
            {
                if (_aggregate[i].UniqueIdentifier < minimalValue)
                {
                    _currentIndex = i;
                }
            }

            return _aggregate[_currentIndex];
        }

        public Place Next()
        {
            if (!IsDone())
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
            int minimumValue = _aggregate[_currentIndex].UniqueIdentifier;

            for (int i = 0; i < _aggregate.Count; i++)
            {
                if (_aggregate[i].UniqueIdentifier < minimumValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
