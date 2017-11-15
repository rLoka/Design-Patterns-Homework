namespace kgrlic_zadaca_2.IO
{
    static class Converter
    {
        public static int? StringToInt(string textNumber, int? returnOnFaliure = null)
        {
            int number;
            if (int.TryParse(textNumber, out number))
            {
                return number;
            }
            return returnOnFaliure;
        }

        public static float? StringToFloat(string textNumber, float? returnOnFaliure = null)
        {
            float number;
            if (float.TryParse(textNumber, out number))
            {
                return number;
            }
            return returnOnFaliure;
        }
    }
}
