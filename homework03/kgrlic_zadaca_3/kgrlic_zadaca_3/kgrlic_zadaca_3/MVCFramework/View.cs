using System;
using System.Collections.Generic;
using System.Threading;
using kgrlic_zadaca_3.Application.Entities.Configurations;

namespace kgrlic_zadaca_3.MVCFramework
{
    abstract class View : Observer
    {
        private static readonly string _esc = "\x1b[";

        protected Model _model;
        protected Controller _controller;

        public Configuration Configuration;

        private int _columnCounter;
        private int _rowCounter;
        private int _inputRowCounter;
        private bool _bufferEmpty;

        protected List<string> _textBuffer = new List<string>();

        public abstract void MakeController();

        public View(Configuration configuration)
        {
            Configuration = configuration;
        }

        public override void Update()
        {
            _textBuffer = _model.GetData();
            Display();
        }

        public void Initialize(Model model)
        {
            _model = model;
        }

        public void Activate()
        {
            Console.Write(_esc + "2J");
            Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows - 2) + ";0f");
            Console.Write(new String('-', Console.WindowWidth));
            Console.Write(_esc + "0;0f");
            Update();

        }

        protected void ClearDisplay()
        {
            _rowCounter = 0;
            Console.Write(_esc + "1J");
            Console.Write(_esc + "0;0f");
        }

        protected void Display()
        {
            while (_rowCounter < Configuration.NumberOfDisplayRows)
            {
                if (_textBuffer.Count > 0)
                {
                    Console.Write(_esc + _rowCounter + ";0f");
                    Console.Write(_textBuffer[0]);
                    _textBuffer.RemoveAt(0);
                    _rowCounter++;
                }
                else
                {
                    Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows - 2) + ";0f");
                    Console.Write(new String('-', Console.WindowWidth));
                    Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows - 1) + ";0f");
                    Console.Write("Unesite neku od komandi (za help odaberite H):");
                    _bufferEmpty = true;
                    ReadLine();
                    return;
                }
            }
            Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows - 2) + ";0f");
            Console.Write(new String('-', Console.WindowWidth));
            Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows - 1) + ";0f");
            Console.Write("Za nastavak ispisa unesite n/N ili neku od komandi (za help odaberite H):");
            _bufferEmpty = false;
            ReadKey();
        }

        protected void ReadKey()
        {
            Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows) + ";0f");

            char key = Console.ReadKey(true).KeyChar;
            
            if (key == 'n' || key == 'N')
            {
                ClearDisplay();
                Display();
            }
            else
            {
                ReadLine(key);
            }
        }

        protected void ReadLine(char? key = null)
        {
            Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows) + ";0f");

            string line = "";
            char? input = key;

            while (input != '\r')
            {
                if ((input == 'n' || input == 'N') && line.Length == 0 && key != null)
                {
                    ClearDisplay();
                    Display();
                    return;
                }

                if (input == '\b')
                {
                    if (line.Length > 0)
                    {
                        line = line.Remove(line.Length - 1, 1);
                    }
                }
                else
                {
                    line += input;
                }

                Console.Write(_esc + "2K");
                Console.Write(_esc + (Console.WindowHeight - Configuration.NumberOfInputRows) + ";0f");
                
                Console.Write(line);

                if (_bufferEmpty)
                {
                    while (!Console.KeyAvailable)
                    {
                        if (_textBuffer.Count > 0)
                        {
                            Display();
                            Console.Write(_esc + "2K");
                            return;
                        }

                        Thread.Sleep(100);
                    }
                }

                input = Console.ReadKey().KeyChar;
            }
            ClearDisplay();
            _controller.OnUserInput(line);
        }
    }
}
