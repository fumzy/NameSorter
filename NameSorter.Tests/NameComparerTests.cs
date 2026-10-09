using NameSorter.Console.Comparers;
using NameSorter.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests
{
    public class NameComparerTests
    {
        private readonly NameComparer _comparer = new();

        [Fact]
        public void Compare_WhenLastNamesDiffer_SortsByLastName()
        {
            var archer = new PersonName(
                new[] { "Adonis" }, "Archer");

            var smith = new PersonName(
                new[] { "Adam" }, "Smith");

            var result = _comparer.Compare(archer, smith);

            Assert.True(result < 0);
        }

        [Fact]
        public void Compare_WhenLastNamesMatch_SortsByFirstGivenName()
        {
            var adam = new PersonName(
                new[] { "Adam", "James" }, "Smith");

            var john = new PersonName(
                new[] { "John", "Michael" }, "Smith");

            var result = _comparer.Compare(adam, john);

            Assert.True(result < 0);
        }

        [Fact]
        public void Compare_WhenFirstGivenNamesMatch_UsesNextGivenName()
        {
            var adamJames = new PersonName(
                new[] { "Adam", "James" }, "Smith");

            var adamMichael = new PersonName(
                new[] { "Adam", "Michael" }, "Smith");

            var result = _comparer.Compare(adamJames, adamMichael);

            Assert.True(result < 0);
        }

        [Fact]
        public void Compare_WhenSharedNamesMatch_ShorterNameComesFirst()
        {
            var shorter = new PersonName(
                new[] { "Adam" }, "Smith");

            var longer = new PersonName(
                new[] { "Adam", "James" }, "Smith");

            var result = _comparer.Compare(shorter, longer);

            Assert.True(result < 0);
        }

        [Fact]
        public void Compare_WhenNamesAreIdentical_ReturnsZero()
        {
            var first = new PersonName(
                new[] { "Adam" }, "Smith");

            var second = new PersonName(
                new[] { "Adam" }, "Smith");

            var result = _comparer.Compare(first, second);

            Assert.Equal(0, result);
        }

        [Fact]
        public void Compare_WhenCapitalizationDiffers_TreatsNamesEqually()
        {
            var first = new PersonName(
                new[] { "Adam" }, "Smith");

            var second = new PersonName(
                new[] { "ADAM" }, "SMITH");

            var result = _comparer.Compare(first, second);

            Assert.Equal(0, result);
        }
    }
}
