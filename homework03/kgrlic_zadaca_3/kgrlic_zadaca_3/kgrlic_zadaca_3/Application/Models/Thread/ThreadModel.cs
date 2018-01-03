using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.Application.Entities.Devices;
using kgrlic_zadaca_3.Application.Entities.Places;
using kgrlic_zadaca_3.Application.Helpers;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Models.Thread
{
    class ThreadModel : Model
    {
        private Configuration _configuration;
        private readonly Foi _foi = Foi.GetInstance();

        public ThreadModel(List<string> arguments = null) : base(arguments) { } 

        public override void Service(List<string> arguments = null) { }

        public void SpinTheThread(Configuration configuration)
        {
            _configuration = configuration;

            int? numberOfThreadCycles = Converter.StringToInt(_arguments[0]);

            if (numberOfThreadCycles == null)
            {
                Data.Add("Broj ciklusa dretvi nije pravilno unesen. Izvest ce se 1 ciklus!");
            }

            System.Threading.Thread thread = new System.Threading.Thread(() => Run(numberOfThreadCycles ?? 1));
            thread.Start();

            Notify();
        }

        private void Run(int numberOfThreadCycles)
        {
            for (int i = 0; i < numberOfThreadCycles; i++)
            {
                foreach (var place in _foi.Places)
                {
                    CheckPlace(place);
                }

                System.Threading.Thread.Sleep(_configuration.ThreadCycleDuration * 1000 ?? 1000);
            }
        }

        private void CheckPlace(Place place)
        {
            Data.Add("");
            Data.Add("");
            Data.Add("** " + place.Name + " (" + place.UniqueIdentifier + ") **");
            CheckDevicesOfPlace(place.Devices);
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
           Data.Add("");
           Data.Add("Uredaj = " + device.Name + " (" + device.UniqueIdentifier + ")");

           int deviceStatus = device.GetStatus();
           Data.Add("Status >>> " + deviceStatus);
           Data.Add("Ispravnost >>> " + (device.Malfunctional ? "ne" : "da"));
        }


        private void ReadValue(Device device)
        {
            Data.Add("Ocitana vrijednost >>> " + device.ReadValue());
        }


        private void CheckForMalfunction(Device device, List<Device> devices, int listIndex)
        {
            if (device.Malfunctional)
            {
                
                devices[listIndex] = ReplaceMalfunctionalDevice(device);
            }
        }


        private Device ReplaceMalfunctionalDevice(Device deviceForReplacement)
        {
            Device replacementDevice = deviceForReplacement.Clone();
            
            replacementDevice.UniqueIdentifier = _foi.Devices.OrderByDescending(d => d.UniqueIdentifierNN).First().UniqueIdentifier + 1;

            if (replacementDevice.Initialize(_configuration.AverageDeviceValidity) == 0)
            {
                replacementDevice.IsBeingUsed = false;
            }

            Data.Add("ZAMJENA UREĐAJA >>> (" + deviceForReplacement.UniqueIdentifier + " --> " + replacementDevice.UniqueIdentifier + ")");

            return replacementDevice;
        }


        private void MoveActuator(Device device)
        {
            ((Actuator)device).ExecuteAction(Data);
        }
    }
}
