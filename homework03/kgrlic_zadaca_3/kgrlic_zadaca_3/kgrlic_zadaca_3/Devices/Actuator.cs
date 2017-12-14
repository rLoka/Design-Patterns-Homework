using System.Collections.Generic;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Devices
{    
    partial class Actuator : Device
    {
        private bool _executionDirection = true;

        public Actuator(Dictionary<string, string> deviceParams, ThingsOfFoi thingsOfFoi) : base(deviceParams, thingsOfFoi)
        {
            DeviceType = DeviceType.Actuator;

            RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

            do
            {
                UniqueIdentifier = randomGeneratorFacade.GiveRandomNumber(1, 1000);
            } while (DoesUniqueIdentifierExists(UniqueIdentifier, thingsOfFoi.Actuators));
        }

        public override Device Clone()
        {
             return new Actuator(DeviceParams, ThingsOfFoi);
        }

        public void ExecuteAction()
        {
            Output output = Output.GetInstance();
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
                    output.WriteLine("\tsmjer = ---> ");
                    _value = 1;
                }
                else
                {
                    output.WriteLine("\tsmjer = <--- ");
                    _value = 0;
                }

                output.WriteLine("\tnova vrijednost =  " + ReadValue());
            }
            else
            {
                RandomGeneratorFacade randomGeneratorFacade = new RandomGeneratorFacade();

                int amount = randomGeneratorFacade.GiveRandomNumber((int)(Min ?? 0), (int)(Max ?? 1));

                output.WriteLine("\tpomicanje za = " + amount);

                if (_executionDirection)
                {
                    output.WriteLine("\tsmjer = ---> ");

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
                    output.WriteLine("\tsmjer = <--- ");

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

                output.WriteLine("\tnova vrijednost = " + ReadValue());
            }
        }
    }
}
