using System.Linq;
using kgrlic_zadaca_3.Configurations;
using kgrlic_zadaca_3.Controller.Help;

namespace kgrlic_zadaca_3.View.Help
{
    class HelpView : Framework.View
    {
        public HelpView(Configuration configuration) : base(configuration) { }

        public override void Update()
        {
            _textBuffer = _model.GetData().Split(';').ToList();
            Display();
        }

        public override void MakeController()
        {
            _controller = new HelpController();
            _controller.Initialize(_model, this);
        }
    }
}
