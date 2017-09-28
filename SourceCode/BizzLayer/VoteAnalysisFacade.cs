using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace BizzLayer
{
    public static class VoteAnalysisFacade
    {
        public static int GetNumberOfVotesForCandidate(Candidate candidate)
        {
            using (var db = new ElectionsEntities())
            {
                var query = from vote in db.Votes
                            where vote.idcandidate == candidate.idcandidates
                                && vote.valid == 1
                                && vote.withRights == 1
                            select vote;
                return query.Count();
            }
        }

        public static Dictionary<Candidate, int> GetNumberOfVotesForCandidates()
        {
            using (var db = new ElectionsEntities())
            {
                var query = from candidate in db.Candidates.DefaultIfEmpty()
                             join vote in db.Votes.DefaultIfEmpty() on candidate.idcandidates equals vote.idcandidate into x
                             group x by candidate into g
                             orderby g.Key.Votes.Count descending
                             select new
                             {
                                 g.Key,
                                 Quantity = g.Key.Votes.Count
                             };
                Dictionary <Candidate, int> candidateVotes = new Dictionary<Candidate, int>();
                foreach (var entry in query)
                {
                     candidateVotes.Add(entry.Key, entry.Quantity);
                }
                return candidateVotes;
            }
        }

        public static Dictionary<string, int> GetNumberOfVotesForParties()
        {
            using (var db = new ElectionsEntities())
            {
                var query = from candidate in db.Candidates.DefaultIfEmpty()
                            join vote in db.Votes.DefaultIfEmpty() on candidate.idcandidates equals vote.idcandidate
                            select new { candidate.party, vote.valid } into s
                            group s by s.party into g
                            orderby g.Count() descending
                            select new
                            {
                                g.Key,
                                Quantity = g.Count()
                            };
                Dictionary<string, int> candidateVotes = new Dictionary<string, int>();
                foreach (var entry in query)
                {
                    candidateVotes.Add(entry.Key, entry.Quantity);
                }
                return candidateVotes;
            }
        }

        public static int GetVotesNumber(bool valid)
        {
            using (var db = new ElectionsEntities())
            {
                var query = from vote in db.Votes
                            where vote.valid == (valid ? 1 : 0) 
                                && vote.withRights == 1
                            group vote by vote.valid into g
                            select g.Count();

                return query.SingleOrDefault();
            }
        }

        public static int GetVotesWithoutRightsNumber()
        {
            using (var db = new ElectionsEntities())
            {
                var query = from vote in db.Votes
                            where vote.withRights == 0
                            group vote by vote.valid into g
                            select g.Count();

                return query.SingleOrDefault();
            }
        }

        public static List<Model.RawVotesData> GetVotesAllData()
        {
            using (var db = new ElectionsEntities())
            {
                var query = from candidate in db.Candidates.DefaultIfEmpty()
                            join vote in db.Votes.DefaultIfEmpty() on candidate.idcandidates equals vote.idcandidate
                            select new { vote.date, vote.Candidate.name, vote.Candidate.party, vote.valid, vote.withRights };
                List<Model.RawVotesData> rawVotesDataList = new List<Model.RawVotesData>();
                foreach (var entry in query)
                {
                    rawVotesDataList.Add(new Model.RawVotesData()
                    {
                        date = entry.date,
                        name = entry.name,
                        party = entry.party,
                        valid = entry.valid,
                        withRights = entry.withRights,
                    });
                }
                return rawVotesDataList;
            }
        }
    }
}
