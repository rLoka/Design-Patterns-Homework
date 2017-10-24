using kgrlic_zadaca_1.Places;

namespace kgrlic_zadaca_1.Algorithms
{
    class RandomFactory : AlgorithmFactory
    {
        public override Algorithm CreateAlgorithm(FOI foi)
        {
            return new RandomAlgorithm(foi);
        }
    }
}
