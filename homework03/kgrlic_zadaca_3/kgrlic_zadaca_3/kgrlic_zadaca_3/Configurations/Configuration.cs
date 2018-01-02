using System.IO;

namespace kgrlic_zadaca_3.Configurations
{
    class Configuration
    {
        public int? NumberOfRows;
        public int? NumberOfColumns;
        public int? NumberOfInputRows;
        public int? NumberOfDisplayRows => NumberOfRows - NumberOfInputRows;

        public double? AverageDeviceValidity;
        public int? GeneratorSeed;
        public int? ThreadCycleDuration;

        public string ScheduleFilePath;
        public string PlaceFilePath;
        public string SensorsFilePath;
        public string ActuatorsFilePath;

        public bool IsConfigurationValid()
        {
            if (GeneratorSeed == null 
                || ThreadCycleDuration == null 
                || NumberOfRows == null
                || NumberOfColumns == null
                || NumberOfInputRows == null
                || AverageDeviceValidity == null
                || !File.Exists(ScheduleFilePath)
                || !File.Exists(ActuatorsFilePath) 
                || !File.Exists(PlaceFilePath)
                || !File.Exists(SensorsFilePath)
                )
            {
                return false;
            }

            return true;
        }

    }
}
