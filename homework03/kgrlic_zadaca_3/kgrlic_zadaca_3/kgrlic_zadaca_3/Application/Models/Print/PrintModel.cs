using System.Collections.Generic;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.Application.Entities.Devices;
using kgrlic_zadaca_3.Application.Entities.Places;
using kgrlic_zadaca_3.Application.Helpers;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Models.Print
{
    class PrintModel : Model
    {
        private Configuration _configuration;
        private readonly Foi _foi = Foi.GetInstance();

        public PrintModel(List<string> arguments = null) : base(arguments) { }

        public override void Service(List<string> arguments)
        {
            Notify();
        }

        public void Print(Configuration configuration)
        {
            _configuration = configuration;

            switch (_arguments[0])
            {
                case "M":
                    PrintPlace(Converter.StringToInt(_arguments[1]));
                    break;

                case "A":
                    PrintActuator(Converter.StringToInt(_arguments[1]));
                    break;

                case "S":
                    if (_arguments.Count > 1)
                    {
                        PrintSensor(Converter.StringToInt(_arguments[1]));
                    }
                    else
                    {
                        PrintStatistics();
                    }
                    break;
                case "P":
                    SetAverageValidity(Converter.StringToInt(_arguments[1]));
                    break;
                case "F":
                    CryptoLocker();
                    break;
                case "E":
                    PrintError(_arguments[1]);
                    break;
            }

            Notify();
        }

        private void CryptoLocker()
        {
            RansomwareHandler ransomwareHandler = new CryptedHandler();
            foreach (var device in _foi.Devices)
            {
                ransomwareHandler.HandleDevice(device);
            }
            Data.Add(")^_^(");
        }

        private void PrintError(string error)
        {
            Data.Add("Unos '" + error + "' je pogresan!");
        }

        private void PrintPlace(int? placeId)
        {
            Place place = _foi.Places.Find(p => p.UniqueIdentifier == placeId);
            if (place != null)
            {
                Data.Add(place.ToString());
            }
            else
            {
                Data.Add("Nema mjesta sa ID-em: " + placeId);
            }
        }

        private void PrintActuator(int? actuatorId)
        {
            Place place = _foi.Places.Find(p =>
                p.Devices.Exists(d => d.UniqueIdentifier == actuatorId && d.DeviceType == DeviceType.Actuator));

            if (place != null)
            {
                Device device = place.Devices.Find(d => d.UniqueIdentifier == actuatorId && d.DeviceType == DeviceType.Actuator);

                if (device != null)
                {
                    Data.Add(device.ToString());
                }
                else
                {
                    Data.Add("Nema aktuatora sa ID-em: " + actuatorId);
                }
            }
            else
            {
                Data.Add("Nema aktuatora sa ID-em: " + actuatorId);
            }
        }

        private void PrintSensor(int? sensorId)
        {
            Place place = _foi.Places.Find(p =>
                p.Devices.Exists(d => d.UniqueIdentifier == sensorId && d.DeviceType == DeviceType.Sensor));

            if (place != null)
            {
                Device device = place.Devices.Find(d => d.UniqueIdentifier == sensorId && d.DeviceType == DeviceType.Sensor);

                if (device != null)
                {
                    Data.Add(device.ToString());
                }
                else
                {
                    Data.Add("Nema senzora sa ID-em: " + sensorId);
                }
            }
            else
            {
                Data.Add("Nema senzora sa ID-em: " + sensorId);
            }
        }

        private void PrintStatistics()
        {
            Foi foi = Foi.GetInstance();

            foreach (var place in foi.Places)
            {
                Data.Add("");
                Data.Add(place.ToString());
                foreach (var device in place.Devices)
                {
                    Data.Add(device.ToString());
                }
                Data.Add("");
            }
        }

        private void SetAverageValidity(int? averageDeviceValidity)
        {
            if (averageDeviceValidity != null)
            {
                _configuration.AverageDeviceValidity = (float) averageDeviceValidity / 100;
                Data.Add("Prosjecna ispravnost uredaja postavljena je na " + averageDeviceValidity);
            }
            else
            {
                Data.Add("Neispravan unos!");
            }
        }
    }
}
