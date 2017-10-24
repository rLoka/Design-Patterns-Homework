using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_1.IO;

namespace kgrlic_zadaca_1.Devices
{
    abstract class Device
    {
        public string Name;
        public int? Type;
        public int? Kind;
        public float? Min;
        public float? Max;
        public string Comentary;

        protected int? _value;

        public int Value
        {
            get
            {
                if (!_value.HasValue)
                {
                    RandomGenerator randomGenerator = RandomGenerator.GetInstance();
                    _value = (int)randomGenerator.GetRandomFloat(Min ?? 0, Max ?? 1);
                }

                return _value ?? 0;
            }
        }

        public bool IsBeingUsed;
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

        protected List<int> StatusHistory = new List<int>();
        protected Dictionary<string, string> DeviceParams;

        protected Device(Dictionary<string, string> deviceParams)
        {
            Name = deviceParams["naziv"];
            Type = Converter.StringToInt(deviceParams["tip"]);
            Kind = Converter.StringToInt(deviceParams["vrsta"]);
            Min = Converter.StringToFloat(deviceParams["min vrijednost"]);
            Max = Converter.StringToFloat(deviceParams["max vrijednost"]);
            Comentary = deviceParams["komentar"];

            DeviceParams = deviceParams;
        }

        public bool IsDeviceValid()
        {
            if (Name.Length < 1 || Type == null || Kind == null || Min == null || Max == null)
            {
                return false;
            }

            return true;
        }

        public abstract Device Clone();

        public override string ToString()
        {
            return 
                "\r\n{ tip: " + Type 
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

        public int GetStatus(double successProbability = 0.5)
        {
            RandomGenerator randomGenerator = RandomGenerator.GetInstance();
            StatusHistory.Add(randomGenerator.GetRandomInteger(0, 1, new[] { 1 }, successProbability));
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

        public int Initialize()
        {
            StatusHistory = new List<int>();
            IsBeingUsed = true;
            return GetStatus(0.9);
        }
    }
}
