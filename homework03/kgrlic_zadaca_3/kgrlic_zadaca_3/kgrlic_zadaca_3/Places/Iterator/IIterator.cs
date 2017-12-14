namespace kgrlic_zadaca_3.Places.Iterator
{
    interface IIterator
    {
        Place CurrentItem();
        Place First();
        Place Next();
        bool IsDone();
    }
}
