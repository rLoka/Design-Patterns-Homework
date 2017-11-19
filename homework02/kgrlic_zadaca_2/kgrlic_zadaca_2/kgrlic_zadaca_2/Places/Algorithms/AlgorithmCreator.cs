namespace kgrlic_zadaca_2.Places.Algorithms
{
    class AlgorithmCreator
    {
        public Algorithm CreateAlgorithm(string algorithm, Foi foi)
        {
            switch (algorithm)
            {
                case "AscendingIdentifierAlgorithm":
                    return new AscendingIdentifierAlgorithm(foi);
                case "DescendingIdentifierAlgorithm":
                    return new DescendingIdentifierAlgorithm(foi);
                case "RandomAlgorithm":
                    return new RandomAlgorithm(foi);
                case "SequentialAlgorithm":
                    return new SequentialAlgorithm(foi);
                default:
                    return new AscendingIdentifierAlgorithm(foi);
            }
        }
    }
}
