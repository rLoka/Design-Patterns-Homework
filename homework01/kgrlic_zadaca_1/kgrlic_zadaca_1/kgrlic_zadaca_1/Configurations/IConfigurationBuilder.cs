﻿namespace kgrlic_zadaca_1.Configurations
{
    interface IConfigurationBuilder
    {
        Configuration Build();

        IConfigurationBuilder SetSetGeneratorSeed(int? generatorSeed);
        IConfigurationBuilder SetThreadCycleDuration(int? threadCycleDuration);
        IConfigurationBuilder SetNumberOfThreadCycles(int? numberOfThreadCycles);

        IConfigurationBuilder SetPlaceFilePath(string placeFilePath);
        IConfigurationBuilder SetSensorsFilePath(string sensorsFilePath);
        IConfigurationBuilder SetActuatorsFilePath(string actuatorsFilePath);
        IConfigurationBuilder SetOutputFilePath(string outputFilePath);
        IConfigurationBuilder SetAlgorithm(string algorithm);
    }
}
