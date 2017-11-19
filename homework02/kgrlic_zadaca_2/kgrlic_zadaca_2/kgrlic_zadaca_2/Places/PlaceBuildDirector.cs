using System.Collections.Generic;
using kgrlic_zadaca_2.Devices;
using kgrlic_zadaca_2.IO;
using kgrlic_zadaca_2.Places.Iterator;

namespace kgrlic_zadaca_2.Places
{
    class PlaceBuildDirector
    {
        private readonly IPlaceBuilder _builder;

        public PlaceBuildDirector(IPlaceBuilder builder)
        {
            _builder = builder;
        }

        public Place Construct(Dictionary<string, string> placeParams, ThingsOfFoi thingsOfFoi, Foi foi)
        {
            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

            if (DoesPlaceNameExists(placeParams["naziv"], foi))
            {
                return null;
            }

            int placeUniqueIdentifier = randomGeneratorFacade.GiveRandomNumber(1, 1000);

            while (DoesIdentifierExists(placeUniqueIdentifier, foi))
            {
                placeUniqueIdentifier = randomGeneratorFacade.GiveRandomNumber(1, 1000);
            }

            List<Device> devices = new List<Device>();
            devices.AddRange(GetRandomDevices(Converter.StringToInt(placeParams["broj senzora"]), thingsOfFoi.Sensors.FindAll(sen => sen.Type == Converter.StringToInt(placeParams["tip"]) || sen.Type == 2)));
            devices.AddRange(GetRandomDevices(Converter.StringToInt(placeParams["broj aktuatora"]), thingsOfFoi.Actuators.FindAll(act => act.Type == Converter.StringToInt(placeParams["tip"]) || act.Type == 2)));

            return _builder
                .SetUniqueIdentifier(placeUniqueIdentifier)
                .SetName(placeParams["naziv"])
                .SetType(Converter.StringToInt(placeParams["tip"]))
                .SetNumberOfSensors(Converter.StringToInt(placeParams["broj senzora"]))
                .SetNumberOfActuators(Converter.StringToInt(placeParams["broj aktuatora"]))
                .SetDevices(devices)
                .Build();
        }

        private bool DoesPlaceNameExists(string placeName, Foi foi)
        {
            IIterator placeIterator = foi.Places.CreateIterator(IteratorType.Sequential);
            Place place = placeIterator.First();

            while (place != null)
            {
                if (place.Name == placeName)
                {
                    return true;
                }

                place = placeIterator.Next();
            }

            return false;
        }

        private bool DoesIdentifierExists(int placeIdentifier, Foi foi)
        {
            IIterator placeIterator = foi.Places.CreateIterator(IteratorType.Sequential);
            Place place = placeIterator.First();

            while (place != null)
            {
                if (place.UniqueIdentifier == placeIdentifier)
                {
                    return true;
                }

                place = placeIterator.Next();
            }

            return false;
        }

        private List<Device> GetRandomDevices(int? numberOfDevices, List<Device> availableDevices)
        {
            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();
            List<Device> placeDevices = new List<Device>();

            for (int i = 0; i < numberOfDevices; i++)
            {
                Device randomDevice = availableDevices[randomGeneratorFacade.GiveRandomNumber(0, availableDevices.Count)];
                placeDevices.Add(randomDevice.Clone());
            }

            return placeDevices;
        }
    }
}
