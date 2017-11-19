using System.Collections.Generic;

namespace kgrlic_zadaca_2.Devices
{
    abstract partial class Device
    {
        //Composite methods of Device class
        public abstract void AddChild(Device device);
        public abstract void RemoveChild(Device device);
        public abstract List<Device> GetChildren();
        public abstract List<Device> GetParents();
        public abstract bool IsLeaf();
    }
}
