using System;
using System.Linq;

namespace kgrlic_zadaca_3.Application.Helpers
{
    internal class RandomGenerator
    {
        private static volatile RandomGenerator _instance;

        private Random _random;

        public static RandomGenerator GetInstance(int? generatorSeed = 0)
        {
            if (_instance == null)
            {
                _instance = new RandomGenerator {_random = new Random(generatorSeed ?? 0 + DateTime.Now.Millisecond) };
            }

            return _instance;
        }

        protected RandomGenerator() { }

        public int GetRandomInteger(int min, int max, int[] favourables = null, double? probability = 0.0)
        {

            if (favourables != null)
            {
                if (_random.NextDouble() < probability)
                {
                    return favourables[_random.Next(favourables.Length)]; ;
                }
                else
                {
                    int[] infavourables = Enumerable.Range(min, max).ToArray().Except(favourables).ToArray();
                    return infavourables[_random.Next(infavourables.Length)];
                }
            }

            return _random.Next(min, max);
        }

        public float GetRandomFloat(float min, float max)
        {
            return (float)_random.NextDouble() * (max - min) + min;
        }
    }
}
