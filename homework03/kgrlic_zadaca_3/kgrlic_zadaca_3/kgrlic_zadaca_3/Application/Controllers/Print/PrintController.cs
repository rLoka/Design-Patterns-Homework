using kgrlic_zadaca_3.Application.Models.Print;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Controllers.Print
{
    class PrintController : Controller
    {
        public override void Update() { }

        public override void OnViewLoaded()
        {
            ((PrintModel)_model).Print(_view.Configuration);
        }

        public override void OnUserInput(string userInput)
        {
            Router.HandleRequest(userInput);
        }
    }
}
