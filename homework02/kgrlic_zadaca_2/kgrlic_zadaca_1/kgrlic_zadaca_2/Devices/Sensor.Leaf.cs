using System.Collections.Generic;
using kgrlic_zadaca_2.IO;

namespace kgrlic_zadaca_2.Devices
{
    partial class Sensor
    {
        private List<Device> _parents = new List<Device>();

        public override void AddChild(Device device)
        {
            Output output = Output.GetInstance();
            output.WriteLine("Nije moguće dodati uređaj jer je ovo senzor.", true);
        }

        public override void RemoveChild(Device device)
        {
            Output output = Output.GetInstance();
            output.WriteLine("Ovaj uređaj nema poduređaja.", true);
        }

        public override List<Device> GetChildren()
        {
            Output output = Output.GetInstance();
            output.WriteLine("Ovaj uređaj nema poduređaja.", true);

            return null;
        }

        public override List<Device> GetParents()
        {
            return _parents;
        }

        public override bool IsLeaf()
        {
            return true;
        }
    }
}
