using NameSorter.Console.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests
{
    public class NameFileServiceTests
    {
        private readonly NameFileService _fileService = new();

        [Fact]
        public void ReadNames_ExistingFile_ReturnsAllNames()
        {
            var path = Path.GetTempFileName();

            try
            {
                File.WriteAllLines(path, new[]
                {
                "Janet Parsons",
                "Marin Alvarez"
            });

                var result = _fileService.ReadNames(path);

                Assert.Equal(2, result.Count());
                Assert.Contains("Janet Parsons", result);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void WriteNames_ExistingFile_OverwritesContent()
        {
            var path = Path.GetTempFileName();

            try
            {
                File.WriteAllText(path, "Old Content");

                _fileService.WriteNames(path, new[]
                {
                "Marin Alvarez",
                "Janet Parsons"
            });

                var result = File.ReadAllLines(path);

                Assert.Equal(new[]
                {
                "Marin Alvarez",
                "Janet Parsons"
            }, result);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void ReadNames_MissingFile_ThrowsFileNotFoundException()
        {
            var path = Path.Combine(
                Path.GetTempPath(),
                $"{Guid.NewGuid()}.txt");

            Assert.Throws<FileNotFoundException>(
                () => _fileService.ReadNames(path).ToList());
        }

        [Fact]
        public void WriteNames_EmptyCollection_CreatesEmptyFile()
        {
            var path = Path.GetTempFileName();

            try
            {
                _fileService.WriteNames(path, Array.Empty<string>());

                Assert.Empty(File.ReadAllLines(path));
            }
            finally
            {
                File.Delete(path);
            }
        }


        [Fact]
        public void ReadNames_EmptyFile_ReturnsEmptyCollection()
        {
            var path = Path.GetTempFileName();

            try
            {
                var result = _fileService.ReadNames(path);

                Assert.Empty(result);
            }
            finally
            {
                File.Delete(path);
            }
        }

    }
}
