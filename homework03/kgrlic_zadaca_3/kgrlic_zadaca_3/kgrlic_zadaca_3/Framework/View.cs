using System;
using System.Collections.Generic;
using kgrlic_zadaca_3.Configurations;


namespace kgrlic_zadaca_3.Framework
{
    abstract class View : Observer
    {
        private static readonly string _esc = "\x1b[";

        protected Model _model;
        protected Controller _controller;

        private Configuration _configuration;

        private int _columnCounter;
        private int _rowCounter;
        private int _inputRowCounter;

        protected List<string> _textBuffer;

        public abstract void MakeController();

        public View(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void Initialize(Model model)
        {
            _model = model;
        }

        public void Activate()
        {
            Console.Write(_esc + "2J");
            Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows - 2) + ";0f");
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
            while (_rowCounter < _configuration.NumberOfDisplayRows)
            {
                if (_textBuffer.Count > 0)
                {
                    Console.Write(_textBuffer[0]);
                    _textBuffer.RemoveAt(0);
                    _rowCounter++;
                    Console.Write(_esc + _rowCounter + ";0f");
                }
                else
                {
                    Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows - 2) + ";0f");
                    Console.Write(new String('-', Console.WindowWidth));
                    Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows - 1) + ";0f");
                    Console.Write("Unesite neku od komandi (za help odaberite H):");
                    ReadLine();
                    return;
                }
            }
            Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows - 2) + ";0f");
            Console.Write(new String('-', Console.WindowWidth));
            Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows - 1) + ";0f");
            Console.Write("Za nastavak ispisa unesite n/N ili neku od komandi (za help odaberite H):");
            ReadKey();
        }

        protected void ReadKey()
        {
            Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows) + ";0f");

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
            Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows) + ";0f");

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
                Console.Write(_esc + (Console.WindowHeight - _configuration.NumberOfInputRows) + ";0f");
                
                Console.Write(line);

                input = Console.ReadKey().KeyChar;
            }

            _controller.HandleUserInput(line);
        }
    }
}
