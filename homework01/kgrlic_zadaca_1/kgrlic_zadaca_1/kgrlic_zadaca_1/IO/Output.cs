using System;
using System.IO;

namespace kgrlic_zadaca_1.IO
{
    class Output
    {
        private static volatile Output _instance;

        public string OutputFilePath;

        public static Output GetInstance(string outputFilePath = "output.txt")
        {
            if (_instance == null)
            {
                _instance = new Output {OutputFilePath = outputFilePath};
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
            }

            return _instance;
        }

        protected Output() { }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
            WriteToOutputFile(line);
        }

        private void WriteToOutputFile(string line)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(OutputFilePath, true))
            {
                file.WriteLine(line);
            }
        }

    }
}
