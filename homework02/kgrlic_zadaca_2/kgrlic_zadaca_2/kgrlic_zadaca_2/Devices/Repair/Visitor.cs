using System.Collections.Generic;
using kgrlic_zadaca_2.IO;

namespace kgrlic_zadaca_2.Devices.Repair
{
    abstract class Visitor
    {
        public abstract void RepairDevice(Device device);
    }

    class DeviceRepairVisitor : Visitor
    {
        public override void RepairDevice(Device device)
        {
            Output.GetInstance().WriteLine("Visitor popravlja uređaj: " + device.Name, true);
            device.IsBeingUsed = true;
            device.StatusHistory = new List<int>();
        }
    }
}
