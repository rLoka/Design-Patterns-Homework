using System;
using System.Collections.Generic;

namespace kgrlic_zadaca_3.Framework
{
    abstract class Model
    {
        private List<Observer> _observers = new List<Observer>();
        protected List<string> _arguments;

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

        protected Model(List<string> arguments = null)
        {
            _arguments = arguments;
        }

        public abstract string GetData();
        public abstract void Service(List<string> arguments);
    }
}
