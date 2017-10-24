using kgrlic_zadaca_1.Places;

namespace kgrlic_zadaca_1.Algorithms
{
    class AlphabeticFactory : AlgorithmFactory
    {
        public override Algorithm CreateAlgorithm(FOI foi)
        {
            return new AlphabeticAlgorithm(foi);
        }
    }
}
