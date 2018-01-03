using System;
using kgrlic_zadaca_3.Application.Helpers;

namespace kgrlic_zadaca_3.Application.Entities.Configurations
{

    class ConfigurationBuilderImpl : IConfigurationBuilder
    {
        private readonly Configuration _configuration;

        private bool _isNumberOfRows;
        private bool _isNumberOfColumns;
        private bool _isNumberOfInputRows;
        private bool _isAverageDeviceValidity;
        private bool _isGeneratorSeedSet;
        private bool _isThreadCycleDurationSet;

        public ConfigurationBuilderImpl()
        {
            _configuration = new Configuration();
        }

        public Configuration Build()
        {
            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

            if (!_isNumberOfRows)
            {
                SetNumberOfRows(24);
            }
            if (!_isNumberOfColumns)
            {
                SetNumberOfColumns(80);
            }
            if (!_isNumberOfInputRows)
            {
                SetNumberOfInputRows(2);
            }
            if (!_isAverageDeviceValidity)
            {
                SetAverageDeviceValidity(50);
            }
            if (!_isGeneratorSeedSet)
            {
                SetGeneratorSeed((int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
            }
            if (!_isThreadCycleDurationSet)
            {
                SetThreadCycleDuration(randomGeneratorFacade.GiveRandomNumber(1, 17));
            }

            return _configuration;
        }

        public IConfigurationBuilder SetNumberOfRows(int? numberOfRows)
        {
            _isNumberOfRows = true;
            _configuration.NumberOfRows = numberOfRows;
            return this;
        }

        public IConfigurationBuilder SetNumberOfColumns(int? numberOfColumns)
        {
            _isNumberOfColumns = true;
            _configuration.NumberOfColumns = numberOfColumns;
            return this;
        }

        public IConfigurationBuilder SetNumberOfInputRows(int? numberOfInputRows)
        {
            _isNumberOfInputRows = true;
            _configuration.NumberOfInputRows = numberOfInputRows;
            return this;
        }

        public IConfigurationBuilder SetAverageDeviceValidity(int? averageDeviceValidity)
        {
            _isAverageDeviceValidity = true;
            _configuration.AverageDeviceValidity = averageDeviceValidity;
            return this;
        }

        public IConfigurationBuilder SetGeneratorSeed(int? generatorSeed)
        {
            _isGeneratorSeedSet = true;
            _configuration.GeneratorSeed = generatorSeed;
            return this;
        }

        public IConfigurationBuilder SetThreadCycleDuration(int? threadCycleDuration)
        {
            _isThreadCycleDurationSet = true;
            _configuration.ThreadCycleDuration = threadCycleDuration;
            return this;
        }

        public IConfigurationBuilder SetPlaceFilePath(string placeFilePath)
        {
            _configuration.PlaceFilePath = placeFilePath;
            return this;
        }

        public IConfigurationBuilder SetScheduleFilePath(string scheduleFilePath)
        {
            _configuration.ScheduleFilePath = scheduleFilePath;
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
    }
}