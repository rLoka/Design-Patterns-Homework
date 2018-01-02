namespace kgrlic_zadaca_3.Framework
{
    abstract class Controller : Observer
    {
        protected Model _model;
        protected View _view;

        public void Initialize(Model model, View view)
        {
            _model = model;
            _view = view;
            _model.Attach(this);
        }

        public abstract void HandleUserInput(string userInput);
    }
}
