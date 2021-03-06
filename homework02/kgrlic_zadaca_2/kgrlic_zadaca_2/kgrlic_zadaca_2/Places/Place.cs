﻿using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_2.Devices;

namespace kgrlic_zadaca_2.Places
{
    partial class Place
    {
        public string Name;
        public int? Type;
        public int? NumberOfSensors;
        public int? NumberOfActuators;
        public int UniqueIdentifier;

        public List<Device> Devices;

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
                "----------------------------- ~ MJESTO ~ --------------------------------\r\n"
                + "\t{ naziv: " + Name
                + ", tip: " + Type
                + ", broj senzora: " + NumberOfSensors
                + ", Broj aktuatora: " + NumberOfActuators
                + " }\r\n";
        }

    }
}
