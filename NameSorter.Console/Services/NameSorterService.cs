using NameSorter.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Console.Services
{
    public sealed class NameSorterService
    {
        private readonly IComparer<PersonName> _comparer;

        public NameSorterService(IComparer<PersonName> comparer)
        {
            _comparer = comparer
                ?? throw new ArgumentNullException(nameof(comparer));
        }

        public IReadOnlyList<PersonName> Sort(
            IEnumerable<PersonName> names)
        {
            ArgumentNullException.ThrowIfNull(names);

            return names
                .OrderBy(name => name, _comparer)
                .ToList();
        }
    }
}
