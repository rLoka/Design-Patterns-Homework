﻿using System.Collections.Generic;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Devices
{
    partial class Actuator
    {
        private List<Device> _children = new List<Device>();

        public override void AddChild(Device device)
        {
            _children.Add(device);
        }

        public override void AddParent(Device device)
        {
            Output output = Output.GetInstance();
            output.WriteLine("Nije moguće dodati uređaj jer je ovo aktuator.", true);
        }

        public override List<Device> GetChildren()
        {
            return _children;
        }

        public override List<Device> GetParents()
        {
            Output output = Output.GetInstance();
            return new List<Device>();
        }
    }
}
