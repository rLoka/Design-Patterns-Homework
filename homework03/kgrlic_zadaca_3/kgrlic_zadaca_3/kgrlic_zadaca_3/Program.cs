using System;
using System.Diagnostics;
using kgrlic_zadaca_3.Application.Entities.Configurations;
using kgrlic_zadaca_3.Application.Helpers;
using kgrlic_zadaca_3.Application.Models.Index;
using kgrlic_zadaca_3.Application.Views.Index;

namespace kgrlic_zadaca_3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ArgumentChecker.CheckArguments(args))
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilderImpl();

                ArgumentHandler argHandler = new DefaultHandler();

                for (int i = 0; i < args.Length; i += 2)
                {
                    argHandler.HandleArgument(new Tuple<string, string>(args[i], args[i + 1]), configurationBuilder);
                }

                ConfigurationBuildDirector configurationBuildDirector = new ConfigurationBuildDirector(configurationBuilder);
                Configuration configuration = configurationBuildDirector.Construct();

                if (!configuration.IsConfigurationValid())
                {
                    Console.WriteLine("Argumenti nisu valjani! Provjerite argumente!");
                    return;
                }

                Router.Initialize(configuration);

                IndexModel indexModel = new IndexModel();
                IndexView indexView = new IndexView(configuration);

                indexView.Initialize(indexModel);
                indexModel.Attach(indexView);

                indexView.MakeController();
                indexView.Activate();
            }
        }

    }
}