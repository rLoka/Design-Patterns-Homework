using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Devices
{
    partial class Sensor
    {
        private List<Device> _parents = new List<Device>();

        public override void AddChild(Device device) { }

        public override void AddParent(Device device)
        {
            _parents.Add(device);
        }

        public override List<Device> GetChildren()
        {
            return new List<Device>();
        }

        public override List<Device> GetParents()
        {
            return _parents;
        }
    }
}
