using System.Collections.Generic;

namespace kgrlic_zadaca_1.Devices
{
    class DeviceCreator
    {
        public enum DeviceType
        {
            Actuator,
            Sensor
        }

        public Device CreateDevice(Dictionary<string, string> deviceParams, DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.Actuator:
                    return new Actuator(deviceParams);
                case DeviceType.Sensor:
                    return new Sensor(deviceParams);
                default: return null;
            }
        }
    }
}
