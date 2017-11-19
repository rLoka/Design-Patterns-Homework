using System.Collections.Generic;

namespace kgrlic_zadaca_2.IO
{
    interface IBufferObserver
    {
        void Update();
    }

    class BufferAllocatorObserver : IBufferObserver
    {
        private List<string> _buffer;
        private string _filePath;

        public BufferAllocatorObserver(List<string> buffer, string filePath)
        {
            _buffer = buffer;
            _filePath = filePath;
        }

        public void Update()
        {
            WriteBufferToFile();
            EmptyBuffer();
        }

        private void WriteBufferToFile()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath, true))
            {
                foreach (var line in _buffer)
                {
                    file.WriteLine(line);
                }
            }
        }

        private void EmptyBuffer()
        {
            _buffer.Clear();
        }
    }
}
