using System.Collections;

namespace kgrlic_zadaca_3.Places.Iterator
{
    enum IteratorType
    {
        Sequential,
        AscendingValue,
        DescendingValue,
        Random
    }

    abstract class Aggregate
    {
        private readonly ArrayList _itemArray = new ArrayList();

        public int Count => _itemArray.Count;

        public Place this[int index]
        {
            get
            {
                if (_itemArray.Count == 0 || index < 0 || index >= _itemArray.Count)
                {
                    return null;
                }

                return (Place)_itemArray[index];
            }

            set
            {
                _itemArray.Insert(index, value);
            }
        }

        public abstract IIterator CreateIterator(IteratorType iteratorType);
    }
}
