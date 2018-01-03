using kgrlic_zadaca_3.Application.Models.Index;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Controllers.Index
{
    class IndexController : Controller
    {
        public override void Update() { }

        public override void OnViewLoaded()
        {
            ((IndexModel)_model).LoadEntities(_view.Configuration);
        }

        public override void OnUserInput(string userInput)
        {
            Router.HandleRequest(userInput);
        }

    }
}
