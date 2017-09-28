using System.Security.Cryptography;
using System.Text;

namespace ParsingUtility
{
    public static class HashCreator
    {
        public static string GetHash(string input)
        {
            return GetHash(Encoding.UTF8.GetBytes(input));
        }

        public static string GetHash(byte[] input)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] hash = shaM.ComputeHash(input);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach(var b in hash)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
