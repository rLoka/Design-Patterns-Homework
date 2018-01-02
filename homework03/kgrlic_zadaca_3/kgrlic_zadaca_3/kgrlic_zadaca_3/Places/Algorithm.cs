using System.Collections.Generic;
using System.Threading;
using kgrlic_zadaca_3.Configurations;
using kgrlic_zadaca_3.Devices;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Places
{
    class Algorithm
    {
        private readonly Foi _foi;
        private readonly Configuration _configuration;
        private readonly Output _output = Output.GetInstance();

        public Algorithm(Foi foi, Configuration configuration)
        {
            _foi = foi;
            _configuration = configuration;
        }

        public void Run()
        {
            foreach (var place in _foi.Places)
            {
                CheckPlace(place, _configuration.ThreadCycleDuration);
            }
        }

        private void CheckPlace(Place place, int? threadCycleDuration)
        {
            _output.WriteLine("************************ " + place.Name + " (" + place.UniqueIdentifier + ") ************************* ");
            CheckDevicesOfPlace(place.Devices);
            Thread.Sleep(threadCycleDuration * 1000 ?? 1000);
        }


        private void CheckDevicesOfPlace(List<Device> devices)
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


        private void CheckStatus(Device device)
        {
            _output.WriteLine("..........................\r\n\tUređaj >>> " + device.Name + " (" + device.UniqueIdentifier + ")");

            int deviceStatus = device.GetStatus();
            _output.WriteLine("Status >>> " + deviceStatus);
            _output.WriteLine("Ispravnost >>> " + (device.Malfunctional ? "ne" : "da"));
        }


        private void ReadValue(Device device)
        {
            _output.WriteLine("Očitana vrijednost >>> " + device.ReadValue());
        }


        private void CheckForMalfunction(Device device, List<Device> devices, int listIndex)
        {
            if (device.Malfunctional)
            {
                _output.WriteLine("!!! -- ZAMJENA UREĐAJA -- !!! ", true);
                devices[listIndex] = ReplaceMalfunctionalDevice(device);
            }
        }


        private Device ReplaceMalfunctionalDevice(Device deviceForReplacement)
        {
            Device replacementDevice = deviceForReplacement.Clone();

            if (replacementDevice.Initialize(_configuration.AverageDeviceValidity) == 0)
            {
                replacementDevice.IsBeingUsed = false;
            }

            return replacementDevice;
        }


        private void MoveActuator(Device device)
        {
            ((Actuator)device).ExecuteAction();
        }

    }
}
