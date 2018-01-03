namespace kgrlic_zadaca_3.Application.Helpers
{
    class RandomGeneratorFacade
    {
        private RandomGenerator _randomGenerator;

        public RandomGeneratorFacade(int seed)
        {
            _randomGenerator = RandomGenerator.GetInstance(seed);
        }

        public RandomGeneratorFacade()
        {
            _randomGenerator = RandomGenerator.GetInstance();
        }

        //public int dajSlucajniBroj(int odBroja, int doBroja)
        public int GiveRandomNumber(int fromNumber, int toNumber)
        {
            return _randomGenerator.GetRandomInteger(fromNumber, toNumber);
        }

        //public int dajSlucajniBroj(int odBroja, int doBroja, int[] pozeljniBrojevi, double vjerojatnostDobivanjaPozeljnihBrojeva)
        public int GiveRandomNumber(int fromNumber, int toNumber, int[] desirableNumbers, double? probabilityOfGettingDesirableNumbers)
        {
            return _randomGenerator.GetRandomInteger(fromNumber, toNumber, desirableNumbers, probabilityOfGettingDesirableNumbers);
        }

        //public float dajSlucajniBroj(float odBroja, float doBroja)
        public float GiveRandomNumber(float fromNumber, float toNumber)
        {
            return _randomGenerator.GetRandomFloat(fromNumber, toNumber);
        }
    }
}
