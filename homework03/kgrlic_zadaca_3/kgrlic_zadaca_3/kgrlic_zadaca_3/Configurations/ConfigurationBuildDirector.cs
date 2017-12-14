using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Configurations
{
    class ConfigurationBuildDirector
    {
        private readonly IConfigurationBuilder _builder;

        public ConfigurationBuildDirector (IConfigurationBuilder builder)
        {
            _builder = builder;
        }

        public Configuration Construct(string[] arguments)
        {
            return _builder
                .SetSetGeneratorSeed(Converter.StringToInt(arguments[0]))
                .SetPlaceFilePath(arguments[1])
                .SetSensorsFilePath(arguments[2])
                .SetActuatorsFilePath(arguments[3])
                .SetAlgorithm(arguments[4])
                .SetThreadCycleDuration(Converter.StringToInt(arguments[5]))
                .SetNumberOfThreadCycles(Converter.StringToInt(arguments[6]))
                .SetOutputFilePath(arguments[7])
                .Build();
        }

        public Configuration Construct()
        {
            return _builder.Build();
        }
    }
}
