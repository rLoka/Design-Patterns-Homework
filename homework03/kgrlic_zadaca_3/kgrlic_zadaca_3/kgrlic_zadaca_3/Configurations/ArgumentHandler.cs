using System;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Configurations
{
    abstract class ArgumentHandler
    {
        protected ArgumentHandler _successor;

        public void SetSuccessor(ArgumentHandler successor)
        {
            _successor = successor;
        }

        public abstract void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder);
    }

    class DefaultHandler : ArgumentHandler
    {
        public DefaultHandler()
        {
            SetSuccessor(new NumberOfRowsHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            _successor.HandleArgument(argument, builder);
        }
    }

    class NumberOfRowsHandler : ArgumentHandler
    {
        public NumberOfRowsHandler()
        {
            SetSuccessor(new NumberOfColumnsHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-br")
            {
                builder.SetNumberOfRows(Converter.StringToInt(argument.Item2));
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class NumberOfColumnsHandler : ArgumentHandler
    {
        public NumberOfColumnsHandler()
        {
            SetSuccessor(new NumberOfInputRowsHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-bs")
            {
                builder.SetNumberOfColumns(Converter.StringToInt(argument.Item2));
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class NumberOfInputRowsHandler : ArgumentHandler
    {
        public NumberOfInputRowsHandler()
        {
            SetSuccessor(new AverageDeviceValidityHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-brk")
            {
                builder.SetNumberOfInputRows(Converter.StringToInt(argument.Item2));
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class AverageDeviceValidityHandler : ArgumentHandler
    {
        public AverageDeviceValidityHandler()
        {
            SetSuccessor(new GeneratorSeedHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-pi")
            {
                builder.SetAverageDeviceValidity(Converter.StringToInt(argument.Item2));
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class GeneratorSeedHandler : ArgumentHandler
    {
        public GeneratorSeedHandler()
        {
            SetSuccessor(new PlaceFilePathHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-g")
            {
                builder.SetGeneratorSeed(Converter.StringToInt(argument.Item2));
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class PlaceFilePathHandler : ArgumentHandler
    {
        public PlaceFilePathHandler()
        {
            SetSuccessor(new SensorsFilePathHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-m")
            {
                builder.SetPlaceFilePath(argument.Item2);
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class SensorsFilePathHandler : ArgumentHandler
    {
        public SensorsFilePathHandler()
        {
            SetSuccessor(new ActuatorsFilePathHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-s")
            {
                builder.SetSensorsFilePath(argument.Item2);
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class ActuatorsFilePathHandler : ArgumentHandler
    {
        public ActuatorsFilePathHandler()
        {
            SetSuccessor(new ScheduleFilePathHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-a")
            {
                builder.SetActuatorsFilePath(argument.Item2);
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class ScheduleFilePathHandler : ArgumentHandler
    {
        public ScheduleFilePathHandler()
        {
            SetSuccessor(new ThreadCycleDurationHandler());
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-r")
            {
                builder.SetScheduleFilePath(argument.Item2);
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class ThreadCycleDurationHandler : ArgumentHandler
    {
        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-tcd")
            {
                builder.SetThreadCycleDuration(Converter.StringToInt(argument.Item2));
            }
        }
    }
}