using System.Collections.Generic;
using kgrlic_zadaca_3.Application.Entities.Places;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Models.State
{
    class StateModel : Model
    {
        private readonly Foi _foi = Foi.GetInstance();

        public StateModel(List<string> arguments = null) : base(arguments)
        {
        }

        public override void Service(List<string> arguments)
        {
            Notify();
        }

        public void Execute()
        {
            switch (_arguments[0])
            {
                case "S":
                    SaveState();
                    break;

                case "V":
                    RestoreState();
                    break;
            }

            Notify();
        }

        private void SaveState()
        {
            Foi foi = Foi.GetInstance();
            FoiCaretaker foiCaretaker = FoiCaretaker.GetInstance();

            foiCaretaker.FoiMemento = foi.CreateMemento();

            Data.Add("Stanje mjesta i uredaja spremljeno!");
        }

        private void RestoreState()
        {
            Foi foi = Foi.GetInstance();
            FoiCaretaker foiCaretaker = FoiCaretaker.GetInstance();

            foi.SetMemento(foiCaretaker.FoiMemento);

            Data.Add("Stanje mjesta i uredaja vraceno!");
        }
    }
}
