using System;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Configurations
{

    class ConfigurationBuilderImpl : IConfigurationBuilder
    {
        private readonly Configuration _configuration;

        public bool IsGeneratorSeedSet = false;
        public bool IsThreadCycleDurationSet = false;
        public bool IsNumberOfThreadCyclesSet = false;
        public bool IsOutputFilePathSet = false;
        public bool IsNumberOfLinesSet = false;

        public ConfigurationBuilderImpl()
        {
            _configuration = new Configuration();
        }

        public Configuration Build()
        {
            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

            if (!IsGeneratorSeedSet)
            {
                SetSetGeneratorSeed(randomGeneratorFacade.GiveRandomNumber(100, 65535));
            }
            if (!IsThreadCycleDurationSet)
            {
                SetThreadCycleDuration(randomGeneratorFacade.GiveRandomNumber(1, 17));
            }
            if (!IsNumberOfThreadCyclesSet)
            {
                SetNumberOfThreadCycles(randomGeneratorFacade.GiveRandomNumber(1, 23));
            }
            if (!IsOutputFilePathSet)
            {
                SetOutputFilePath("kgrlic_" + DateTime.Now.ToString("yyyymmdd_HHmmss") + ".txt");
            }
            if (!IsNumberOfLinesSet)
            {
                SetNumberOfLines(randomGeneratorFacade.GiveRandomNumber(100, 999));
            }

            return _configuration;
        }

        public IConfigurationBuilder SetSetGeneratorSeed(int? generatorSeed)
        {
            IsGeneratorSeedSet = true;
            _configuration.GeneratorSeed = generatorSeed;
            return this;
        }

        public IConfigurationBuilder SetThreadCycleDuration(int? threadCycleDuration)
        {
            IsThreadCycleDurationSet = true;
            _configuration.ThreadCycleDuration = threadCycleDuration;
            return this;
        }

        public IConfigurationBuilder SetNumberOfThreadCycles(int? numberOfThreadCycles)
        {
            IsNumberOfThreadCyclesSet = true;
            _configuration.NumberOfThreadCycles = numberOfThreadCycles;
            return this;
        }

        public IConfigurationBuilder SetNumberOfLines(int? numberOfLines)
        {
            IsNumberOfLinesSet = true;
            _configuration.NumberOfLines = numberOfLines;
            return this;
        }

        public IConfigurationBuilder SetPlaceFilePath(string placeFilePath)
        {
            _configuration.PlaceFilePath = placeFilePath;
            return this;
        }

        public IConfigurationBuilder SetSensorsFilePath(string sensorsFilePath)
        {
            _configuration.SensorsFilePath = sensorsFilePath;
            return this;
        }

        public IConfigurationBuilder SetActuatorsFilePath(string actuatorsFilePath)
        {
            _configuration.ActuatorsFilePath = actuatorsFilePath;
            return this;
        }

        public IConfigurationBuilder SetOutputFilePath(string outputFilePath)
        {
            IsOutputFilePathSet = true;
            _configuration.OutputFilePath = outputFilePath;
            return this;
        }

        public IConfigurationBuilder SetAlgorithm(string algorithm)
        {
            _configuration.Algorithm = algorithm;
            return this;
        }
    }
}