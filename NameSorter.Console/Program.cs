
using NameSorter.Console.Comparers;
using NameSorter.Console.Models;
using NameSorter.Console.Services;

if (args.Length != 1)
{
    System.Console.Error.WriteLine(
        "Usage: name-sorter <input-file-path>");

    return 1;
}

try
{
    var fileService = new NameFileService();
    var parser = new NameParser();
    var comparer = new NameComparer();
    var sorter = new NameSorterService(comparer);

    // Read all names from the input file.
    var lines = fileService.ReadNames(args[0]);

    // Parse each line into a PersonName object.

    var names = new List<PersonName>();

    int lineNumber = 0;

    foreach (var line in lines)
    {
        lineNumber++;

        if (string.IsNullOrWhiteSpace(line))
            continue;

        try
        {
            names.Add(parser.Parse(line));
        }
        catch (ArgumentException ex)
        {
            throw new FormatException(
                $"Invalid name on line {lineNumber}: {ex.Message}", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException(
                $"Invalid name on line {lineNumber}: {ex.Message}", ex);
        }
    }


    // Sort by surname, then given names.
    var sortedNames = sorter.Sort(names);

    // Convert the sorted names back to strings.
    var output = sortedNames
        .Select(name => name.ToString())
        .ToList();

    // Print sorted names to the console.
    foreach (var name in output)
    {
        System.Console.WriteLine(name);
    }

    // Write output to the current working directory.
    var outputPath = Path.Combine(
        Directory.GetCurrentDirectory(),
        "sorted-names-list.txt");

    fileService.WriteNames(outputPath, output);

    return 0;
}
catch (FileNotFoundException)
{
    System.Console.Error.WriteLine(
        "Error: The specified input file was not found.");

    return 1;
}
catch (UnauthorizedAccessException)
{
    System.Console.Error.WriteLine(
        "Error: Access to the file was denied.");

    return 1;
}
catch (IOException ex)
{
    System.Console.Error.WriteLine(
        $"File error: {ex.Message}");

    return 1;
}
catch (ArgumentException ex)
{
    System.Console.Error.WriteLine(
        $"Invalid input: {ex.Message}");

    return 1;
}
catch (FormatException ex)
{
    System.Console.Error.WriteLine(
        $"Invalid name format: {ex.Message}");

    return 1;
}
