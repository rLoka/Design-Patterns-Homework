using kgrlic_zadaca_3.Application.Controllers.Help;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Views.Help
{
    class HelpView : View
    {
        public HelpView(Configuration configuration) : base(configuration) { }

        public override void MakeController()
        {
            _controller = new HelpController();
            _controller.Initialize(_model, this);
            _controller.OnViewLoaded();
        }
    }
}
