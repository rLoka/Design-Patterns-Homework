using System.Collections.Generic;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Devices
{
    partial class Sensor : Device
    {
        public Sensor(Dictionary<string, string> deviceParams, ThingsOfFoi thingsOfFoi) : base(deviceParams, thingsOfFoi)
        {
            DeviceType = DeviceType.Sensor;
            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

            do
            {
                UniqueIdentifier = randomGeneratorFacade.GiveRandomNumber(1, 1000);
            } while (DoesUniqueIdentifierExists(UniqueIdentifier, thingsOfFoi.Sensors));

        }

        public override Device Clone()
        {
            return new Sensor(DeviceParams, ThingsOfFoi);
        }
    }
}
