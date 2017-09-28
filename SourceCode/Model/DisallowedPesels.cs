using System.Collections.Generic;

namespace Model
{
    public class JsonDisallowedPesels
    {
        public DisallowedPesels disallowed;
        public JsonDisallowedPesels() { }
    }

    public class DisallowedPesels
    {
        public string publicationDate;
        public List<DisallowedPesel> person;
        public DisallowedPesels()
        {
            person = new List<DisallowedPesel>();
        }
    }

    public class DisallowedPesel
    {
        public string pesel;
    }
}
