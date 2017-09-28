using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace BizzLayer
{
    public static class ElectionFacade
    {
        public static List<Candidate> GetCandidates()
        {
            using (var db = new ElectionsEntities())
            {
                var query = from candidate in db.Candidates
                            select candidate;
                return query.ToList();
            }
        }

        public static void Vote(ref Electorate electorate, Candidate candidate)
        {
            using (var db = new ElectionsEntities())
            {
                Vote vote = new Vote()
                {
                    date = DateTime.Now,
                    idcandidate = candidate?.idcandidates,
                    valid = candidate != null ? (byte)1 : (byte)0,
                    withRights = 1,
                };
                db.Votes.Add(vote);

                electorate.voted = 1;
                ElectorateFacade.UpdateElector(electorate);

                db.SaveChanges();
            }
        }
    }
}
