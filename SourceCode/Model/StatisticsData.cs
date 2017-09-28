using System.Collections.Generic;

namespace Model
{
    public class StatisticsData
    {
        public List<RawVotesData> rawData;

        public int validVotesNumber;
        public int invalidVotesNumber;
        public int withoutRightsVotesNumber;
        public Dictionary<DataLayer.Candidate, int> candidateVotes;
        public Dictionary<string, int> partyVotes;
    }
}
