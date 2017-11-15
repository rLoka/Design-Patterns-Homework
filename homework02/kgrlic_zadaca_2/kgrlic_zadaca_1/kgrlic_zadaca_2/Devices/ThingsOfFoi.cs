using System.Collections.Generic;

namespace kgrlic_zadaca_2.Devices
{
    class ThingsOfFoi
    {
        public List<Device> Sensors = new List<Device>();
        public List<Device> Actuators = new List<Device>();

        public void AddSensor(Device sensor)
        {
            Sensors.Add(sensor);
        }

        public void AddActuator(Device actuator)
        {
            Actuators.Add(actuator);
        }
    }
}
