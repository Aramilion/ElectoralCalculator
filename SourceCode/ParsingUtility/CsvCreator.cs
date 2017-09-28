using Model;
using System.IO;

namespace ParsingUtility
{
    public class CsvCreator : IDataExporter
    {
        public void ExportData(string fileName, StatisticsData statisticsData)
        {
            foreach (RawVotesData rawVotesData in statisticsData.rawData)
            {
                string newLine = string.Format("{0},{1},{2},{3},{4}\n", 
                    rawVotesData.date, 
                    rawVotesData.name, 
                    rawVotesData.party,
                    rawVotesData.valid,
                    rawVotesData.withRights
                    );
                File.AppendAllText(fileName, newLine.ToString(), System.Text.Encoding.UTF8);
            }
        }
    }
}
