# Name Sorter

A .NET command-line application that sorts names alphabetically by last name, followed by given names.

Built with an emphasis on clean code, separation of concerns, maintainability, and automated testing.

## Requirements

- .NET 8 SDK or later compatible SDK
- A text file containing one name per line
- Each name must have between one and three given names, followed by a surname

## Getting Started

Clone the repository and navigate to the solution directory.

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run --project NameSorter.Console -- ./unsorted-names-list.txt
```

The application prints the sorted names to the console and creates or overwrites `sorted-names-list.txt` in the current working directory.

### Example Input

```text
Janet Parsons
Adonis Julius Archer
Marin Alvarez
Beau Tristan Bentley
```

### Example Output

```text
Marin Alvarez
Adonis Julius Archer
Beau Tristan Bentley
Janet Parsons
```

## Design

The application separates responsibilities into focused components:

| Component | Responsibility |
|---|---|
| PersonName | Represents and validates a person's name |
| NameParser | Converts input strings into PersonName objects |
| NameComparer | Defines surname-first, given-name-second ordering |
| NameSorterService | Sorts names using an injected comparer |
| NameFileService | Handles reading and writing text files |
| Program | Coordinates execution, command-line arguments, and error handling |

The design applies SOLID principles where they provide practical value, particularly Single Responsibility and Dependency Inversion.

The built-in `IComparer<PersonName>` abstraction keeps sorting rules separate from the sorting operation.

## Sorting Rules

1. Compare surnames first.
2. When surnames match, compare given names from left to right.
3. If all shared given names match, the name with fewer given names comes first.
4. Comparisons are case-insensitive.
5. Duplicate names are preserved.

## Input Handling

- Blank lines are ignored.
- Extra whitespace between names is normalized.
- Invalid names are rejected with an error.
- The last whitespace-separated token is treated as the surname.
- Original spelling and capitalization are preserved.

## Testing

The solution uses xUnit for automated testing.

Run all tests with:

```bash
dotnet test
```

Tests cover:

- Sorting by surname and given names
- Names containing one to three given names
- Duplicate names and case-insensitive comparisons
- Invalid input and empty files
- File reading and overwriting
- Integration between application components
- Command-line execution
- Sorting 1,000 names

## Engineering Decisions

The application intentionally uses a lightweight design rather than introducing unnecessary frameworks or abstractions.

LINQ's built-in sorting functionality is used instead of implementing a custom sorting algorithm, allowing the code to focus on the domain-specific comparison rules.

Constructor injection is used for the comparer, while other dependencies are composed directly in the application entry point.

The goal is to keep the solution straightforward to understand, test, and extend.
