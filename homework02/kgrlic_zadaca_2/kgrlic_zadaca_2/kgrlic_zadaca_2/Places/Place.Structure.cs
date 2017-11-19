using System.Collections.Generic;
using kgrlic_zadaca_2.Devices;
using kgrlic_zadaca_2.Devices.Repair;
using kgrlic_zadaca_2.IO;

namespace kgrlic_zadaca_2.Places
{
    partial class Place
    {
        public void Accept(Visitor visitor)
        {
            List<Device> brokenDevices = Devices.FindAll(d => d.IsBeingUsed == false || d.Malfunctional);

            if (brokenDevices.Count > 0)
            {
                RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();
                int randomDeviceIndex = randomGeneratorFacade.GiveRandomNumber(0, brokenDevices.Count);
                brokenDevices[randomDeviceIndex].Accept(visitor);
            }
        }
    }
}
