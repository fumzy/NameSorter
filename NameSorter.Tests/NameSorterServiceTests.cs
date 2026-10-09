using NameSorter.Console.Comparers;
using NameSorter.Console.Models;
using NameSorter.Console.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests
{
    public class NameSorterServiceTests
    {
        private readonly NameSorterService _sorter =
            new(new NameComparer());

        [Fact]
        public void Sort_UnsortedNames_ReturnsCorrectOrder()
        {
            var names = new[]
            {
            new PersonName(new[] { "Janet" }, "Parsons"),
            new PersonName(new[] { "Adonis", "Julius" }, "Archer"),
            new PersonName(new[] { "Marin" }, "Alvarez"),
            new PersonName(new[] { "Beau", "Tristan" }, "Bentley")
        };

            var result = _sorter.Sort(names);

            Assert.Equal(
                new[]
                {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Janet Parsons"
                },
                result.Select(name => name.ToString()));
        }

        [Fact]
        public void Sort_EmptyCollection_ReturnsEmptyCollection()
        {
            var result = _sorter.Sort(Array.Empty<PersonName>());

            Assert.Empty(result);
        }

        [Fact]
        public void Sort_DoesNotModifyOriginalCollection()
        {
            var names = new[]
            {
            new PersonName(new[] { "John" }, "Smith"),
            new PersonName(new[] { "Adam" }, "Adams")
        };

            _sorter.Sort(names);

            Assert.Equal("Smith", names[0].LastName);
            Assert.Equal("Adams", names[1].LastName);
        }

        [Fact]
        public void Sort_NullCollection_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _sorter.Sort(null!));
        }


        [Fact]
        public void Sort_OneThousandNames_ReturnsCorrectOrder()
        {
            var names = Enumerable.Range(1, 1000)
                .Reverse()
                .Select(i => new PersonName(
                    new[] { "John" },
                    $"Surname{i:D4}"))
                .ToList();

            var result = _sorter.Sort(names);

            Assert.Equal(1000, result.Count);
            Assert.Equal("Surname0001", result[0].LastName);
            Assert.Equal("Surname1000", result[^1].LastName);

            for (int i = 1; i < result.Count; i++)
            {
                Assert.True(
                    new NameComparer().Compare(
                        result[i - 1], result[i]) <= 0);
            }
        }


        [Fact]
        public void Sort_DuplicateNames_PreservesAllOccurrences()
        {
            var names = new[]
            {
        new PersonName(new[] { "John" }, "Smith"),
        new PersonName(new[] { "Adam" }, "Brown"),
        new PersonName(new[] { "John" }, "Smith")
    };

            var result = _sorter.Sort(names);

            Assert.Equal(3, result.Count);
            Assert.Equal(
                new[]
                {
            "Adam Brown",
            "John Smith",
            "John Smith"
                },
                result.Select(n => n.ToString()));
        }

        [Fact]
        public void Sort_SameSurnameAndFirstName_UsesThirdGivenName()
        {
            var names = new[]
            {
        new PersonName(
            new[] { "John", "Michael", "Peter" }, "Smith"),
        new PersonName(
            new[] { "John", "Michael", "Adam" }, "Smith")
    };

            var result = _sorter.Sort(names);

            Assert.Equal(
                "John Michael Adam Smith",
                result[0].ToString());
        }

    }
}
