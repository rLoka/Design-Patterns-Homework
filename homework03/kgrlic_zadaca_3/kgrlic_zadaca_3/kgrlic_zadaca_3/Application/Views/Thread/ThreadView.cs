using kgrlic_zadaca_3.Application.Controllers.Thread;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using View = kgrlic_zadaca_3.MVCFramework.View;

namespace kgrlic_zadaca_3.Application.Views.Thread
{
    class ThreadView : View
    {
        public ThreadView(Configuration configuration) : base(configuration) { }

        public override void MakeController()
        {
            _controller = new ThreadController();
            _controller.Initialize(_model, this);
            _controller.OnViewLoaded();
        }
    }
}
