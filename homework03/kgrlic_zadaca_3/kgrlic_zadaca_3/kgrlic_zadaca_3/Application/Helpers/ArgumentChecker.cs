using System;

namespace kgrlic_zadaca_3.Application.Helpers
{
    static class ArgumentChecker
    {
        public static bool CheckArguments(string[] args)
        {
            if (args.Length > 20 || (args.Length < 4 && args.Length != 1))
            {
                Console.WriteLine("Pogresan broj argumenata!");
                return false;
            }
            if (args.Length == 1 && args[0] == "--help")
            {
                string _help = "-br broj redaka na ekranu;-bs broj stupaca na ekranu;-brk broj redaka na ekranu za unos komandi;-pi prosječni % ispravnosti uređaja;-g sjeme za generator slučajnog broja;-m naziv datoteke mjesta;-s naziv datoteke senzora;-a naziv datoteke aktuatora;-r naziv datoteke rasporeda;-tcd trajanje ciklusa dretve u sek;--help pomoć za korištenje opcija u programu";

                string[] lineSplit = _help.Split(';');
                foreach (var line in lineSplit)
                {
                    Console.WriteLine(line);
                }
                return false;
            }
            if (args.Length == 1 && args[0] != "--help")
            {
                Console.WriteLine("Argumenti nisu valjani! Provjerite argumente!");
                return false;
            }
            if (args.Length % 2 != 0)
            {
                Console.WriteLine("Broja argumenata mora biti paran!");
                return false;
            }
            return true;
        }
    }
}
