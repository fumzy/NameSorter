using NameSorter.Console.Comparers;
using NameSorter.Console.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests.Integration
{
    public class NameSortingIntegrationTests
    {
        [Fact]
        public void ProcessNames_ValidFile_WritesCorrectSortedOutput()
        {
            var directory = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString());

            Directory.CreateDirectory(directory);

            var inputPath = Path.Combine(
                directory, "unsorted-names-list.txt");

            var outputPath = Path.Combine(
                directory, "sorted-names-list.txt");

            try
            {
                File.WriteAllLines(inputPath, new[]
                {
                "Janet Parsons",
                "Adonis Julius Archer",
                "Marin Alvarez",
                "",
                "Beau Tristan Bentley",
                "John Smith",
                "Adam Smith"
            });

                var fileService = new NameFileService();
                var parser = new NameParser();
                var sorter = new NameSorterService(
                    new NameComparer());

                // Read the input file.
                var lines = fileService.ReadNames(inputPath);

                // Parse all non-empty lines.
                var names = lines
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(parser.Parse)
                    .ToList();

                // Sort the names.
                var sortedNames = sorter.Sort(names);

                // Write the sorted results.
                fileService.WriteNames(
                    outputPath,
                    sortedNames.Select(name => name.ToString()));

                var actual = File.ReadAllLines(outputPath);

                Assert.Equal(new[]
                {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Janet Parsons",
                "Adam Smith",
                "John Smith"
            }, actual);
            }
            finally
            {
                Directory.Delete(directory, recursive: true);
            }
        }
    }
}
