using System.Collections.Generic;

namespace kgrlic_zadaca_2.Devices
{
    class Sensor : Device
    {
        public DeviceCreator.DeviceType DeviceType = DeviceCreator.DeviceType.Sensor;

        public Sensor(Dictionary<string, string> deviceParams) : base(deviceParams) { }

        public override Device Clone()
        {
            return new Sensor(DeviceParams);
        }
    }
}
