using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Devices
{
    class ThingsOfFoi
    {
        public readonly List<Sensor> Sensors = new List<Sensor>();
        public readonly List<Actuator> Actuators = new List<Actuator>();

        public void AddSensor(Sensor sensor)
        {
            Sensors.Add(sensor);
        }

        public void AddActuator(Actuator actuator)
        {
            Actuators.Add(actuator);
        }
    }
}
