using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3.Application.Controllers.Help
{
    class HelpController : Controller
    {
        public override void Update() { }

        public override void OnViewLoaded() { }

        public override void OnUserInput(string userInput)
        {
            Router.HandleRequest(userInput);
        }
    }
}
