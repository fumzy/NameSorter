using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Console.Models
{
    public sealed class PersonName
    {
        public IReadOnlyList<string> GivenNames { get; }

        public string LastName { get; }

        public PersonName(
            IEnumerable<string> givenNames,
            string lastName)
        {
            ArgumentNullException.ThrowIfNull(givenNames);

            var names = givenNames.ToArray();

            if (names.Length < 1 || names.Length > 3)
            {
                throw new ArgumentException(
                    "A person must have between 1 and 3 given names.",
                    nameof(givenNames));
            }

            if (names.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException(
                    "Given names cannot be empty.",
                    nameof(givenNames));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException(
                    "Last name cannot be empty.",
                    nameof(lastName));
            }

            GivenNames = Array.AsReadOnly(names);
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{string.Join(" ", GivenNames)} {LastName}";
        }
    }
}
