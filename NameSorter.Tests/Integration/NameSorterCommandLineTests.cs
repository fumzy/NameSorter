using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests.Integration
{
    public class NameSorterCommandLineTests
    {
        [Fact]
        public async Task Run_ValidInput_PrintsAndWritesSortedNames()
        {
            var directory = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid().ToString());

            Directory.CreateDirectory(directory);

            try
            {
                var inputPath = Path.Combine(
                    directory, "unsorted-names-list.txt");

                File.WriteAllLines(inputPath, new[]
                {
                "Janet Parsons",
                "Marin Alvarez",
                "Adonis Julius Archer"
            });

                var projectPath = Path.GetFullPath(
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "..", "..", "..", "..",
                        "NameSorter.Console",
                        "NameSorter.Console.csproj"));

                var startInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    WorkingDirectory = directory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                startInfo.ArgumentList.Add("run");
                startInfo.ArgumentList.Add("--no-launch-profile");
                startInfo.ArgumentList.Add("--project");
                startInfo.ArgumentList.Add(projectPath);
                startInfo.ArgumentList.Add("--");
                startInfo.ArgumentList.Add(inputPath);

                using var process = Process.Start(startInfo)
                    ?? throw new InvalidOperationException(
                        "Could not start the name sorter.");

                var stdoutTask = process.StandardOutput.ReadToEndAsync();
                var stderrTask = process.StandardError.ReadToEndAsync();

                using var timeout = new CancellationTokenSource(
                    TimeSpan.FromSeconds(30));

                await process.WaitForExitAsync(timeout.Token);

                var stdout = await stdoutTask;
                var stderr = await stderrTask;

                Assert.True(
                    process.ExitCode == 0,
                    $"Exit code: {process.ExitCode}. Error: {stderr}");

                var expected = new[]
                {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Janet Parsons"
            };

                var outputPath = Path.Combine(
                    directory, "sorted-names-list.txt");

                Assert.True(File.Exists(outputPath));

                Assert.Equal(
                    expected,
                    File.ReadAllLines(outputPath));

                Assert.Equal(
                    expected,
                    stdout.Split(
                        new[] { "\r\n", "\n" },
                        StringSplitOptions.RemoveEmptyEntries));
            }
            finally
            {
                Directory.Delete(directory, recursive: true);
            }
        }
    }
}
