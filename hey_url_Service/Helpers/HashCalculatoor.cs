using System;
using System.Collections.Generic;
using System.Text;

namespace hey_url_Service.Helpers
{
   public static  class HashCalculator
    {
        public static string CalculateHash(int Number )
        {
            string hash = Number.ToString("X");

            hash = hash.Replace("0", "G");
            hash = hash.Replace("1", "H");
            hash = hash.Replace("2", "I");
            hash = hash.Replace("3", "J");
            hash = hash.Replace("4", "K");
            hash = hash.Replace("5", "L");
            hash = hash.Replace("6", "M");
            hash = hash.Replace("7", "N");
            hash = hash.Replace("8", "O");
            hash = hash.Replace("9", "P");

            
            if (hash.Length<5)
            {
                for(int i=hash.Length; i<=4; i++)
                {
                    hash = "Q" + hash;
                }
            }
            
            return hash;

        }

    }
}
