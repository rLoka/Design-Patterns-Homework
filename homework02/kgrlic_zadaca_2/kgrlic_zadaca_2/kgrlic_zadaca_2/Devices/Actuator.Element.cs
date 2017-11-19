using kgrlic_zadaca_2.Devices.Repair;

namespace kgrlic_zadaca_2.Devices
{
    partial class Actuator
    {
        public override void Accept(Visitor visitor)
        {
            visitor.RepairDevice(this);
        }
    }
}
