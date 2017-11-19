namespace kgrlic_zadaca_2.IO
{
    interface IOutputSubject
    {
        void Notify();
    }

    partial class Output : IOutputSubject
    {
        public void Notify()
        {
            _bufferAllocatorObserver.Update();
        }
    }
}
