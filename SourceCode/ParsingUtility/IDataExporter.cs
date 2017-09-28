using Model;

namespace ParsingUtility
{
    public interface IDataExporter
    {
        void ExportData(string fileName, StatisticsData statisticsData);
    }
}
