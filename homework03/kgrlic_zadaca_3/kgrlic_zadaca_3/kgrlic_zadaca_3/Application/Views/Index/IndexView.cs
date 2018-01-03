using kgrlic_zadaca_3.Application.Controllers.Index;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Views.Index
{
    class IndexView : View
    {
        public IndexView(Configuration configuration) : base(configuration) { }

        public override void MakeController()
        {
            _controller = new IndexController();
            _controller.Initialize(_model, this);
            _controller.OnViewLoaded();
        }
    }
}
