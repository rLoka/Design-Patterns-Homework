using System.Collections.Generic;
using System.Threading;
using kgrlic_zadaca_2.Devices;
using kgrlic_zadaca_2.IO;
using kgrlic_zadaca_2.Places;

namespace kgrlic_zadaca_2.Algorithms
{
    abstract class Algorithm
    {
        protected FOI Foi;
        protected Output Output = Output.GetInstance();
        protected List<Place> OrderedPlaces;

        protected Algorithm(FOI foi)
        {
            Foi = foi;
        }

        public abstract void OrderPlaces();

        public void Run(int threadCycleDuration = 1)
        {
            if (OrderedPlaces == null)
            {
                OrderPlaces();
            }

            int perPlaceDelay = threadCycleDuration * 1000 / Foi.Places.Count;

            for (var i = 0; i < OrderedPlaces.Count; i++)
            {
                Place place = OrderedPlaces[i];

                Output.WriteLine("\r\n\r\n\r\n*************************** " + place.Name + " **************************** ");
                Output.WriteLine("\r\n\r\n-------------------------- Senzori --------------------------");
                for (int j = 0; j < place.Sensors.Count; j++)
                {
                    Device device = place.Sensors[j];
                    CheckStatus(device);
                    CheckForMalfunction(device, place.Sensors, j);
                    ReadValue(device);
                    Thread.Sleep(perPlaceDelay / place.Sensors.Count);
                }

                Output.WriteLine("\r\n\r\n-------------------------- Aktuatori --------------------------");
                for (int j = 0; j < place.Actuators.Count; j++)
                {
                    Device device = place.Actuators[j];
                    CheckStatus(device);
                    CheckForMalfunction(device, place.Actuators, j);
                    ReadValue(device);
                    MoveActuator(device);
                    Thread.Sleep(perPlaceDelay / place.Actuators.Count);
                }
            }
        }


        private void CheckStatus(Device device)
        {
            Output.WriteLine("\r\n..........................\r\nUređaj >>> " + device.Name);

            int deviceStatus = device.GetStatus();
            Output.WriteLine("Status >>> " + deviceStatus);
            Output.WriteLine("Ispravnost >>> " + (device.Malfunctional ? "ne" : "da"));
        }


        private void ReadValue(Device device)
        {
            Output.WriteLine("Očitana vrijednost >>> " + device.ReadValue());
        }


        private void CheckForMalfunction(Device device, List<Device> devices, int listIndex)
        {
            if (device.Malfunctional)
            {
                Output.WriteLine("!!! -- ZAMJENA UREĐAJA -- !!! ");
                Device replacementDevice = device.Clone();

                if (replacementDevice.Initialize() == 0)
                {
                    replacementDevice.IsBeingUsed = false;
                }

                devices[listIndex] = replacementDevice;
            }
        }

        private void MoveActuator(Device device)
        {
            ((Actuator)device).ExecuteAction();
        }

    }
}
