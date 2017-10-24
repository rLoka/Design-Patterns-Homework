using System;
using kgrlic_zadaca_1.Places;

namespace kgrlic_zadaca_1.Algorithms
{
    class NumericFactory : AlgorithmFactory
    {
        public override Algorithm CreateAlgorithm(FOI foi)
        {
            return new NumericAlgorithm(foi);
        }
    }
}
