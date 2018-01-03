using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Devices
{
    public enum DeviceType
    {
        Actuator,
        Sensor
    }

    class DeviceCreator
    {
        public Device CreateDevice(Dictionary<string, string> deviceParams, DeviceType deviceType, ThingsOfFoi thingsOfFoi)
        {
            switch (deviceType)
            {
                case DeviceType.Actuator:
                    return new Actuator(deviceParams, thingsOfFoi);
                case DeviceType.Sensor:
                    return new Sensor(deviceParams, thingsOfFoi);
                default: return null;
            }
        }
    }
}
