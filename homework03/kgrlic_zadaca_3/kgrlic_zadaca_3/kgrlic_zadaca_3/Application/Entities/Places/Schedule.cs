using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Places
{
    class Schedule
    {
        public int? TypeOfRecord;
        public int? PlaceId;
        public int? TypeOfDevice;
        public int? DeviceModelId;
        public int? DeviceId;
        public int? ActuatorId;
        public List<int?> SensorIds = new List<int?>();
    }
}
