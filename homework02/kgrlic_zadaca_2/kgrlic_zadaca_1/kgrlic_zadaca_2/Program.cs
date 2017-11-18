using System;
using System.Collections.Generic;
using System.Threading;
using kgrlic_zadaca_2.Configurations;
using kgrlic_zadaca_2.Devices;
using kgrlic_zadaca_2.IO;
using kgrlic_zadaca_2.Places;
using kgrlic_zadaca_2.Places.Algorithms;
using kgrlic_zadaca_2.Places.Iterator;

namespace kgrlic_zadaca_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Output output = Output.GetInstance();
            output.WelcomeUser();

            if (args.Length != 8)
            {
                output.WriteLine("Nedovoljan broj argumenata!", true);
                output.NotifyEnd();
                return;
            }

            Program program = new Program();

            Configuration configuration = program.LoadConfiguration(args);

            if (!configuration.IsConfigurationValid())
            {
                Console.WriteLine("Argumenti nisu valjani! Provjerite argumente!");
                output.NotifyEnd();
                return;
            }

            program.SetOutputPath(configuration);
            program.InitializeRandomGenerator(configuration);

            ThingsOfFoi thingsOfFoi = program.LoadDevices(configuration);

            if (thingsOfFoi == null)
            {
                output.NotifyEnd();
                return;
            }

            Foi foi = program.LoadPlaces(configuration, thingsOfFoi);

            if (foi == null)
            {
                output.NotifyEnd();
                return;
            }

            program.InitializeSystem(foi);
            program.CheckDevices(configuration, foi);
        }

        private Configuration LoadConfiguration(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilderImpl();
            ConfigurationBuildDirector configurationBuildDirector = new ConfigurationBuildDirector(configurationBuilder);
            Configuration configuration = configurationBuildDirector.Construct(args);

            return configuration;
        }

        private void SetOutputPath(Configuration configuration)
        {
             Output.GetInstance().SetOutputPath(configuration.OutputFilePath);
        }

        private void InitializeRandomGenerator(Configuration configuration)
        {
            new RandomGeneratorFacade(configuration.GeneratorSeed ?? 0);
        }

        private ThingsOfFoi LoadDevices(Configuration configuration)
        {
            ThingsOfFoi thingsOfFoi = new ThingsOfFoi();
            Output output = Output.GetInstance();

            List<Dictionary<string, string>> sensorsList = Csv.Parse(configuration.SensorsFilePath);
            List<Dictionary<string, string>> actuatorsList = Csv.Parse(configuration.ActuatorsFilePath);

            DeviceCreator deviceCreator = new DeviceCreator();

            foreach (var sensor in sensorsList)
            {
                Device device = deviceCreator.CreateDevice(sensor, DeviceType.Sensor, thingsOfFoi);
                if (device.IsDeviceValid())
                {
                    thingsOfFoi.AddSensor(device);
                }
                else
                {
                    output.WriteLine("Unos za uređaj: '" + device.Name + "' nije dobar. Preskačem!");
                }
                
            }

            if (sensorsList.Count == 0 || thingsOfFoi.Sensors.Count == 0)
            {
                output.WriteLine("Nije učitan nijedan senzor. Program ne može nastaviti!");
                return null;
            }

            foreach (var actuator in actuatorsList)
            {
                Device device = deviceCreator.CreateDevice(actuator, DeviceType.Actuator, thingsOfFoi);
                if (device.IsDeviceValid())
                {
                    thingsOfFoi.AddActuator(device);
                }
                else
                {
                    output.WriteLine("Unos za uređaj: '" + device.Name + "' nije dobar. Preskačem!");
                }
            }

            if (actuatorsList.Count == 0 || thingsOfFoi.Actuators.Count == 0)
            {
                output.WriteLine("Nije učitan nijedan aktuator. Program ne može nastaviti!");
                return null;
            }

            return thingsOfFoi;
        }

        private Foi LoadPlaces(Configuration configuration, ThingsOfFoi thingsOfFoi)
        {
            Output output = Output.GetInstance();
            Foi foi = new Foi();
            List<Dictionary<string, string>> placeList = Csv.Parse(configuration.PlaceFilePath);

            IPlaceBuilder placeBuilder = new PlaceBuilderImpl();
            PlaceBuildDirector placeBuildDirector = new PlaceBuildDirector(placeBuilder);

            foreach (var placeParams in placeList)
            {
                Place newPlace = placeBuildDirector.Construct(placeParams, thingsOfFoi, foi);

                if (newPlace == null)
                {
                    output.WriteLine("Mjesto '" + placeParams["naziv"] + "' već postoji. Preskačem ...");
                }
                else
                {
                    if (newPlace.IsPlaceValid())
                    {
                        foi.Places[foi.Places.Count] = newPlace;
                    }
                    else
                    {
                        output.WriteLine("Unos za mjesto: '" + newPlace.Name + "' nije dobar. Preskačem!");
                    }
                    
                }
            }

            if (placeList.Count == 0 || foi.Places.Count == 0)
            {
                output.WriteLine("Nije učitano nijedno mjesto. Program ne može nastaviti!");
                return null;
            }

            return foi;
        }

        private void InitializeSystem(Foi foi)
        {
            IIterator placeIterator = foi.Places.CreateIterator(IteratorType.AscendingValue);

            for 
            (
                Place place = placeIterator.First();
                !placeIterator.IsDone();
                place = placeIterator.Next()
            )
            {
                IEnumerable<Device> devicesOfPlace = place.Devices;

                foreach (var device in devicesOfPlace)
                {
                    if (device.Initialize() == 0)
                    {
                        device.IsBeingUsed = false;
                    }
                }
            }
        }

        private void CheckDevices(Configuration configuration, Foi foi)
        {
            Output output = Output.GetInstance();
            AlgorithmCreator algorithmCreator = new AlgorithmCreator();
            Algorithm algorithm = algorithmCreator.CreateAlgorithm(configuration.Algorithm, foi);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = false;

                for (int i = 0; i < configuration.NumberOfThreadCycles; i++)
                {
                    algorithm.Run(configuration.ThreadCycleDuration ?? 1);
                }
                
                ShowStatistics(foi);

                output.NotifyEnd();

            }).Start();
        }

        private void ShowStatistics(Foi foi)
        {
            Output output = Output.GetInstance();

            IIterator placeIterator = foi.Places.CreateIterator(IteratorType.Sequential);

            for (
                Place place = (Place)placeIterator.First();
                !placeIterator.IsDone();
                place = (Place)placeIterator.Next()
                )
            {
                output.WriteLine(place.ToString());
                output.WriteLine(">>>>>>>>>>>>>>> > UREĐAJI < <<<<<<<<<<<<<<<<<\r\n");

                foreach (var device in place.Devices)
                {
                    output.WriteLine(device.ToString());
                }
            }

        }
    }
}
