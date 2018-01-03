using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.Application.Entities.Devices;
using kgrlic_zadaca_3.Application.Entities.Places;
using kgrlic_zadaca_3.Application.Helpers;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Models.Index
{
    class IndexModel : Model
    {
        private ThingsOfFoi _thingsOfFoi;
        private Foi _foi;
        private List<Schedule> _schedules;
        private Configuration _configuration;

        public override void Service(List<string> arguments) { }

        public void LoadEntities(Configuration configuration)
        {
            _configuration = configuration;

            new RandomGeneratorFacade(configuration.GeneratorSeed ?? 0);

            _thingsOfFoi = LoadDevices(configuration);

            if (_thingsOfFoi == null)
            {
                return;
            }

            _foi = LoadPlaces(configuration, _thingsOfFoi);

            if (_foi == null)
            {
                return;
            }

            _schedules = LoadSchedules(_configuration);

            Initialize();

            Notify();
        }

        private ThingsOfFoi LoadDevices(Configuration configuration)
        {
            ThingsOfFoi thingsOfFoi = new ThingsOfFoi();

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
                        Data.Add("Model senzora sa ID-jem: '" + sensor.UniqueIdentifier + " (" + sensor.Name + ") " + "' vec postoji. Preskacem!");
                    }
                }
                else
                {
                    Data.Add("Unos za uredaj: '" + sensor.Name + "' nije dobar. Preskacem!");
                }
            }

            if (sensorsList.Count == 0 || thingsOfFoi.Sensors.Count == 0)
            {
                Data.Add("Nije ucitan nijedan senzor. Program ne moze nastaviti!");
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
                        Data.Add("Aktuator sa ID-jem: " + actuator.UniqueIdentifier + " (" + actuator.Name + ") " + " vec postoji. Preskacem!");
                    }
                }
                else
                {
                    Data.Add("Unos za uredaj: '" + actuator.Name + "' nije dobar. Preskacem!");
                }
            }

            if (actuatorsList.Count == 0 || thingsOfFoi.Actuators.Count == 0)
            {
                Data.Add("Nije ucitan nijedan aktuator. Program ne moze nastaviti!");
                return null;
            }

            return thingsOfFoi;
        }

        private Foi LoadPlaces(Configuration configuration, ThingsOfFoi thingsOfFoi)
        {
            Foi foi = Foi.GetInstance();
            List<Dictionary<string, string>> placeList = Csv.Parse(configuration.PlaceFilePath);

            IPlaceBuilder placeBuilder = new PlaceBuilderImpl();
            PlaceBuildDirector placeBuildDirector = new PlaceBuildDirector(placeBuilder);

            foreach (var row in placeList)
            {
                Place newPlace = placeBuildDirector.Construct(row, thingsOfFoi);

                if (newPlace != null)
                {
                    if (newPlace.IsPlaceValid())
                    {
                        foi.AddPlace(newPlace);
                    }
                    else
                    {
                        Data.Add("Unos za mjesto: '" + newPlace.Name + "' nije dobar. Preskacem!");
                    }

                }
            }

            if (placeList.Count == 0 || foi.Places.Count == 0)
            {
                Data.Add("Nije ucitano nijedno mjesto. Program ne moze nastaviti!");
                return null;
            }

            return foi;
        }

        private List<Schedule> LoadSchedules(Configuration configuration)
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
                    Data.Add("Neispravan tip zapisa, preskacem!");
                }
            }

            return schedules;
        }

        private void Initialize()
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
                    Data.Add("Pogresan tip zapisa: " + schedule.TypeOfRecord);
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
                    Actuator actuator = (Actuator)actuatorPlace.Devices.Find(d => d.UniqueIdentifier == actuatorId && d.DeviceType == DeviceType.Actuator);
                    Sensor sensor = (Sensor)sensorPlace.Devices.Find(d => d.UniqueIdentifier == sensorId && d.DeviceType == DeviceType.Sensor);

                    if (!actuator.GetChildren().Contains(sensor))
                    {
                        actuator.AddChild(sensor);
                        sensor.AddParent(actuator);
                    }
                    else
                    {
                        Data.Add("Aktuator (" + actuatorId + ") vec sadrzi senzor (" + sensorId + ")!");
                    }

                }
                else
                {
                    Data.Add("Aktuator (" + actuatorId + ") i/ili senzor (" + sensorId + ") se ne nalaze u (istoj) prostoriji pa ih se ne moze spariti!");
                }
            }
            else
            {
                Data.Add("Aktuator sa ID-em: " + actuatorId + " nije definiran ni za jedno mjesto!");
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
                                Sensor sensor = (Sensor)_thingsOfFoi.Sensors
                                    .Find(s => s.ModelIdentifier == schedule.DeviceModelId).Clone();

                                if (place.Type == sensor.Type || sensor.Type == 2)
                                {
                                    sensor.UniqueIdentifier = schedule.DeviceId;
                                    place.Devices.Add(sensor);
                                }
                                else
                                {
                                    Data.Add("Tip senzora (" + sensor.Kind + ") ne odgovara tipu mjesta (" +
                                                      place.Type + ")!");
                                }
                            }
                            else
                            {
                                Data.Add("Senzor sa ID-em: " + schedule.DeviceId + " vec postoji!");
                            }
                        }
                        else
                        {
                            Data.Add("Ne postoji sensor sa ID-em modela: " + schedule.DeviceModelId);
                        }
                    }
                    else
                    {
                        Data.Add("Mjesto " + place.Name + " (" + place.UniqueIdentifier +
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
                                Actuator actuator = (Actuator)_thingsOfFoi.Actuators
                                    .Find(a => a.ModelIdentifier == schedule.DeviceModelId).Clone();

                                if (place.Type == actuator.Type || actuator.Type == 2)
                                {
                                    actuator.UniqueIdentifier = schedule.DeviceId;
                                    place.Devices.Add(actuator);
                                }
                                else
                                {
                                    Data.Add("Tip aktuatora (" + actuator.Kind +
                                                      ") ne odgovara tipu mjesta (" + place.Type + ")!");
                                }
                            }
                            else
                            {
                                Data.Add("Senzor sa ID-em: " + schedule.DeviceId + " vec postoji!");
                            }
                        }
                        else
                        {
                            Data.Add("Ne postoji actuator sa ID-em modela: " + schedule.DeviceModelId);
                        }

                    }
                    else
                    {
                        Data.Add("Mjesto " + place.Name + " (" + place.UniqueIdentifier +
                                          ") vec ima dovoljan broj aktuatora (max " + place.NumberOfActuators + "), pa se aktuator (" + schedule.DeviceId + ") ne definira!");
                    }
                }
                else
                {
                    Data.Add("Ne postoji tip uredaja: " + schedule.TypeOfDevice);
                }
            }
            else
            {
                Data.Add("Ne postoji mjesto sa ID-em: " + schedule.PlaceId);
            }
        }

        private void ListDevices(Place place)
        {
            Data.Add("*********************************************************************************");
            Data.Add("Ispis opremljenih aktuatora i senzora za mjesto: " + place.Name + " (" + place.UniqueIdentifier + ")");
            List<Device> devices = place.Devices.FindAll(d => d.Malfunctional == false);
            devices.Reverse();

            foreach (var device in devices)
            {
                Data.Add("-----------------------------------------------");
                Data.Add("Podaci za uredaj => " + device.Name + " (" + device.UniqueIdentifier + ")");

                Data.Add("Pridruzeni uređaji >>> ...");
                if (!device.IsLeaf())
                {
                    foreach (var child in device.GetChildren())
                    {
                        Data.Add(child.Name + " (" + child.UniqueIdentifier + ")");
                    }
                }
                else
                {
                    Data.Add("nema");
                }

                Data.Add("Uredaj je pridružen sljedecim uređajima >>> ...");
                if (!device.IsRoot())
                {
                    foreach (var parent in device.GetParents())
                    {
                        Data.Add(parent.Name + " (" + parent.UniqueIdentifier + ")");
                    }
                }
                else
                {
                    Data.Add("nema");
                }

            }
        }
    }
}
