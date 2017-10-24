using System;
using System.Collections.Generic;
using kgrlic_zadaca_1.IO;

namespace kgrlic_zadaca_1.Devices
{    
    class Actuator : Device
    {
        public DeviceCreator.DeviceType DeviceType = DeviceCreator.DeviceType.Actuator;

        private bool _executionDirection = true;

        public Actuator(Dictionary<string, string> deviceParams) : base(deviceParams) { }

        public override Device Clone()
        {
             return new Actuator(DeviceParams);
        }

        public void ExecuteAction()
        {
            Output output = Output.GetInstance();
            RandomGenerator randomGenerator = RandomGenerator.GetInstance();

            output.WriteLine("Pokretanje motora >>> ...");
            Move();
        }

        private void Move()
        {
            Output output = Output.GetInstance();

            if (Kind == 3)
            {
                if (_value == 0)
                {
                    output.WriteLine("      smjer = ---> ");
                    _value = 1;
                }
                else
                {
                    output.WriteLine("      smjer = <--- ");
                    _value = 0;
                }

                output.WriteLine("      nova vrijednost =  " + ReadValue());
            }
            else
            {
                RandomGenerator randomGenerator = RandomGenerator.GetInstance();

                int amount = randomGenerator.GetRandomInteger((int)(Min ?? 0), (int)(Max ?? 1));

                output.WriteLine("      pomicanje za = " + amount);

                if (_executionDirection)
                {
                    output.WriteLine("      smjer = ---> ");

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
                    output.WriteLine("      smjer = <--- ");

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

                output.WriteLine("      nova vrijednost = " + ReadValue());
            }
        }
    }
}
