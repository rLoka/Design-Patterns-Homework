namespace kgrlic_zadaca_3.Places
{
    interface IPlaceBuilder
    {
        Place Build();
        IPlaceBuilder SetUniqueIdentifier(int? uniqueIdentifier);
        IPlaceBuilder SetName(string name);
        IPlaceBuilder SetType(int? type);
        IPlaceBuilder SetNumberOfSensors(int? numberOfSensors);
        IPlaceBuilder SetNumberOfActuators(int? numberOfActuators);
    }
}