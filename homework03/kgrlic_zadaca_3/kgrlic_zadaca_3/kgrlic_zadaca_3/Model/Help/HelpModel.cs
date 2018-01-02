using System.Collections.Generic;

namespace kgrlic_zadaca_3.Model.Help
{
    class HelpModel : Framework.Model
    {
        private static readonly string _menu = "M x - ispis podataka mjesta x;S x - ispis podataka senzora x;A x - ispis podataka aktuatora x;S - ispis statistike;SP - spremi podatke (mjesta, uredaja);VP - vrati spremljene podatke (mjesta, uredaja);C n - izvrsavanje n ciklusa dretve (1-100);VF - izvrsavanje vlastite funkcionalnosti;PI n - prosjecni % ispravnosti uređaja (0-100);VF [argumenti] - izvrsavanje vlastite funkcionalnosti, po potrebni moguci su argumenti;PI n - prosjecna % ispravnosti uređaja (0-100);H - pomoc, ispis dopustenih komandi i njihov opis;I - izlaz";

        public override string GetData()
        {
            return _menu;
        }

        public override void Service(List<string> arguments)
        {
            Notify();
        }
    }
}
