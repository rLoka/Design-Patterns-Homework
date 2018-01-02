using System.Collections.Generic;
using kgrlic_zadaca_3.Devices;

namespace kgrlic_zadaca_3.Places
{
    class Place
    {
        public string Name;
        public int? Type;
        public int? NumberOfSensors;
        public int? NumberOfActuators;
        public int? UniqueIdentifier;

        public List<Device> Devices = new List<Device>();

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
                "----------------------------- ~ MJESTO ~ --------------------------------\r\n"
                + "\t{ ID: " + UniqueIdentifier
                + ", naziv: " + Name
                + ", tip: " + Type
                + ", broj senzora: " + NumberOfSensors
                + ", Broj aktuatora: " + NumberOfActuators
                + " }\r\n";
        }

    }
}
