using kgrlic_zadaca_3.Devices.Repair;

namespace kgrlic_zadaca_3.Devices
{
    partial class Actuator
    {
        public override void Accept(Visitor visitor)
        {
            visitor.RepairDevice(this);
        }
    }
}
