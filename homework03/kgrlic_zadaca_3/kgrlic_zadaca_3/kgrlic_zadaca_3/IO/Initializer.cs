using System;
using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.Configurations;
using kgrlic_zadaca_3.Devices;
using kgrlic_zadaca_3.Places;

namespace kgrlic_zadaca_3.IO
{
    class Initializer
    {
        private Foi _foi;
        private ThingsOfFoi _thingsOfFoi;
        private List<Schedule> _schedules;
        private Configuration _configuration;

        public Initializer(Foi foi, ThingsOfFoi thingsOfFoi, List<Schedule> schedules, Configuration configuration)
        {
            _foi = foi;
            _thingsOfFoi = thingsOfFoi;
            _schedules = schedules;
            _configuration = configuration;
        }
        

        public void Initialize()
        {
            ArrangeEntities();
            InitializeSystem();
            foreach (var place in _foi.Places)
            {
                ListDevices(place);
            }
            
        }

        private void ArrangeEntities()
        {
            foreach (var schedule in _schedules)
            {
                if (schedule.TypeOfRecord == 0)
                {
                    EquipPlace(schedule);
                }
                else if (schedule.TypeOfRecord == 1)
                {
                    foreach (var sensorId in schedule.SensorIds)
                    {
                        EquipActuator(schedule.ActuatorId, sensorId);
                    }
                }
                else
                {
                    Console.WriteLine("Pogresan tip zapisa: " + schedule.TypeOfRecord);
                }
            }
        }

        private void InitializeSystem()
        {
            foreach (var place in _foi.Places)
            {
                foreach (var device in place.Devices)
                {
                    if (device.Initialize(_configuration.AverageDeviceValidity) == 0)
                    {
                        device.IsBeingUsed = false;
                    }
                }
            }
        }
       
        private void EquipActuator(int? actuatorId, int? sensorId)
        {
            if (_foi.Places.Exists(p => p.Devices.Exists(d => d.UniqueIdentifier == actuatorId && d.DeviceType == DeviceType.Actuator)))
            {
                Place actuatorPlace = _foi.Places.Find(p => p.Devices.Exists(d => d.UniqueIdentifier == actuatorId && d.DeviceType == DeviceType.Actuator));
                Place sensorPlace = _foi.Places.Find(p => p.Devices.Exists(d => d.UniqueIdentifier == sensorId && d.DeviceType == DeviceType.Sensor));

                if (actuatorPlace == sensorPlace)
                {
                    Actuator actuator = (Actuator) actuatorPlace.Devices.Find(d => d.UniqueIdentifier == actuatorId && d.DeviceType == DeviceType.Actuator);
                    Sensor sensor = (Sensor) sensorPlace.Devices.Find(d => d.UniqueIdentifier == sensorId && d.DeviceType == DeviceType.Sensor);

                    if (!actuator.GetChildren().Contains(sensor))
                    {
                        actuator.AddChild(sensor);
                        sensor.AddParent(actuator);
                    }
                    else
                    {
                        Console.WriteLine("Aktuator (" + actuatorId + ") vec sadrzi senzor (" + sensorId + ")!");
                    }

                }
                else
                {
                    Console.WriteLine("Aktuator (" + actuatorId + ") i/ili senzor (" + sensorId + ") se ne nalaze u (istoj) prostoriji pa ih se ne moze spariti!");
                }
            }
            else
            {
                Console.WriteLine("Aktuator sa ID-em: " + actuatorId + " nije definiran ni za jedno mjesto!");
            }
        }

