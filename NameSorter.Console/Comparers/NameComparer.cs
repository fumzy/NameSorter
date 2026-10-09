using NameSorter.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Console.Comparers
{
    public sealed class NameComparer : IComparer<PersonName>
    {
        public int Compare(PersonName? x, PersonName? y)
        {
            if (ReferenceEquals(x, y))
                return 0;

            if (x is null)
                return -1;

            if (y is null)
                return 1;

            // First compare last names.
            int result = StringComparer.OrdinalIgnoreCase.Compare(
                x.LastName,
                y.LastName);

            if (result != 0)
                return result;

            // If last names match, compare given names
            // from left to right.
            int commonCount = Math.Min(
                x.GivenNames.Count,
                y.GivenNames.Count);

            for (int i = 0; i < commonCount; i++)
            {
                result = StringComparer.OrdinalIgnoreCase.Compare(
                    x.GivenNames[i],
                    y.GivenNames[i]);

                if (result != 0)
                    return result;
            }

            // If all shared given names match,
            // the shorter name comes first.
            return x.GivenNames.Count.CompareTo(y.GivenNames.Count);
        }
    }
}
