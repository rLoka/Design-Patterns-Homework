using System;
using System.IO;
using System.Text;
using System.Threading;

namespace kgrlic_zadaca_2.IO
{
    class Output
    {
        private static volatile Output _instance;

        private static string _welcome = "\r\n\t████████╗██╗  ██╗██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███████╗███████╗ ██████╗ ██╗██████╗ ;╚══██╔══╝██║  ██║██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗██╔════╝██╔════╝██╔═══██╗██║╚════██╗;   ██║   ███████║██║██╔██╗ ██║██║  ███╗███████╗██║   ██║█████╗  █████╗  ██║   ██║██║ █████╔╝;   ██║   ██╔══██║██║██║╚██╗██║██║   ██║╚════██║██║   ██║██╔══╝  ██╔══╝  ██║   ██║██║██╔═══╝ ;   ██║   ██║  ██║██║██║ ╚████║╚██████╔╝███████║╚██████╔╝██║     ██║     ╚██████╔╝██║███████╗;   ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝     ╚═╝      ╚═════╝ ╚═╝╚══════╝";

        public string OutputFilePath;

        public static Output GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Output();
            }

            return _instance;
        }

        protected Output() { }

        public void SetOutputPath(string outputFilePath)
        {
            OutputFilePath = outputFilePath;

            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
        }

        public void WriteLine(string line, bool isWarning = false)
        {

            if (isWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine("\t" + line);

            Console.ForegroundColor = ConsoleColor.White;

            if (OutputFilePath != null)
            {
                WriteToOutputFile(line);
            }
        }

        public void NotifyEnd()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\n\r\n\tProgram je završio. Za izlaz pritisnite tipku ENTER");

            Console.ReadLine();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        private void WriteToOutputFile(string line)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(OutputFilePath, true))
            {
                file.WriteLine(line);
            }
        }

        public void WelcomeUser(bool beep = false)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            string[] lineSplit = _welcome.Split(';');

            if (beep)
            {
                new Thread(delegate()
                {
                    Thread.CurrentThread.IsBackground = true;
                    Beeper();
                }).Start();
            }

            WriteLine("\r\n\t#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ + ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#");

            foreach (var line in lineSplit)
            {
                Console.WriteLine("\t" + line);
                Thread.Sleep(100);
            }

            WriteLine("\r\n\t#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ + ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#\r\n");

            Thread.Sleep(300);

        }

        public void Beeper()
        {
            Console.Beep(658, 125);
            Console.Beep(1320, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 250);
            Console.Beep(1320, 125);
            Console.Beep(1188, 125);
            Console.Beep(1056, 250);
            Console.Beep(990, 250);
            Console.Beep(880, 500);
            Console.Beep(880, 250);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 750);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(250);
            Console.Beep(1188, 500);
            Console.Beep(1408, 250);
            Console.Beep(1760, 500);
            Console.Beep(1584, 250);
            Console.Beep(1408, 250);
            Console.Beep(1320, 750);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(500);
            Console.Beep(1320, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 250);
            Console.Beep(1320, 125);
            Console.Beep(1188, 125);
            Console.Beep(1056, 250);
            Console.Beep(990, 250);
            Console.Beep(880, 500);
            Console.Beep(880, 250);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 750);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(250);
            Console.Beep(1188, 500);
            Console.Beep(1408, 250);
            Console.Beep(1760, 500);
            Console.Beep(1584, 250);
            Console.Beep(1408, 250);
            Console.Beep(1320, 750);
            Console.Beep(1056, 250);
            Console.Beep(1320, 500);
            Console.Beep(1188, 250);
            Console.Beep(1056, 250);
            Console.Beep(990, 500);
            Console.Beep(990, 250);
            Console.Beep(1056, 250);
            Console.Beep(1188, 500);
            Console.Beep(1320, 500);
            Console.Beep(1056, 500);
            Console.Beep(880, 500);
            Console.Beep(880, 500);
            Thread.Sleep(500);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 1000);
            Console.Beep(440, 1000);
            Console.Beep(419, 1000);
            Console.Beep(495, 1000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 500);
            Console.Beep(660, 500);
            Console.Beep(880, 1000);
            Console.Beep(838, 2000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 1000);
            Console.Beep(440, 1000);
            Console.Beep(419, 1000);
            Console.Beep(495, 1000);
            Console.Beep(660, 1000);
            Console.Beep(528, 1000);
            Console.Beep(594, 1000);
            Console.Beep(495, 1000);
            Console.Beep(528, 500);
            Console.Beep(660, 500);
            Console.Beep(880, 1000);
            Console.Beep(838, 2000);
        }

    }
}
