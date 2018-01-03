using kgrlic_zadaca_3.Application.Controllers.Print;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Views.Print
{
    class PrintView : View
    {
        public PrintView(Configuration configuration) : base(configuration) { }

        public override void MakeController()
        {
            _controller = new PrintController();
            _controller.Initialize(_model, this);
            _controller.OnViewLoaded();
        }
    }
}
