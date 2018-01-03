using System.Collections.Generic;

namespace kgrlic_zadaca_3.MVCFramework
{
    abstract class Model
    {
        private List<Observer> _observers = new List<Observer>();
        protected List<string> _arguments;

        private volatile List<string> _data = new List<string>();

        public List<string> Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (Observer observer in _observers)
            {
                observer.Update();
            }
        }

        public Model(List<string> arguments = null)
        {
            _arguments = arguments;
        }

        public List<string> GetData()
        {
            return Data;
        }
        public abstract void Service(List<string> arguments = null);
    }
}
