using System;
using System.Text;
using System.Threading;

namespace kgrlic_zadaca_3.IO
{
    public class Output
    {
        private static volatile Output _instance;

        private static string _welcome = "\r\n\t████████╗██╗  ██╗██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███████╗███████╗ ██████╗ ██╗██████╗ ;╚══██╔══╝██║  ██║██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗██╔════╝██╔════╝██╔═══██╗██║╚════██╗;   ██║   ███████║██║██╔██╗ ██║██║  ███╗███████╗██║   ██║█████╗  █████╗  ██║   ██║██║ █████╔╝;   ██║   ██╔══██║██║██║╚██╗██║██║   ██║╚════██║██║   ██║██╔══╝  ██╔══╝  ██║   ██║██║██╔═══╝ ;   ██║   ██║  ██║██║██║ ╚████║╚██████╔╝███████║╚██████╔╝██║     ██║     ╚██████╔╝██║███████╗;   ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝     ╚═╝      ╚═════╝ ╚═╝╚══════╝";

        private static string _help = "-br broj redaka na ekranu;-bs broj stupaca na ekranu;-brk broj redaka na ekranu za unos komandi;-pi prosječni % ispravnosti uređaja;-g sjeme za generator slučajnog broja;-m naziv datoteke mjesta;-s naziv datoteke senzora;-a naziv datoteke aktuatora;-r naziv datoteke rasporeda;-tcd trajanje ciklusa dretve u sek;--help pomoć za korištenje opcija u programu";

        public static Output GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Output();
            }

            return _instance;
        }

        public void WriteLine(string line, bool isWarning = false)
        {
            if (isWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine("\t" + line);
            Console.ForegroundColor = ConsoleColor.White;
        }

        
        public void HelpUser()
        {
            string[] lineSplit = _help.Split(';');
            foreach (var line in lineSplit)
            {
                Console.WriteLine("\t" + line);
                Thread.Sleep(50);
            }
        }

        public void WelcomeUser(bool beep = false)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            string[] lineSplit = _welcome.Split(';');

            Console.WriteLine("\r\n\t#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ + ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#");

            foreach (var line in lineSplit)
            {
                Console.WriteLine("\t" + line);
                Thread.Sleep(100);
            }

            Console.WriteLine("\r\n\t#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ + ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#\r\n");

            Thread.Sleep(100);

        }
    }
}
