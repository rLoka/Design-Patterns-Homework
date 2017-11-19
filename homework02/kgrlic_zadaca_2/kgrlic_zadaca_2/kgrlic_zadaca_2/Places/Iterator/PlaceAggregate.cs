namespace kgrlic_zadaca_2.Places.Iterator
{
    class PlaceAggregate : Aggregate
    {
        public override IIterator CreateIterator(IteratorType iteratorType)
        {
            switch (iteratorType)
            {
                case IteratorType.Sequential:
                    return new SequentialIterator(this);
                case IteratorType.AscendingValue:
                    return new AscendingIdentifierIterator(this);
                case IteratorType.DescendingValue:
                    return new DescendingIdentifierIterator(this);
                case IteratorType.Random:
                    return new RandomIterator(this);
                default:
                    return new SequentialIterator(this);
            }
        }
    }
}

