using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.MVCFramework;

namespace kgrlic_zadaca_3
{
    static class Router
    {
        private static Configuration _configuration;

        public static void Initialize(Configuration configuration)
        {
            _configuration = configuration;
        }

        public static void HandleRequest(string request)
        {
            string key = new string(request.Take(2).ToArray());
            List<string> arguments = new List<string>();
            switch (key.ToUpper())
            {
                case "M ":
                    arguments.Add("M");
                    arguments.Add(request.Substring(2));
                    LoadView("Print", arguments);
                    break;
                case "S ":
                    arguments.Add("S");
                    arguments.Add(request.Substring(2));
                    LoadView("Print", arguments);
                    break;
                case "A ":
                    arguments.Add("A");
                    arguments.Add(request.Substring(2));
                    LoadView("Print", arguments);
                    break;
                case "S":
                    arguments.Add("S");
                    LoadView("Print", arguments);
                    break;
                case "SP":
                    arguments.Add("S");
                    LoadView("State", arguments);
                    break;
                case "VP":
                    arguments.Add("V");
                    LoadView("State", arguments);
                    break;
                case "C ":
                    arguments.Add(request.Substring(2));
                    LoadView("Thread", arguments);
                    break;
                case "VF":
                    arguments.Add("F");
                    LoadView("Print", arguments);
                    break;
                case "PI":
                    arguments.Add("P");
                    arguments.Add(request.Substring(3));
                    LoadView("Print", arguments);
                    break;
                case "H":
                    LoadView("Help");
                    break;
                case "I":
                    Process.GetCurrentProcess().Kill();
                    return;
                default:
                    arguments.Add("E");
                    arguments.Add(request);
                    LoadView("Print", arguments);
                    return;
            }
        }

        private static void LoadView(string viewName, List<string> arguments = null)
        {
            Model model = (Model) Activator.CreateInstance(Type.GetType("kgrlic_zadaca_3.Application.Models." + viewName + "." + viewName + "Model"), arguments);
            View view = (View) Activator.CreateInstance(Type.GetType("kgrlic_zadaca_3.Application.Views." + viewName + "." + viewName + "View"), _configuration);

            view.Initialize(model);
            model.Attach(view);

            view.MakeController();
            view.Activate();
        }
    }
}
