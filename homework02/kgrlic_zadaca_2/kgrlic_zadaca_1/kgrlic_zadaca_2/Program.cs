using System;
using System.Collections.Generic;
using System.Threading;
using kgrlic_zadaca_2.Algorithms;
using kgrlic_zadaca_2.Configurations;
using kgrlic_zadaca_2.Devices;
using kgrlic_zadaca_2.IO;
using kgrlic_zadaca_2.Places;

namespace kgrlic_zadaca_2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 8)
            {
                Console.WriteLine("Nedovoljan  broj argumenata!");
                return;
            }

            Program program = new Program();

            Configuration configuration = program.LoadConfiguration(args);

            if (!configuration.IsConfigurationValid())
            {
                Console.WriteLine("Pogrešni argumenti!");
                return;
            }

            Output output = program.GetOutput(configuration);

            ThingsOfFoi thingsOfFoi = program.LoadDevices(configuration);
            FOI foi = program.LoadPlaces(configuration, thingsOfFoi);

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

        private Output GetOutput(Configuration configuration)
        {
            return Output.GetInstance(configuration.OutputFilePath);
        }

        private RandomGenerator GetRandomGenerator(Configuration configuration)
        {
            return RandomGenerator.GetInstance(configuration.GeneratorSeed);
        }

        private ThingsOfFoi LoadDevices(Configuration configuration)
        {
            ThingsOfFoi thingsOfFoi = new ThingsOfFoi();

            List<Dictionary<string, string>> sensorsList = CSV.Parse(configuration.SensorsFilePath);
            List<Dictionary<string, string>> actuatorsList = CSV.Parse(configuration.ActuatorsFilePath);
            DeviceCreator deviceCreator = new DeviceCreator();
            Output output = GetOutput(configuration);

            foreach (var sensor in sensorsList)
            {
                Device device = deviceCreator.CreateDevice(sensor, DeviceCreator.DeviceType.Sensor);
                if (device.IsDeviceValid())
                {
                    thingsOfFoi.AddSensor(device);
                }
                else
                {
                    output.WriteLine("Unos za uređaj: '" + device.Name + "' nije dobar. Preskačem!");
                }
                
            }

            foreach (var actuator in actuatorsList)
            {
                Device device = deviceCreator.CreateDevice(actuator, DeviceCreator.DeviceType.Actuator);
                if (device.IsDeviceValid())
                {
                    thingsOfFoi.AddActuator(device);
                }
                else
                {
                    output.WriteLine("Unos za uređaj: '" + device.Name + "' nije dobar. Preskačem!");
                }
            }

            return thingsOfFoi;
        }

        private FOI LoadPlaces(Configuration configuration, ThingsOfFoi thingsOfFoi)
        {
            FOI foi = new FOI();
            List<Dictionary<string, string>> placeList = CSV.Parse(configuration.PlaceFilePath);

            IPlaceBuilder placeBuilder = new PlaceBuilderImpl();
            PlaceBuildDirector placeBuildDirector = new PlaceBuildDirector(placeBuilder);

            Output output = Output.GetInstance();

            foreach (var placeParams in placeList)
            {
                Place newPlace = placeBuildDirector.Construct(placeParams, thingsOfFoi);
                if (newPlace == null)
                {
                    output.WriteLine("Mjesto '" + placeParams["naziv"] + "' već postoji. Preskačem ...");
                }
                else
                {
                    if (newPlace.IsPlaceValid())
                    {
                        foi.Places.Add(newPlace);
                    }
                    else
                    {
                        output.WriteLine("Unos za mjesto: '" + newPlace.Name + "' nije dobar. Preskačem!");
                    }
                    
                }
            }
            
            return foi;
        }

        private void InitializeSystem(FOI foi)
        {
            foreach (var place in foi.Places)
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

        private void CheckDevices(Configuration configuration, FOI foi)
        {
            AlgorithmFactory algorithmFactory = AlgorithmFactory.GetFactory(configuration.Algorithm);
            Algorithm algorithm = algorithmFactory.CreateAlgorithm(foi);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = false;

                for (int i = 0; i < configuration.NumberOfThreadCycles; i++)
                {
                    algorithm.Run(configuration.ThreadCycleDuration ?? 1);
                }
                
                ShowStatistics(foi);

                Console.WriteLine("Program je završio. Pritisnite tipku za izlaz.");
                Console.ReadKey();

            }).Start();
        }

        private void ShowStatistics(FOI foi)
        {
            Output output = Output.GetInstance();

            foreach (var place in foi.Places)
            {
                output.WriteLine(place.ToString());
                output.WriteLine("\r\n>>>>>>>>>>>>>>> > UREĐAJI < <<<<<<<<<<<<<<<<<\r\n");

                foreach (var device in place.Devices)
                {
                    output.WriteLine(device.ToString());
                }
            }

        }
    }
}
