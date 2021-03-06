﻿namespace kgrlic_zadaca_3.Application.Entities.Configurations
{
    class ConfigurationBuildDirector
    {
        private readonly IConfigurationBuilder _builder;

        public ConfigurationBuildDirector (IConfigurationBuilder builder)
        {
            _builder = builder;
        }

        public Configuration Construct()
        {
            return _builder.Build();
        }
    }
}
