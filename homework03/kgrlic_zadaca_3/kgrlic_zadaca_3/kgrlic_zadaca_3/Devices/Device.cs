using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Devices
{
    abstract partial class Device
    {
        //Abstract methods
        public abstract Device Clone();

        //Shared stuff
        public string Name;
        public int? Type;
        public int? Kind;
        public float? Min;
        public float? Max;
        public int? ModelIdentifier;
        public int? UniqueIdentifier;
        public string Comentary;
        public DeviceType DeviceType;

        protected int? _value;

        public int Value
        {
            get
            {
                if (!_value.HasValue)
                {
                    RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();
                    _value = (int)randomGeneratorFacade.GiveRandomNumber(Min ?? 0, Max ?? 1);
                }

                return _value ?? 0;
            }
        }

        public bool IsBeingUsed;

        public List<int> StatusHistory = new List<int>();

        public bool Malfunctional
        {
            get
            {
                if (StatusHistory.Count >= 3)
                {
                    if (StatusHistory.Skip(StatusHistory.Count - 3).Take(3).ToArray().SequenceEqual(new int[] {0,0,0}))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        protected ThingsOfFoi ThingsOfFoi;
        protected Dictionary<string, string> DeviceParams;

        protected Device(Dictionary<string, string> deviceParams, ThingsOfFoi thingsOfFoi)
        {
            ModelIdentifier = Converter.StringToInt(deviceParams["id"]);
            Name = deviceParams["naziv"];
            Type = Converter.StringToInt(deviceParams["tip"]);
            Kind = Converter.StringToInt(deviceParams["vrsta"]);
            Min = Converter.StringToFloat(deviceParams["min vrijednost"]);
            Max = Converter.StringToFloat(deviceParams["max vrijednost"]);
            Comentary = deviceParams["komentar"];

            DeviceParams = deviceParams;
            ThingsOfFoi = thingsOfFoi;
        }

        public bool IsDeviceValid()
        {
            return Name.Length >= 1 && Type != null && Kind != null && Min != null && Max != null;
        }

        public override string ToString()
        {
            return
                "{ ID: " + UniqueIdentifier
                + ", tip: " + Type 
                + ", vrsta: " + Kind 
                + ", min: " + Min 
                + ", max: " + Max 
                + ", komentar:" + Comentary
                + ", konačna vrijednost:" + ReadValue()
                + ", korišten:" + (IsBeingUsed ? "Da" : "Ne")
                + ", komentar:" + Comentary
                + ", pogrešnih statusa:" + StatusHistory.FindAll(s => s == 0).Count
                + ", pouzdanost (greške/svi statusi):" + ((1 - ((float)StatusHistory.FindAll(s => s == 0).Count / StatusHistory.Count)) * 100).ToString("N2") + "%"
                + " }";
        }

        public int GetStatus(double? averageDeviceValidity = 0.5)
        {
            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();
            StatusHistory.Add(randomGeneratorFacade.GiveRandomNumber(0, 1, new[] { 1 }, averageDeviceValidity));
            return StatusHistory.Last();
        }

        public string ReadValue()
        {
            switch (Kind)
            {
                case 0:
                    return Value.ToString() + " " + Comentary;
                case 1:
                    return ((double) Value).ToString("N1") + " " + Comentary;
                case 2:
                    return ((double) Value).ToString("N5") + " " + Comentary;
                case 3:
                    return Value == 0 ? "Ne" : "Da";
            }

            return "";
        }

        public int Initialize(double? averageDeviceValidity)
        {
            StatusHistory = new List<int>();
            IsBeingUsed = true;
            return GetStatus(averageDeviceValidity);
        }
    }
}
