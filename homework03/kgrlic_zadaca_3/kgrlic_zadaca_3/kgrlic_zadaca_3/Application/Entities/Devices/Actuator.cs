using System.Collections.Generic;
using kgrlic_zadaca_3.Application.Helpers;

namespace kgrlic_zadaca_3.Application.Entities.Devices
{    
    partial class Actuator : Device
    {
        private bool _executionDirection = true;

        public Actuator(Dictionary<string, string> deviceParams, ThingsOfFoi thingsOfFoi) : base(deviceParams, thingsOfFoi)
        {
            DeviceType = DeviceType.Actuator;
        }

        public override Device Clone()
        {
             return new Actuator(DeviceParams, ThingsOfFoi);
        }

        public override Device DeepClone()
        {
            Actuator actuator = (Actuator)Clone();
            actuator._value = _value;

            actuator.IsBeingUsed = IsBeingUsed;
            actuator.UniqueIdentifier = UniqueIdentifier;

            foreach (var status in StatusHistory)
            {
                actuator.StatusHistory.Add(status);
            }

            return actuator;
        }

        public void ExecuteAction(List<string> data)
        {
            data.Add("Pokretanje motora >>> ...");
            Move(data);
        }

        private void Move(List<string> data)
        {
            if (Kind == 3)
            {
                if (_value == 0)
                {
                    data.Add("smjer = (+) ");
                    _value = 1;
                }
                else
                {
                    data.Add("smjer = (-) ");
                    _value = 0;
                }

                data.Add("nova vrijednost =  " + ReadValue());
            }
            else
            {
                RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

                int amount = randomGeneratorFacade.GiveRandomNumber((int)(Min ?? 0), (int)(Max ?? 1));

                data.Add("pomicanje za = " + amount);

                if (_executionDirection)
                {
                    data.Add("smjer = (+) ");

                    if (Value + amount > (int)(Max ?? 1))
                    {
                        _value = (int)(Max ?? 1);
                        _executionDirection = false;
                    }
                    else
                    {
                        _value = Value + amount;
                    }

                }
                else
                {
                    data.Add("smjer = (-) ");

                    if (Value - amount < (int)(Min ?? 0))
                    {
                        _value = (int)(Min ?? 0);
                        _executionDirection = true;
                    }
                    else
                    {
                        _value = Value - amount;
                    }
                }

                data.Add("nova vrijednost = " + ReadValue());
            }
        }
    }
}
