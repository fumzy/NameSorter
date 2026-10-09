using NameSorter.Console.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests
{
    public class NameParserTests
    {
        private readonly NameParser _parser = new();

        [Theory]
        [InlineData("Janet Parsons", 1, "Parsons")]
        [InlineData("Adonis Julius Archer", 2, "Archer")]
        [InlineData("Hunter Uriah Mathew Clarke", 3, "Clarke")]
        public void Parse_ValidName_ReturnsCorrectPerson(
            string input,
            int expectedGivenNames,
            string expectedLastName)
        {
            var result = _parser.Parse(input);

            Assert.Equal(expectedGivenNames, result.GivenNames.Count);
            Assert.Equal(expectedLastName, result.LastName);
        }

        [Fact]
        public void Parse_ExtraSpaces_ParsesCorrectly()
        {
            var result = _parser.Parse(
                "  Hunter   Uriah   Clarke  ");

            Assert.Equal("Clarke", result.LastName);
            Assert.Equal(
                new[] { "Hunter", "Uriah" },
                result.GivenNames);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Parse_EmptyName_ThrowsArgumentException(
            string input)
        {
            Assert.Throws<ArgumentException>(
                () => _parser.Parse(input));
        }

        [Fact]
        public void Parse_OnlyLastName_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(
                () => _parser.Parse("Smith"));
        }

        [Fact]
        public void Parse_TooManyGivenNames_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(
                () => _parser.Parse("One Two Three Four Five"));
        }


        [Theory]
        [InlineData(
            "Mary-Jane Watson",
            "Mary-Jane",
            "Watson")]
        [InlineData(
            "Sean O'Connor",
            "Sean",
            "O'Connor")]
        [InlineData(
            "Jean-Luc Picard",
            "Jean-Luc",
            "Picard")]
        public void Parse_NamesWithPunctuation_PreservesNames(
            string input,
            string expectedGivenName,
            string expectedLastName)
        {
            var result = _parser.Parse(input);

            Assert.Equal(expectedGivenName, result.GivenNames[0]);
            Assert.Equal(expectedLastName, result.LastName);
        }

        [Fact]
        public void Parse_TabsAndExtraSpaces_NormalizesWhitespace()
        {
            var result = _parser.Parse(
                "  John\t  Michael   Smith  ");

            Assert.Equal(
                "John Michael Smith",
                result.ToString());
        }

    }
}
