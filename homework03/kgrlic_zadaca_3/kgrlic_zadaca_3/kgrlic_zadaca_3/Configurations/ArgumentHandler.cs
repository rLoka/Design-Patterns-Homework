using System;
using kgrlic_zadaca_3.IO;

namespace kgrlic_zadaca_3.Configurations
{
    abstract class ArgumentHandler
    {
        protected ArgumentHandler _successor;
        protected Output _output = Output.GetInstance();

        public void SetSuccessor(ArgumentHandler successor)
        {
            _successor = successor;
        }

        public abstract void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder);
    }

    class GeneratorSeedHandler : ArgumentHandler
    {
        public GeneratorSeedHandler()
        {
            ArgumentHandler successorHandler = new ThreadCycleDurationHandler();
            SetSuccessor(successorHandler);
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-g")
            {
                builder.SetSetGeneratorSeed(Converter.StringToInt(argument.Item2));
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class ThreadCycleDurationHandler : ArgumentHandler
    {
        public ThreadCycleDurationHandler()
        {
            ArgumentHandler successorHandler = new NumberOfThreadCyclesHandler();
            SetSuccessor(successorHandler);
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-tcd")
            {
                builder.SetThreadCycleDuration(Converter.StringToInt(argument.Item2));
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class NumberOfThreadCyclesHandler : ArgumentHandler
    {
        public NumberOfThreadCyclesHandler()
        {
            ArgumentHandler successorHandler = new PlaceFilePathHandler();
            SetSuccessor(successorHandler);
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-bcd")
            {
                builder.SetNumberOfThreadCycles(Converter.StringToInt(argument.Item2));
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
            ArgumentHandler successorHandler = new SensorsFilePathHandler();
            SetSuccessor(successorHandler);
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
            ArgumentHandler successorHandler = new ActuatorsFilePathHandler();
            SetSuccessor(successorHandler);
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
            ArgumentHandler successorHandler = new OutputFilePathHandler();
            SetSuccessor(successorHandler);
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

    class OutputFilePathHandler : ArgumentHandler
    {
        public OutputFilePathHandler()
        {
            ArgumentHandler successorHandler = new AlgorithmHandler();
            SetSuccessor(successorHandler);
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-i")
            {
                builder.SetOutputFilePath(argument.Item2);
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class AlgorithmHandler : ArgumentHandler
    {
        public AlgorithmHandler()
        {
            ArgumentHandler successorHandler = new NumberOfLinesHandler();
            SetSuccessor(successorHandler);
        }

        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-alg")
            {
                builder.SetAlgorithm(argument.Item2);
            }
            else
            {
                _successor.HandleArgument(argument, builder);
            }
        }
    }

    class NumberOfLinesHandler : ArgumentHandler
    {
        public override void HandleArgument(Tuple<string, string> argument, IConfigurationBuilder builder)
        {
            if (argument.Item1 == "-brl")
            {
                builder.SetNumberOfLines(Converter.StringToInt(argument.Item2));
            }
        }
    }
}
