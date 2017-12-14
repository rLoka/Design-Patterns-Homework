using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace kgrlic_zadaca_3.IO
{
    partial class Output
    {
        private static volatile Output _instance;
        private IBufferObserver _bufferAllocatorObserver;
        private List<string> _buffer = new List<string>();
        private int _bufferLimit;

        private static string _welcome = "\r\n\t████████╗██╗  ██╗██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███████╗███████╗ ██████╗ ██╗██████╗ ;╚══██╔══╝██║  ██║██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗██╔════╝██╔════╝██╔═══██╗██║╚════██╗;   ██║   ███████║██║██╔██╗ ██║██║  ███╗███████╗██║   ██║█████╗  █████╗  ██║   ██║██║ █████╔╝;   ██║   ██╔══██║██║██║╚██╗██║██║   ██║╚════██║██║   ██║██╔══╝  ██╔══╝  ██║   ██║██║██╔═══╝ ;   ██║   ██║  ██║██║██║ ╚████║╚██████╔╝███████║╚██████╔╝██║     ██║     ╚██████╔╝██║███████╗;   ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝     ╚═╝      ╚═════╝ ╚═╝╚══════╝";
        private static string _help = "-g sjeme za generator slučajnog broja (u intervalu 100 - 65535);-m naziv datoteke mjesta;-s naziv datoteke senzora;-a naziv datoteke aktuatora;-alg puni naziv klase algoritma provjere koja se dinamički učitava;-tcd trajanje ciklusa dretve u sek.;-bcd broj ciklusa dretve;-i naziv datoteke u koju se sprema izlaz programa;-brl broj linija u spremniku za upis u datoteku za izlaz;--help pomoć za korištenje opcija u programu";

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

        public void SetUpOutput(string outputFilePath, int bufferLimit)
        {
            OutputFilePath = outputFilePath;

            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            _bufferLimit = bufferLimit;

            _bufferAllocatorObserver = new BufferAllocatorObserver(_buffer, OutputFilePath);
        }

        public void WriteLine(string line, bool isWarning = false)
        {
            if (isWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine("\t" + line);
            Console.ForegroundColor = ConsoleColor.White;

            _buffer.Add(line);

            if (_buffer.Count >= _bufferLimit && OutputFilePath != null)
            {
                Notify();
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

            if (beep)
            {
                new Thread(delegate()
                {
                    Thread.CurrentThread.IsBackground = true;
                    Beeper();
                }).Start();
            }

            Console.WriteLine("\r\n\t#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ + ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#");

            foreach (var line in lineSplit)
            {
                Console.WriteLine("\t" + line);
                Thread.Sleep(100);
            }

            Console.WriteLine("\r\n\t#~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ + ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#\r\n");

            Thread.Sleep(100);

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
