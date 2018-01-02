using System;
using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.Configurations;
using kgrlic_zadaca_3.Devices;
using kgrlic_zadaca_3.Places;

namespace kgrlic_zadaca_3.IO
{
    class EntityLoader
    {
        public Configuration LoadConfiguration(string[] args)
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

        public ThingsOfFoi LoadDevices(Configuration configuration)
        {
            ThingsOfFoi thingsOfFoi = new ThingsOfFoi();
            Output output = Output.GetInstance();

            List<Dictionary<string, string>> sensorsList = Csv.Parse(configuration.SensorsFilePath);
            List<Dictionary<string, string>> actuatorsList = Csv.Parse(configuration.ActuatorsFilePath);

            DeviceCreator deviceCreator = new DeviceCreator();

            foreach (var row in sensorsList)
            {
                Sensor sensor = (Sensor)deviceCreator.CreateDevice(row, DeviceType.Sensor, thingsOfFoi);
                if (sensor.IsDeviceValid())
                {
                    if (!thingsOfFoi.Sensors.Exists(s => s.ModelIdentifier == sensor.ModelIdentifier))
                    {
                        thingsOfFoi.AddSensor(sensor);
                    }
                    else
                    {
                        output.WriteLine("Model senzora sa ID-jem: '" + sensor.UniqueIdentifier + " (" + sensor.Name + ") " + "' vec postoji. Preskacem!");
                    }
                    
                }
                else
                {
                    output.WriteLine("Unos za uređaj: '" + sensor.Name + "' nije dobar. Preskačem!");
                }

            }

            if (sensorsList.Count == 0 || thingsOfFoi.Sensors.Count == 0)
            {
                output.WriteLine("Nije učitan nijedan senzor. Program ne može nastaviti!");
                return null;
            }

            foreach (var row in actuatorsList)
            {
                Actuator actuator = (Actuator)deviceCreator.CreateDevice(row, DeviceType.Actuator, thingsOfFoi);
                if (actuator.IsDeviceValid())
                {
                    if (!thingsOfFoi.Actuators.Exists(a => a.ModelIdentifier == actuator.ModelIdentifier))
                    {
                        thingsOfFoi.AddActuator(actuator);
                    }
                    else
                    {
                        output.WriteLine("Aktuator sa ID-jem: " + actuator.UniqueIdentifier + " (" + actuator.Name + ") " + " vec postoji. Preskacem!");
                    }
                }
                else
                {
                    output.WriteLine("Unos za uredaj: '" + actuator.Name + "' nije dobar. Preskacem!");
                }
            }

            if (actuatorsList.Count == 0 || thingsOfFoi.Actuators.Count == 0)
            {
                output.WriteLine("Nije učitan nijedan aktuator. Program ne moze nastaviti!");
                return null;
            }

            return thingsOfFoi;
        }

        public Foi LoadPlaces(Configuration configuration, ThingsOfFoi thingsOfFoi)
        {
            Output output = Output.GetInstance();
            Foi foi = new Foi();
            List<Dictionary<string, string>> placeList = Csv.Parse(configuration.PlaceFilePath);

            IPlaceBuilder placeBuilder = new PlaceBuilderImpl();
            PlaceBuildDirector placeBuildDirector = new PlaceBuildDirector(placeBuilder);

            foreach (var row in placeList)
            {
                Place newPlace = placeBuildDirector.Construct(row, thingsOfFoi, foi);

                if (newPlace != null)
                {
                    if (newPlace.IsPlaceValid())
                    {
                        foi.AddPlace(newPlace);
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

        public List<Schedule> LoadSchedules(Configuration configuration)
        {
            List<Schedule> schedules = new List<Schedule>();
            List<Dictionary<string, string>> scheduleList = Csv.Parse(configuration.ScheduleFilePath, 3);

            foreach (var row in scheduleList)
            {
                Schedule schedule = new Schedule();
                if (row["vrsta zapisa"] == "0")
                {
                    schedule.TypeOfRecord = 0;
                    schedule.PlaceId = Converter.StringToInt(row["id mjesta"]);
                    schedule.TypeOfDevice = Converter.StringToInt(row["vrsta"]);
                    schedule.DeviceModelId = Converter.StringToInt(row["id modela uredaja"]);
                    schedule.DeviceId = Converter.StringToInt(row["id uredaja"]);
                    schedules.Add(schedule);
                }
                else if (row["vrsta zapisa"] == "1")
                {
                    schedule.TypeOfRecord = 1;
                    schedule.ActuatorId = Converter.StringToInt(row["id aktuatora"]);
                    List<string> seonsorIds = row["id senzor"].Split(',').ToList();

                    foreach (var sensorId in seonsorIds)
                    {
                        schedule.SensorIds.Add(Converter.StringToInt(sensorId));
                    }

                    schedules.Add(schedule);
                }
                else
                {
                    Console.WriteLine("Neispravan tip zapisa, preskacem!");
                }
            }

            return schedules;
        }
    }
}
