namespace kgrlic_zadaca_2.Places.Iterator
{
    interface IIterator
    {
        Place CurrentItem();
        Place First();
        Place Next();
        bool IsDone();
    }
}
