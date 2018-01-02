using System.Collections.Generic;

namespace kgrlic_zadaca_3.Devices
{
    partial class Sensor : Device
    {
        public Sensor(Dictionary<string, string> deviceParams, ThingsOfFoi thingsOfFoi) : base(deviceParams, thingsOfFoi)
        {
            DeviceType = DeviceType.Sensor;
        }

        public override Device Clone()
        {
            return new Sensor(DeviceParams, ThingsOfFoi);
        }
    }
}
