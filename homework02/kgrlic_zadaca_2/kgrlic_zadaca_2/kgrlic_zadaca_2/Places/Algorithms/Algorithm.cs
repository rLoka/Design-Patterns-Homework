using System.Collections.Generic;
using System.Threading;
using kgrlic_zadaca_2.Devices;
using kgrlic_zadaca_2.IO;

namespace kgrlic_zadaca_2.Places.Algorithms
{
    abstract class Algorithm
    {
        protected Foi Foi;
        protected Output Output = Output.GetInstance();

        protected Algorithm(Foi foi)
        {
            Foi = foi;
        }

        public abstract void Run(int threadCycleDuration);

        protected void CheckPlace(Place place, int threadCycleDuration)
        {
            Output.WriteLine("*************************** " + place.Name + " **************************** ");
            CheckDevicesOfPlace(place.Devices);
            Thread.Sleep(threadCycleDuration * 1000);
        }


        protected void CheckDevicesOfPlace(List<Device> devices)
        {
            for (int i = 0; i < devices.Count; i++)
            {
                Device device = devices[i];
                CheckStatus(device);
                CheckForMalfunction(device, devices, i);
                ReadValue(device);

                if (device.DeviceType == DeviceType.Actuator)
                {
                    MoveActuator(device);
                }
            }
        }


        protected void CheckStatus(Device device)
        {
            Output.WriteLine("..........................\r\n\tUređaj >>> " + device.Name);

            int deviceStatus = device.GetStatus();
            Output.WriteLine("Status >>> " + deviceStatus);
            Output.WriteLine("Ispravnost >>> " + (device.Malfunctional ? "ne" : "da"));
        }


        protected void ReadValue(Device device)
        {
            Output.WriteLine("Očitana vrijednost >>> " + device.ReadValue());
        }


        protected void CheckForMalfunction(Device device, List<Device> devices, int listIndex)
        {
            if (device.Malfunctional)
            {
                Output.WriteLine("!!! -- ZAMJENA UREĐAJA -- !!! ", true);
                devices[listIndex] = ReplaceMalfunctionalDevice(device);
            }
        }


        protected Device ReplaceMalfunctionalDevice(Device deviceForReplacement)
        {
            Device replacementDevice = deviceForReplacement.Clone();

            if (replacementDevice.Initialize() == 0)
            {
                replacementDevice.IsBeingUsed = false;
            }

            return replacementDevice;
        }


        protected void MoveActuator(Device device)
        {
            ((Actuator)device).ExecuteAction();
        }

    }
}
