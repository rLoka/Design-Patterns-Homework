using System;
using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.Devices;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Places
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

            if (DoesPlaceExists(Converter.StringToInt(placeParams["id"]), foi))
            {
                Console.WriteLine("Mjesto sa identifikatorom: " + placeParams["id"] + "(" + placeParams["naziv"] + ") vec postoji, preskacem!");
                return null;
            }

            return _builder
                .SetUniqueIdentifier(Converter.StringToInt(placeParams["id"]))
                .SetName(placeParams["naziv"])
                .SetType(Converter.StringToInt(placeParams["tip"]))
                .SetNumberOfSensors(Converter.StringToInt(placeParams["broj senzora"]))
                .SetNumberOfActuators(Converter.StringToInt(placeParams["broj aktuatora"]))
                .Build();
        }

        private bool DoesPlaceExists(int? placeId, Foi foi)
        {
            return foi.Places.Any(p => p.UniqueIdentifier == placeId);
        }
    }
}
