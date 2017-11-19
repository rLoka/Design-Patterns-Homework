using System.Collections.Generic;
using kgrlic_zadaca_2.IO;

namespace kgrlic_zadaca_2.Devices
{
    partial class Actuator
    {
        private List<Device> _children = new List<Device>();

        public override void AddChild(Device device)
        {
            _children.Add(device);
        }

        public override void RemoveChild(Device device)
        {
            _children.Remove(device);
        }

        public override List<Device> GetChildren()
        {
            return _children;
        }

        public override List<Device> GetParents()
        {
            Output output = Output.GetInstance();
            output.WriteLine("Ovaj uređaj nema naduređaja.", true);

            return null;
        }

        public override bool IsLeaf()
        {
            return false;
        }
    }
}
