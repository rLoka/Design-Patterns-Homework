using System;

namespace kgrlic_zadaca_3.IO
{
    static class ArgumentChecker
    {
        public static bool CheckArguments(string[] args)
        {
            if (args.Length > 20 || (args.Length < 4 && args.Length != 1))
            {
                Console.WriteLine("Pogresan broj argumenata!");
                return false;
            }
            if (args.Length == 1 && args[0] == "--help")
            {
                //Console.HelpUser();
                return false;
            }
            if (args.Length == 1 && args[0] != "--help")
            {
                Console.WriteLine("Argumenti nisu valjani! Provjerite argumente!");
                return false;
            }
            if (args.Length % 2 != 0)
            {
                Console.WriteLine("Broja argumenata mora biti paran!");
                return false;
            }
            return true;
        }
    }
}
