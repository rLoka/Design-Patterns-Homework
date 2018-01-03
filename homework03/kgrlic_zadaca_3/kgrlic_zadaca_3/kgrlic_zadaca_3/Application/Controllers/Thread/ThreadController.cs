using kgrlic_zadaca_3.Application.Models.Thread;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Controllers.Thread
{
    class ThreadController : Controller
    {
        public override void Update() { }

        public override void OnViewLoaded()
        {
            ((ThreadModel)_model).SpinTheThread(_view.Configuration);
        }

        public override void OnUserInput(string userInput)
        {
            Router.HandleRequest(userInput);
        }
    }
}
