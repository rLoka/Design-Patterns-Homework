using kgrlic_zadaca_3.Application.Models.State;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Controllers.State
{
    class StateController : Controller
    {
        public override void Update() { }

        public override void OnViewLoaded()
        {
            ((StateModel)_model).Execute();
        }

        public override void OnUserInput(string userInput)
        {
            Router.HandleRequest(userInput);
        }
    }
}
