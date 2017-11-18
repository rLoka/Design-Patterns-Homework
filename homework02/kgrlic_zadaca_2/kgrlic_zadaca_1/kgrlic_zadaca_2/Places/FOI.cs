using kgrlic_zadaca_2.Places.Iterator;

namespace kgrlic_zadaca_2.Places
{
    class Foi
    {
        public Aggregate Places = new PlaceAggregate();

        public void AddPlace(Place place)
        {
            Places[Places.Count] = place;
        }
    }
}
