using NameSorter.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Console.Services
{
    public sealed class NameParser
    {
        public PersonName Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                throw new ArgumentException(
                    "Name cannot be empty.",
                    nameof(line));
            }

            var parts = line.Split(
                (char[]?)null,
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2 || parts.Length > 4)
            {
                throw new FormatException(
                    "A name must contain 1 to 3 given names and a last name.");
            }

            var lastName = parts[^1];
            var givenNames = parts[..^1];

            return new PersonName(givenNames, lastName);
        }
    }
}
