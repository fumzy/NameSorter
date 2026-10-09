using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Console.Services
{
    public sealed class NameFileService
    {
        public IEnumerable<string> ReadNames(string filePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

            return File.ReadAllLines(filePath);
        }

        public void WriteNames(
            string filePath,
            IEnumerable<string> names)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
            ArgumentNullException.ThrowIfNull(names);

            File.WriteAllLines(filePath, names);
        }
    }
}
