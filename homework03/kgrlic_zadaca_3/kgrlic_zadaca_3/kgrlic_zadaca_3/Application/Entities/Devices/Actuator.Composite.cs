using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Devices
{
    partial class Actuator
    {
        private List<Device> _children = new List<Device>();

        public override void AddChild(Device device)
        {
            _children.Add(device);
        }

        public override void AddParent(Device device) { }

        public override List<Device> GetChildren()
        {
            return _children;
        }

        public override List<Device> GetParents()
        {
            return new List<Device>();
        }
    }
}
