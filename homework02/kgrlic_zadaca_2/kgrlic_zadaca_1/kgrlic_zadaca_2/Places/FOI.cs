using System.Collections.Generic;

namespace kgrlic_zadaca_2.Places
{
    class FOI
    {
        public List<Place> Places = new List<Place>();

        public void AddPlace(Place place)
        {
            Places.Add(place);
        }
    }
}