        private void EquipPlace(Schedule schedule)
        {
            if (_foi.Places.Any(p => p.UniqueIdentifier == schedule.PlaceId))
            {
                Place place = _foi.Places.Find(p => p.UniqueIdentifier == schedule.PlaceId);

                if (schedule.TypeOfDevice == 0)
                {
                    if (place.Devices.FindAll(d => d.DeviceType == DeviceType.Sensor).Count < place.NumberOfSensors)
                    {
                        if (_thingsOfFoi.Sensors.Any(s => s.ModelIdentifier == schedule.DeviceModelId))
                        {
                            if (!_foi.Places.Exists(p => p.Devices.Exists(d => d.UniqueIdentifier == schedule.DeviceId)))
                            {
                                Sensor sensor = (Sensor) _thingsOfFoi.Sensors
                                    .Find(s => s.ModelIdentifier == schedule.DeviceModelId).Clone();

                                if (place.Type == sensor.Type || sensor.Type == 2)
                                {
                                    sensor.UniqueIdentifier = schedule.DeviceId;
                                    place.Devices.Add(sensor);
                                }
                                else
                                {
                                    Console.WriteLine("Tip senzora (" + sensor.Kind + ") ne odgovara tipu mjesta (" +
                                                      place.Type + ")!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Senzor sa ID-em: " + schedule.DeviceId + " vec postoji!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ne postoji sensor sa ID-em modela: " + schedule.DeviceModelId);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Mjesto " + place.Name + " (" + place.UniqueIdentifier +
                                          ") vec ima dovoljan broj senzora (max " + place.NumberOfSensors + "), pa se senzor (" + schedule.DeviceId + ") ne definira!");
                    }
                }
                else if (schedule.TypeOfDevice == 1)
                {
                    if (place.Devices.FindAll(d => d.DeviceType == DeviceType.Actuator).Count < place.NumberOfActuators)
                    {
                        if (_thingsOfFoi.Actuators.Any(a => a.ModelIdentifier == schedule.DeviceModelId))
                        {
                            if (!_foi.Places.Exists(p => p.Devices.Exists(d => d.UniqueIdentifier == schedule.DeviceId)))
                            {
                                Actuator actuator = (Actuator) _thingsOfFoi.Actuators
                                    .Find(a => a.ModelIdentifier == schedule.DeviceModelId).Clone();

                                if (place.Type == actuator.Type || actuator.Type == 2)
                                {
                                    actuator.UniqueIdentifier = schedule.DeviceId;
                                    place.Devices.Add(actuator);
                                }
                                else
                                {
                                    Console.WriteLine("Tip aktuatora (" + actuator.Kind +
                                                      ") ne odgovara tipu mjesta (" + place.Type + ")!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Senzor sa ID-em: " + schedule.DeviceId + " vec postoji!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ne postoji actuator sa ID-em modela: " + schedule.DeviceModelId);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Mjesto " + place.Name + " (" + place.UniqueIdentifier +
                                          ") vec ima dovoljan broj aktuatora (max " + place.NumberOfActuators + "), pa se aktuator (" + schedule.DeviceId + ") ne definira!");
                    }
                }
                else
                {
                    Console.WriteLine("Ne postoji tip uredaja: " + schedule.TypeOfDevice);
                }
            }
            else
            {
                Console.WriteLine("Ne postoji mjesto sa ID-em: " + schedule.PlaceId);
            }
        }

        /*
       private void EquipPlaces(Foi foi)
       {
           RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

           foreach (var place in foi.Places)
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

               ListDevices(place);

           }
       }
       */

        private void ListDevices(Place place)
        {
            Output output = Output.GetInstance();
            output.WriteLine("*********************************************************************************");
            output.WriteLine("Ispis opremljenih aktuatora i senzora za mjesto: " + place.Name + " (" + place.UniqueIdentifier + ")" );
            List<Device> devices = place.Devices.FindAll(d => d.Malfunctional == false);
            devices.Reverse();

            foreach (var device in devices)
            {
                output.WriteLine("-----------------------------------------------");
                output.WriteLine("Podaci za uređaj => " + device.Name + " (" + device.UniqueIdentifier + ")");

                output.WriteLine("Pridruženi uređaji >>> ...");
                if (!device.IsLeaf())
                {
                    foreach (var child in device.GetChildren())
                    {
                        output.WriteLine("\t" + child.Name + " (" + child.UniqueIdentifier + ")");
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
                        output.WriteLine("\t" + parent.Name + " (" + parent.UniqueIdentifier + ")");
                    }
                }
                else
                {
                    output.WriteLine("\tnema");
                }

            }
        }
    }
}
