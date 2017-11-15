using kgrlic_zadaca_2.Places;

namespace kgrlic_zadaca_2.Algorithms
{
    class NumericFactory : AlgorithmFactory
    {
        public override Algorithm CreateAlgorithm(FOI foi)
        {
            return new NumericAlgorithm(foi);
        }
    }
}
