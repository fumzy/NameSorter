using NameSorter.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests
{

    public class PersonNameTests
    {
        [Fact]
        public void Constructor_NoGivenNames_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new PersonName(Array.Empty<string>(), "Smith"));
        }

        [Fact]
        public void Constructor_MoreThanThreeGivenNames_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new PersonName(
                    new[] { "One", "Two", "Three", "Four" },
                    "Smith"));
        }

        [Fact]
        public void Constructor_EmptyLastName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new PersonName(new[] { "John" }, ""));
        }

        [Fact]
        public void Constructor_NullGivenNames_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PersonName(null!, "Smith"));
        }

        [Fact]
        public void Constructor_NullGivenName_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new PersonName(new[] { "John", null! }, "Smith"));
        }
    }

}
