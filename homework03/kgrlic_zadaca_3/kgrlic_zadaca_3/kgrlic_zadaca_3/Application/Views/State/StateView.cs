using kgrlic_zadaca_3.Application.Controllers.State;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Views.State
{
    class StateView : View
    {
        public StateView(Configuration configuration) : base(configuration) { }

        public override void MakeController()
        {
            _controller = new StateController();
            _controller.Initialize(_model, this);
            _controller.OnViewLoaded();
        }
    }
}
