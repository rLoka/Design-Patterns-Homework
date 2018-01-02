namespace kgrlic_zadaca_3.Configurations
{
    interface IConfigurationBuilder
    {
        Configuration Build();

        IConfigurationBuilder SetNumberOfRows(int? numberOfRows);
        IConfigurationBuilder SetNumberOfColumns(int? numberOfColumns);
        IConfigurationBuilder SetNumberOfInputRows(int? numberOfInputRows);
        IConfigurationBuilder SetAverageDeviceValidity(int? averageDeviceValidity);
        IConfigurationBuilder SetGeneratorSeed(int? generatorSeed);
        IConfigurationBuilder SetThreadCycleDuration(int? threadCycleDuration);
        IConfigurationBuilder SetPlaceFilePath(string placeFilePath);
        IConfigurationBuilder SetSensorsFilePath(string sensorsFilePath);
        IConfigurationBuilder SetActuatorsFilePath(string actuatorsFilePath);
        IConfigurationBuilder SetScheduleFilePath(string scheduleFilePath);
    }
}
