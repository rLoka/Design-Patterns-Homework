using kgrlic_zadaca_2.Places;

namespace kgrlic_zadaca_2.Algorithms
{
    class RandomFactory : AlgorithmFactory
    {
        public override Algorithm CreateAlgorithm(FOI foi)
        {
            return new RandomAlgorithm(foi);
        }
    }
}
