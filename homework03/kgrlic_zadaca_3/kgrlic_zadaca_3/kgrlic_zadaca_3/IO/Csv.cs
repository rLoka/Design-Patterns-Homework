using System.Collections.Generic;
using System.Linq;

namespace kgrlic_zadaca_3.IO
{
    static class Csv
    {
        public static List<Dictionary<string,string>> Parse(string csvFilePath, int headerRows = 1)
        {
            Output output = Output.GetInstance();
            List <Dictionary<string, string>> parsedDictionaryList = new List<Dictionary<string, string>>();
            string[] csvLines = System.IO.File.ReadAllLines(@csvFilePath);

            List<List<string>> headerList = new List<List<string>>();

            for (int i = 0; i < headerRows; i++)
            {
                headerList.Add(GetHeaderList(csvLines[i]));
            }

            for (int i = headerRows; i < csvLines.Length; i++)
            {
                Dictionary<string, string> rowDictionary = new Dictionary<string, string>();
                string[] lineSplit = csvLines[i].Split(';');

                if (headerList.Any(h => h.Count == lineSplit.Length))
                {
                    List<string> header = headerList.Find(h => h.Count == lineSplit.Length);

                    for (int j = 0; j < header.Count; j++)
                    {
                        rowDictionary.Add(header[j], lineSplit[j].Trim());
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
                headerStringArray.Add(headerStringSplit.Split(':')[0].Split('[')[0].Trim());
            }

            return headerStringArray;
        }
    }
}
