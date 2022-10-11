using System;
using System.Collections.Generic;
using System.Text;

namespace Diamond_Kata_Requirement_Test
{
    public static class LetterGenerator
    {
        static Random random = new Random();
        public static char GetLetter()
        {
            // This method returns a random letter
            // ... Between 'a' and 'z' inclusize.
            int num = random.Next(0, 26); // Zero to 25
            char let = (char)('A' + num);
            return let;
        }
    }
}
