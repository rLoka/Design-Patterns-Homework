using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.Application.Entities.Devices;

namespace kgrlic_zadaca_3.Application.Entities.Places
{
    partial class Foi
    {
        private static Foi _foi;

        private Foi() { }

        public static Foi GetInstance()
        {
            return _foi ?? (_foi = new Foi());
        }

        public List<Place> Places = new List<Place>();

        public IEnumerable<Device> Devices
        {
            get
            {
                IEnumerable<Device> devices = new List<Device>();

                foreach (var place in Places)
                {
                    devices = devices.Concat(place.Devices);
                }

                return devices;
            }
        }

        public void AddPlace(Place place)
        {
            Places.Add(place);
        }
    }
}
