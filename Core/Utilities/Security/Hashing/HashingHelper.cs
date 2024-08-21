using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void sifreHashingOlustur(string sifre,out byte[] sifreHash,out byte[] sifreSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                sifreSalt = hmac.Key;
                sifreHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(sifre));
            }
        }

        public static bool sifreHasingKontrol(string sifre,byte[] sifreHash,byte[] sifreSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(sifreSalt))
            {
                var hesaplananHasing= hmac.ComputeHash(Encoding.UTF8.GetBytes(sifre));
                for (int i = 0;i< hesaplananHasing.Length;i++) 
                {
                    if (hesaplananHasing[i]!= sifreHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
