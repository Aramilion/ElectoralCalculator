using System.Linq;
using DataLayer;

namespace BizzLayer
{
    public class ElectorateFacade
    {
        public static void UpdateElector(Electorate electorate)
        {
            using (var db = new ElectionsEntities())
            {
                var result = db.Electorates.SingleOrDefault(b => b.idelectorate == electorate.idelectorate);
                if (result != null)
                {                  
                    result.name = electorate.name;
                    result.surname = electorate.surname;
                    result.pesel = electorate.pesel;
                    result.voted = electorate.voted;
                    result.logged = electorate.logged;
                    db.SaveChanges();
                }
            }
        }

        public static Electorate GetElectorate(Electorate electorateTemplate)
        {
            using (var db = new ElectionsEntities())
            {
                var query = from b in db.Electorates
                            where b.pesel == electorateTemplate.pesel
                                && (electorateTemplate.name == null || b.name == electorateTemplate.name)
                                && (electorateTemplate.surname == null || b.surname == electorateTemplate.surname)
                            select b;
                return query.SingleOrDefault();
            }
        }

        public static Electorate AddNewElectorate(Electorate electorateTemplate)
        {
            using (var db = new ElectionsEntities())
            {
                Electorate check = new Electorate();
                check.pesel = electorateTemplate.pesel;
                if (GetElectorate(check) != null)
                {
                    //there already is electorate with provided pesel
                    return null;
                }
                db.Electorates.Add(electorateTemplate);
                db.SaveChanges();
                return electorateTemplate;
            }
        }
    }
}
