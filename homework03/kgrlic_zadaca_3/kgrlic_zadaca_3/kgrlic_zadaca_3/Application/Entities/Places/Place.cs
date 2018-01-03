using System.Collections.Generic;
using kgrlic_zadaca_3.Application.Entities.Devices;

namespace kgrlic_zadaca_3.Application.Entities.Places
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
                "MJESTO = "
                + "{ ID: " + UniqueIdentifier
                + ", naziv: " + Name
                + ", tip: " + Type
                + ", broj senzora: " + NumberOfSensors
                + ", Broj aktuatora: " + NumberOfActuators
                + " }";
        }

        public Place Clone()
        {
            Place place = new Place();
            place.Name = Name;
            place.Type = Type;
            place.NumberOfSensors = NumberOfSensors;
            place.NumberOfActuators = NumberOfActuators;
            place.UniqueIdentifier = UniqueIdentifier;

            foreach (var device in Devices)
            {
                Device deviceClone = device.DeepClone();
                place.Devices.Add(deviceClone);
            }

            foreach (var device in place.Devices)
            {
                Device originalDevice = Devices.Find(d => d.UniqueIdentifier == device.UniqueIdentifier);

                if (device.DeviceType == DeviceType.Sensor)
                {
                    foreach (var parent in originalDevice.GetParents())
                    {
                        device.AddParent(parent);
                    }
                }
                else
                {
                    foreach (var child in originalDevice.GetChildren())
                    {
                        device.AddChild(child);
                    }
                }
            }

            return place;
        }

    }
}
