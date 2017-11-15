using kgrlic_zadaca_2.Places;

namespace kgrlic_zadaca_2.Algorithms
{
    abstract class AlgorithmFactory
    {
        public static AlgorithmFactory GetFactory(string algorithm)
        {
            switch (algorithm)
            {
                case "AlphabeticAlgorithm":
                    return new AlphabeticFactory();
                case "NumericAlgorithm":
                    return new NumericFactory();
                case "RandomAlgorithm":
                    return new RandomFactory();
                default:
                    return new AlphabeticFactory();
            }
        }

        public abstract Algorithm CreateAlgorithm(FOI foi);
    }
}
