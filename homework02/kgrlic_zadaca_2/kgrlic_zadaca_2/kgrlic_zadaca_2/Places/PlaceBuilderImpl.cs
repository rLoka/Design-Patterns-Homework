using System.Collections.Generic;
using kgrlic_zadaca_2.Devices;

namespace kgrlic_zadaca_2.Places
{
    class PlaceBuilderImpl : IPlaceBuilder
    {
        private Place _place = new Place();

        public Place Build()
        {
            Place builtPlace = _place;
            _place = new Place();
            return builtPlace;
        }

        public IPlaceBuilder SetUniqueIdentifier(int uniqueIdentifier)
        {
            _place.UniqueIdentifier = uniqueIdentifier;
            return this;
        }

        public IPlaceBuilder SetName(string name)
        {
            _place.Name = name;
            return this;
        }

        public IPlaceBuilder SetType(int? type)
        {
            _place.Type = type;
            return this;
        }

        public IPlaceBuilder SetNumberOfSensors(int? numberOfSensors)
        {
            _place.NumberOfSensors = numberOfSensors;
            return this;
        }

        public IPlaceBuilder SetNumberOfActuators(int? numberOfActuators)
        {
            _place.NumberOfActuators = numberOfActuators;
            return this;
        }

        public IPlaceBuilder SetDevices(List<Device> devices)
        {
            _place.Devices = devices;
            return this;
        }
    }
}
