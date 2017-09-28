using System;
using DataLayer;
using Model;

namespace BizzLayer
{
    public static class LoginFacade
    {
        public static bool? CheckVotingRights(PeselData peselData)
        {
            //check if user is over 18 years old
            DateTime dateTurns18 = peselData.BirthDate.AddYears(18);
            if (DateTime.Compare(dateTurns18, DateTime.Today) > 0)
            {
                return false;
            }
            //check if user is on blocked list
            const string blockedPeselsUrl = "http://webtask.future-processing.com:8069/blocked";
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    var json = wc.DownloadString(blockedPeselsUrl);
                    var disallowedPesels = ParsingUtility.JsonParser.GetData<JsonDisallowedPesels>(json);
                    DisallowedPesel mathingPesel = disallowedPesels.disallowed.person.Find(x => ParsingUtility.HashCreator.GetHash(x.pesel) == peselData.Pesel);
                    if (mathingPesel != null)
                    {
                        //user is on disallowed list
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return true;
        }

        public static void RegisterAttempToVoteWitoutRights()
        {
            using (var db = new ElectionsEntities())
            {
                Vote vote = new Vote()
                {
                    date = DateTime.Now,
                    idcandidate = null,
                    valid = 0,
                    withRights = 0,
                };
                db.Votes.Add(vote);
                db.SaveChanges();
            }
        }

        public static void RegisterLoginAttemp(string pesel, bool succesful, bool valid)
        {
            using (var db = new ElectionsEntities())
            {
                Loginattemp loginattemp = new Loginattemp()
                {
                    date = DateTime.Now,
                    pesel = pesel,
                    valid = (valid == true ? (byte)1 : (byte)0),
                    succesful = (succesful == true ? (byte)1 : (byte)0),
                };
                db.Loginattemps.Add(loginattemp);
                db.SaveChanges();
            }
        }

        public static bool TryLogin(ref Electorate electorate)
        {
            if (electorate.logged == 1)
            {
                //someone is loged alrady
                return false;
            }
            //set loged flag
            electorate.logged = 1;
            ElectorateFacade.UpdateElector(electorate);
            return true;
        }

        public static void LogOut(ref Electorate electorate)
        {
            if (electorate == null)
            {
                return;
            }
            //set loged flag
            electorate.logged = 0;
            ElectorateFacade.UpdateElector(electorate);
        }     
    }
}
