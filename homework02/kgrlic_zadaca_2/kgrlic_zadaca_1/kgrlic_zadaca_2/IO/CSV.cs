using System.Collections.Generic;

namespace kgrlic_zadaca_2.IO
{
    static class CSV
    {

        public static List<Dictionary<string,string>> Parse(string csvFilePath)
        {
            Output output = Output.GetInstance();
            List <Dictionary<string, string>> parsedDictionaryList = new List<Dictionary<string, string>>();
            string[] csvLines = System.IO.File.ReadAllLines(@csvFilePath);

            List<string> headerList = GetHeaderList(csvLines[0]);

            for (int i = 1; i < csvLines.Length; i++)
            {
                Dictionary<string, string> rowDictionary = new Dictionary<string, string>();
                string[] lineSplit = csvLines[i].Split(';');
                if (headerList.Count == lineSplit.Length)
                {
                    for (int j = 0; j < lineSplit.Length; j++)
                    {
                        rowDictionary.Add(headerList[j], lineSplit[j].Trim());
                    }

                    parsedDictionaryList.Add(rowDictionary);
                }
                else
                {
                    output.WriteLine("Greška u obradi retka: '" + csvLines[i] + "', GREŠKA: Zaglavlje CSV datoteke ne odgovara broju zapisa u retku! Preskačem!");
                }
            }

            return parsedDictionaryList;
        }

        private static List<string> GetHeaderList(string headerLine)
        {
            List<string> headerStringArray = new List<string>();

            string[] headerStringArraySplit = headerLine.Split(';');

            foreach (var headerStringSplit in headerStringArraySplit)
            {
                headerStringArray.Add(headerStringSplit.Split(':')[0].Trim());
            }

            return headerStringArray;
        }
    }
}
