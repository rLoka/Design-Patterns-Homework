using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Places
{
    class FoiMemento
    {
        public FoiMemento(List<Place> places)
        {
            Places = places;
        }

        public List<Place> Places { get; }
    }
}
