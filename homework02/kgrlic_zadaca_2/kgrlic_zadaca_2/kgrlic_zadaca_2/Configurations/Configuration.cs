﻿using System.IO;
using System.Linq;

namespace kgrlic_zadaca_2.Configurations
{
    class Configuration
    {
        public int? GeneratorSeed;
        public int? ThreadCycleDuration;
        public int? NumberOfThreadCycles;
        public int? NumberOfLines;

        public string PlaceFilePath;
        public string SensorsFilePath;
        public string ActuatorsFilePath;
        public string OutputFilePath;
        public string Algorithm;

        public bool IsConfigurationValid()
        {
            if (GeneratorSeed == null 
                || ThreadCycleDuration == null 
                || NumberOfThreadCycles == null
                || NumberOfLines == null
                || NumberOfThreadCycles < 1 
                || !File.Exists(ActuatorsFilePath) 
                || !File.Exists(PlaceFilePath)
                || !File.Exists(SensorsFilePath)
                || !(new string[] { "AscendingIdentifierAlgorithm", "DescendingIdentifierAlgorithm", "RandomAlgorithm", "SequentialAlgorithm" }).Contains(Algorithm))
            {
                return false;
            }

            return true;
        }

    }
}
