using System;

namespace Model
{
    public class PeselData
    {
        private string pesel;
        public string Pesel
        {
            get
            {
                return pesel;
            }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
        }

        public Sex sex;
        public string SexName
        {
            get
            {
                return sex.ToString();
            }
        }

        public PeselData(string pesel, bool male, DateTime birthDate)
        {
            this.pesel = pesel;
            sex = male ? Sex.Male : Sex.Female;
            this.birthDate = birthDate;
        }
    }
}
