using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using kgrlic_zadaca_3.Configurations;
using kgrlic_zadaca_3.Devices;
using kgrlic_zadaca_3.IO;
using kgrlic_zadaca_3.Model;
using kgrlic_zadaca_3.Model.Help;
using kgrlic_zadaca_3.Model.Index;
using kgrlic_zadaca_3.Places;
using kgrlic_zadaca_3.View;
using kgrlic_zadaca_3.View.Help;
using kgrlic_zadaca_3.View.Index;

namespace kgrlic_zadaca_3
{
    class Program
    {
        static void Main(string[] args)
        {
            #if DEBUG
            ProcessStartInfo pi = new ProcessStartInfo(@"C:\Program Files\ConEmu\ConEmu\ConEmuC.exe", "/AUTOATTACH");
            pi.CreateNoWindow = false;
            pi.UseShellExecute = false;
            Console.WriteLine("Press Enter after attach succeeded");
            Process.Start(pi);
            Console.ReadLine();
            #endif
            if (ArgumentChecker.CheckArguments(args))
            {
                EntityLoader entityLoader = new EntityLoader();

                Program program = new Program();

                Configuration configuration = entityLoader.LoadConfiguration(args);

                if (!configuration.IsConfigurationValid())
                {
                    Console.WriteLine("Argumenti nisu valjani! Provjerite argumente!", true);
                    return;
                }

                IndexModel indexModel = new IndexModel();
                IndexView indexView = new IndexView(configuration);

                indexView.Initialize(indexModel);
                indexModel.Attach(indexView);

                indexView.MakeController();
                indexView.Activate();

                /*
                HelpModel helpModel = new HelpModel();
                HelpView helpView = new HelpView(configuration);

                helpView.Initialize(helpModel);
                helpModel.Attach(helpView);

                helpView.MakeController();
                helpView.Activate();
                */

                /*
                new RandomGeneratorFacade(configuration.GeneratorSeed ?? 0);

                ThingsOfFoi thingsOfFoi = entityLoader.LoadDevices(configuration);

                if (thingsOfFoi == null)
                {
                    return;
                }

                Foi foi = entityLoader.LoadPlaces(configuration, thingsOfFoi);

                if (foi == null)
                {
                    return;
                }

                List<Schedule> schedules = entityLoader.LoadSchedules(configuration);

                Initializer initializer = new Initializer(foi, thingsOfFoi, schedules, configuration);
                initializer.Initialize();
                
                program.CheckDevices(configuration, foi);
               */
            }
        }

        private void CheckDevices(Configuration configuration, Foi foi)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = false;
                Algorithm algorithm = new Algorithm(foi, configuration);

                for (int i = 0; i < 3; i++)
                {
                    algorithm.Run();
                }
                
                ShowStatistics(foi);

            }).Start();
        }

        private void ShowStatistics(Foi foi)
        {
            Output output = Output.GetInstance();

            foreach (var place in foi.Places)
            {
                output.WriteLine(place.ToString());
                output.WriteLine(">>>>>>>>>>>>>>> > UREĐAJI < <<<<<<<<<<<<<<<<<\r\n");

                foreach (var device in place.Devices)
                {
                    output.WriteLine(device.ToString());
                }
            }
        }
    }
}