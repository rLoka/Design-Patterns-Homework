namespace kgrlic_zadaca_2.Configurations
{

    class ConfigurationBuilderImpl : IConfigurationBuilder
    {
        private readonly Configuration _configuration;

        public ConfigurationBuilderImpl()
        {
            _configuration = new Configuration();
        }

        public Configuration Build()
        {
            return _configuration;
        }

        public IConfigurationBuilder SetSetGeneratorSeed(int? generatorSeed)
        {
            _configuration.GeneratorSeed = generatorSeed;
            return this;
        }

        public IConfigurationBuilder SetThreadCycleDuration(int? threadCycleDuration)
        {
            _configuration.ThreadCycleDuration = threadCycleDuration;
            return this;
        }

        public IConfigurationBuilder SetNumberOfThreadCycles(int? numberOfThreadCycles)
        {
            _configuration.NumberOfThreadCycles = numberOfThreadCycles;
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
