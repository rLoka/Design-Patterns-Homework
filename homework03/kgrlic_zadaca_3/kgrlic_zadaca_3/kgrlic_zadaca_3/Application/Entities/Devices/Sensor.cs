using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Devices
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

        public override Device DeepClone()
        {
            Sensor sensor = (Sensor)Clone();
            sensor._value = _value;

            sensor.IsBeingUsed = IsBeingUsed;
            sensor.UniqueIdentifier = UniqueIdentifier;

            foreach (var status in StatusHistory)
            {
                sensor.StatusHistory.Add(status);
            }

            return sensor;
        }
    }
}
