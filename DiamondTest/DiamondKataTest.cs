using Diamond_Kata_Requirement_Test;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiamondTest
{
    public class DiamondKataTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Input_Value_NotEmpty()
        {
            char c = LetterGenerator.GetLetter();
            bool result = DiamondKata.Generate(c).All(s => s != string.Empty);

            Assert.IsTrue(result);
        }

        [Test]
        public void First_Line_Contains_A()
        {
            char c = LetterGenerator.GetLetter();
            bool result = DiamondKata.Generate(c).First().Contains('A');

            Assert.IsTrue(result);
        }

        [Test]
        public void Last_Line_Contains_A()
        {
            char c = LetterGenerator.GetLetter();
            bool result = DiamondKata.Generate(c).Last().Contains('A');

            Assert.AreEqual(result, true);
        }

        [Test]
        public void Spaces_Per_Row_Are_Symmetric()
        {
            char randomLetterGenarate = LetterGenerator.GetLetter();
            bool result = DiamondKata.Generate(randomLetterGenarate).All(row =>
                CountLeadingSpaces(row) == CountTrailingSpaces(row)
            );

            Assert.IsTrue(result);

        }

        [Test]
        public void Rows_Contain_Correct_Letter_In_Correct_Order()
        {
            char c = LetterGenerator.GetLetter();
            var expected = new List<char>();
            for (var i = 'A'; i < c; i++) expected.Add(i);

            for (var i = c; i >= 'A'; i--) expected.Add(i);

            var actual = DiamondKata.Generate(c).ToList().Select(GetCharInRow);
            bool result = actual.SequenceEqual(expected);

            Assert.IsTrue(result);
        }

        [Test]
        public void Diamond_Width_Equals_Height()
        {
            char c = LetterGenerator.GetLetter();
            var diamond = DiamondKata.Generate(c).ToList();
            bool result = diamond.All(row => row.Length == diamond.Count);

            Assert.IsTrue(result);
        }

        [Test]
        public void All_Rows_Except_First_And_Last_Contain_Two_Identical_Letters()
        {
            char c = LetterGenerator.GetLetter();

            bool result = false;

            if (c == 'A')
            {
                result = true;
            }
            else
            {
                var diamond = DiamondKata.Generate(c).ToArray()[1..^1];
                result = diamond.All(x =>
                {
                    var s = x.Replace(" ", string.Empty);
                    var b = s.Length == 2 && s.First() == s.Last();
                    return b;
                });
            }


            Assert.IsTrue(result);
        }

        [Test]
        public void Symmetric_Around_Horizontal_Axis()
        {
            char c = LetterGenerator.GetLetter();
            var diamond = DiamondKata.Generate(c).ToArray();
            var half = diamond.Length / 2;
            var topHalf = diamond[..half];
            var bottomHalf = diamond[(half + 1)..];
            bool result = topHalf.Reverse().SequenceEqual(bottomHalf);

            Assert.IsTrue(result);
        }

        [Test]
        public void Symmetric_Around_Vertical_Axis()
        {
            char c = LetterGenerator.GetLetter();
            bool result = DiamondKata.Generate(c).ToArray()
                .All(row =>
                {
                    var half = row.Length / 2;
                    var firstHalf = row[..half];
                    var secondHalf = row[(half + 1)..];

                    return firstHalf.Reverse().SequenceEqual(secondHalf);
                });

            Assert.IsTrue(result);
        }

        [Test]
        public void Input_Letter_Row_Contains_NoOutside_Padding_Spaces()
        {
            char c = LetterGenerator.GetLetter();
            var inputLetterRow = DiamondKata.Generate(c).ToArray().First(x => GetCharInRow(x) == c);
            bool result = (inputLetterRow[0] != ' ' && inputLetterRow[^1] != ' ');

            Assert.IsTrue(result);
        }

        [Test]
        public void PrintDiamond_With_Small_Letter_Input()
        {
            //Arrange
            char userInputletter = 'a';
            var writer = new StringWriter();
            Console.SetOut(writer);
            var sb = writer.GetStringBuilder();

            //Act
            DiamondKata.Generate(userInputletter);
            //Assert

            Assert.AreEqual("Please enter a capital letter from A to Z.", sb.ToString().Trim());
        }

        [Test]
        public void PrintDiamond_With_Interger_Input()
        {
            //Arrange
            char userInputletter = '1';
            var writer = new StringWriter();
            Console.SetOut(writer);
            var sb = writer.GetStringBuilder();

            //Act
            DiamondKata.Generate(userInputletter);
            //Assert

            Assert.AreEqual("Please enter a capital letter from A to Z.", sb.ToString().Trim());
        }

        #region Private Method
        private int CountLeadingSpaces(string s)
        {
            return s.IndexOf(GetCharInRow(s));
        }

        private static char GetCharInRow(string row)
        {
            return row.First(x => x != ' ');
        }

        private int CountTrailingSpaces(string s)
        {
            var i = s.LastIndexOf(GetCharInRow(s));
            return s.Length - i - 1;
        }
        #endregion
    }
}