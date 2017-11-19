using System.Collections.Generic;
using kgrlic_zadaca_2.Devices;

namespace kgrlic_zadaca_2.Places
{
    interface IPlaceBuilder
    {
        Place Build();

        IPlaceBuilder SetUniqueIdentifier(int uniqueIdentifier);
        IPlaceBuilder SetName(string name);
        IPlaceBuilder SetType(int? type);
        IPlaceBuilder SetNumberOfSensors(int? numberOfSensors);
        IPlaceBuilder SetNumberOfActuators(int? numberOfActuators);

        IPlaceBuilder SetDevices(List<Device> devices);
    }
}