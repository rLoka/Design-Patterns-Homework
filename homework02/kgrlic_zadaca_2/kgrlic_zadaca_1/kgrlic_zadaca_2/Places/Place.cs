using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_2.Devices;

namespace kgrlic_zadaca_2.Places
{
    class Place
    {
        public string Name;
        public int? Type;
        public int? NumberOfSensors;
        public int? NumberOfActuators;

        public List<Device> Sensors;
        public List<Device> Actuators;

        public IEnumerable<Device> Devices => Sensors.Concat(Actuators);

        public Place() { }

        public bool IsPlaceValid()
        {
            if (Name.Length < 1 || Type == null || NumberOfSensors == null || NumberOfActuators == null)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return
                "\r\n----------------------------- ~ MJESTO ~ --------------------------------\r\n"
                + "{ naziv: " + Name
                + ", tip: " + Type
                + ", broj senzora: " + NumberOfSensors
                + ", Broj aktuatora: " + NumberOfActuators
                + " }";
        }

    }
}
