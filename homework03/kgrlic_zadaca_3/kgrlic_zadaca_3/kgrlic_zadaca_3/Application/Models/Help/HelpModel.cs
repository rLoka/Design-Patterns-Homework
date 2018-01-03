using System.Collections.Generic;
using System.Linq;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Models.Help
{
    class HelpModel : Model
    {
        private static readonly string _menu = "M x - ispis podataka mjesta x;S x - ispis podataka senzora x;A x - ispis podataka aktuatora x;S - ispis statistike;SP - spremi podatke (mjesta, uredaja);VP - vrati spremljene podatke (mjesta, uredaja);C n - izvrsavanje n ciklusa dretve (1-100);VF - izvrsavanje vlastite funkcionalnosti;PI n - prosjecni % ispravnosti uređaja (0-100);H - pomoc, ispis dopustenih komandi i njihov opis;I - izlaz";

        public HelpModel(List<string> arguments = null) : base(arguments)
        {
            Data = _menu.Split(';').ToList();
            Notify();
        }

        public override void Service(List<string> arguments)
        {
            Notify();
        }
    }
}
