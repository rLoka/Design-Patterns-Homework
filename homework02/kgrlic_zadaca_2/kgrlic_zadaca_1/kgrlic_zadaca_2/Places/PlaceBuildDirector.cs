using System.Collections.Generic;
using kgrlic_zadaca_2.Devices;
using kgrlic_zadaca_2.IO;

namespace kgrlic_zadaca_2.Places
{
    class PlaceBuildDirector
    {
        private readonly List<string> _builtPlacesList = new List<string>();

        private readonly IPlaceBuilder _builder;

        public PlaceBuildDirector(IPlaceBuilder builder)
        {
            _builder = builder;
        }

        public Place Construct(Dictionary<string, string> placeParams, ThingsOfFoi thingsOfFoi)
        {
            if (_builtPlacesList.Contains(placeParams["naziv"]))
            {
                return null;
            }

            _builtPlacesList.Add(placeParams["naziv"]);

            return _builder
                .SetName(placeParams["naziv"])
                .SetType(Converter.StringToInt(placeParams["tip"]))
                .SetNumberOfSensors(Converter.StringToInt(placeParams["broj senzora"]))
                .SetNumberOfActuators(Converter.StringToInt(placeParams["broj aktuatora"]))
                .SetSensors(GetRandomSensors(Converter.StringToInt(placeParams["broj senzora"]), thingsOfFoi.Sensors.FindAll(sen => sen.Type == Converter.StringToInt(placeParams["tip"]) || sen.Type == 2)))
                .SetActuators(GetRandomActuators(Converter.StringToInt(placeParams["broj aktuatora"]), thingsOfFoi.Actuators.FindAll(act => act.Type == Converter.StringToInt(placeParams["tip"]) || act.Type == 2)))
                .Build();
        }

        private List<Device> GetRandomSensors(int? numberOfSensors, List<Device> availableSensors)
        {
            RandomGenerator randomGenerator = RandomGenerator.GetInstance();
            List<Device> placeSensors = new List<Device>();
            for (int i = 0; i < numberOfSensors; i++)
            {
                Device randomSensor = availableSensors[randomGenerator.GetRandomInteger(0, availableSensors.Count)];
                placeSensors.Add(randomSensor.Clone());
            }

            return placeSensors;
        }

        private List<Device> GetRandomActuators(int? numberOfActuators, List<Device> availableActuators)
        {
            RandomGenerator randomGenerator = RandomGenerator.GetInstance();
            List<Device> placeActuators = new List<Device>();
            for (int i = 0; i < numberOfActuators; i++)
            {
                Device randomActuator = availableActuators[randomGenerator.GetRandomInteger(0, availableActuators.Count)];
                placeActuators.Add(randomActuator.Clone());
            }

            return placeActuators;
        }
    }
}
