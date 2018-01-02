using System;
using System.Collections.Generic;
using System.Linq;

namespace kgrlic_zadaca_3
{
    class Router
    {
        public void Route(string request)
        {
            switch (request.Take(2).ToString())
            {
                case "M ":
                    break;
                case "S ":
                    break;
                case "A ":
                    break;
                case "S":
                    break;
                case "SP":
                    break;
                case "VP":
                    break;
                case "C ":
                    break;
                case "VF":
                    break;
                case "PI":
                    break;
                case "H":
                    LoadView("Help");
                    break;
                case "I":
                    return;
                default:
                    break;
            }
        }

        private void LoadView(string viewName, List<string> arguments = null)
        {
            Framework.Model model = (Framework.Model) Activator.CreateInstance(Type.GetType("kgrlic_zadaca_3.Model." + viewName + "." + viewName + "Model"), arguments);
            Framework.View view = (Framework.View) Activator.CreateInstance(Type.GetType("kgrlic_zadaca_3.View." + viewName + "." + viewName + "View"));

            view.Initialize(model);
            model.Attach(view);

            view.MakeController();
            view.Activate();
        }
    }
}
