using System;
using System.Collections.Generic;
using System.Text;

namespace Diamond_Kata_Requirement_Test
{
    public class DiamondKata
    {
        public static IEnumerable<string> Generate(char c)
        {
            if (Char.IsLower(c))
            {
                Console.WriteLine(MessageConstants.UserErrorInputMessage);
                return null;
            }
            else if (Char.IsDigit(c))
            {
                Console.WriteLine(MessageConstants.UserErrorInputMessage);
                return null;
            }
            return GenerateFinal(c);
        }

        private static IEnumerable<string> GenerateFinal(char c)
        {
            for (var i = 'A'; i < c; i++) yield return GenerateRow(i, c);
            for (var i = c; i >= 'A'; i--) yield return GenerateRow(i, c);
        }

        private static string GenerateRow(char currentChar, char maxChar)
        {
            var length = (maxChar - 'A' + 1) * 2 - 1;
            var padding = maxChar - currentChar;
            var sb = new StringBuilder();

            sb.Append(new string(' ', padding));
            sb.Append(currentChar);
            if (currentChar != 'A')
            {
                var insidePadding = length - padding * 2 - 2;
                sb.Append(new string(' ', insidePadding));
                sb.Append(currentChar);
            }

            sb.Append(new string(' ', padding));
            return sb.ToString();
        }
    }
}
