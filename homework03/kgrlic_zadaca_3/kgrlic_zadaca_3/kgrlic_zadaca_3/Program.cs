using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using kgrlic_zadaca_3.Configurations;
using kgrlic_zadaca_3.Devices;
using kgrlic_zadaca_3.Devices.Repair;
using kgrlic_zadaca_3.IO;
using kgrlic_zadaca_3.Places;
using kgrlic_zadaca_3.Places.Algorithms;
using kgrlic_zadaca_3.Places.Iterator;

namespace kgrlic_zadaca_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Output output = Output.GetInstance();
            output.WelcomeUser();

            if (args.Length > 18 || (args.Length < 4 && args.Length != 1))
            {
                output.WriteLine("Nedovoljan broj argumenata!", true);
                output.NotifyEnd();
                return;
            }
            else
            {
                if (args.Length == 1 && args[0] == "--help")
                {
                    output.HelpUser();
                    output.NotifyEnd();
                    return;
                }
                else
                {
                    if (args.Length % 2 != 0)
                    {
                        output.WriteLine("Broja argumenata mora biti paran!", true);
                        output.NotifyEnd();
                        return;
                    }
                }
            }

            Program program = new Program();

            Configuration configuration = program.LoadConfiguration(args);

            if (!configuration.IsConfigurationValid())
            {
                output.WriteLine("Argumenti nisu valjani! Provjerite argumente!", true);
                output.NotifyEnd();
                return;
            }

            program.SetUpOutput(configuration);
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
            program.EquipPlaces(foi);
            program.CheckDevices(configuration, foi);
        }

        private Configuration LoadConfiguration(string[] args)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilderImpl();

            ArgumentHandler generatorSeedHandler = new GeneratorSeedHandler();

            for (int i = 0; i < args.Length; i += 2)
            {
                generatorSeedHandler.HandleArgument(new Tuple<string, string>(args[i], args[i + 1]), configurationBuilder);
            }
            
            ConfigurationBuildDirector configurationBuildDirector = new ConfigurationBuildDirector(configurationBuilder);
            Configuration configuration = configurationBuildDirector.Construct();

            return configuration;
        }

        private void SetUpOutput(Configuration configuration)
        {
             Output.GetInstance().SetUpOutput(configuration.OutputFilePath, configuration.NumberOfLines ?? 100);
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
            IIterator placeIterator = foi.Places.CreateIterator(IteratorType.Sequential);
            Place place = placeIterator.First();

            while (place != null)
            {
                foreach (var device in place.Devices)
                {
                    if (device.Initialize() == 0)
                    {
                        device.IsBeingUsed = false;
                    }
                }
                place = placeIterator.Next();
            }
        }

        private void EquipPlaces(Foi foi)
        {
            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();
            IIterator placeIterator = foi.Places.CreateIterator(IteratorType.AscendingValue);
            Place place = placeIterator.First();

            while (place != null)
            {
                List<Device> actuators = place.Devices.FindAll(d => d.DeviceType == DeviceType.Actuator && d.IsBeingUsed && d.Malfunctional == false);
                List<Device> sensors = place.Devices.FindAll(d => d.DeviceType == DeviceType.Sensor && d.IsBeingUsed && d.Malfunctional == false);

                foreach (var actuator in actuators)
                {
                    int numberOfSensorsToEquipActuator = randomGeneratorFacade.GiveRandomNumber(1, sensors.Count);

                    List<int> usedSensorIndexes = new List<int>();
                    int[] availableSensorIndexes = Enumerable.Range(0, sensors.Count).ToArray();

                    for (int i = 0; i < numberOfSensorsToEquipActuator; i++)
                    {
                        int[] unusedSensorIndexes = availableSensorIndexes.Except(usedSensorIndexes).ToArray();
                        int randomIndex = randomGeneratorFacade.GiveRandomNumber(0, sensors.Count - 1, unusedSensorIndexes, 1);
                        usedSensorIndexes.Add(randomIndex);
                        
                        actuator.AddChild(sensors[randomIndex]);
                        sensors[randomIndex].AddParent(actuator);
                    }
                }
                
                RepairOneDevice(place);
                ListDevices(place);

                place = placeIterator.Next();
            }
        }

        private void RepairOneDevice(Place place)
        {
            Visitor repairVisitor = new DeviceRepairVisitor();
            place.Accept(repairVisitor);
        }

        private void ListDevices(Place place)
        {
            Output output = Output.GetInstance();
            output.WriteLine("*********************************************************************************");
            output.WriteLine("Ispis opremljenih aktuatora i senzora za mjesto: " + place.Name);
            List<Device> devices = place.Devices.FindAll(d => d.IsBeingUsed && d.Malfunctional == false);
            devices.Reverse();

            foreach (var device in devices)
            {
                output.WriteLine("-----------------------------------------------");
                output.WriteLine("Podaci za uređaj => " + device.Name);

                output.WriteLine("Pridruženi uređaji >>> ...");
                if (!device.IsLeaf())
                {
                    foreach (var child in device.GetChildren())
                    {
                        output.WriteLine("\t" + child.Name);
                    }
                }
                else
                {
                    output.WriteLine("\tnema");
                }

                output.WriteLine("Uređaj je pridružen sljedećim uređajima >>> ...");
                if (!device.IsRoot())
                {
                    foreach (var parent in device.GetParents())
                    {
                        output.WriteLine("\t" + parent.Name);
                    }
                }
                else
                {
                    output.WriteLine("\tnema");
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