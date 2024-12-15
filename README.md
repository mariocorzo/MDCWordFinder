# WordFinder

The **WordFinder** is a C# library designed to efficiently search for words in a matrix of characters, supporting both horizontal(from left to right) and vertical(from top to bottom) searches. It includes functionality to identify the top 10 most frequently occurring words from a given word stream. The project also includes unit tests using **xUnit** to ensure code quality and reliability.

## Features

- Search for words in both horizontal(from left to right) and vertical(from top to bottom) directions in a matrix.
- Count the frequency of words in a word stream.
- Return the top 10 most frequently found words, sorted by frequency and alphabetically (in case of ties).
- Ensures case-insensitivity and removes duplicate words from the input stream.
- Validates the matrix to ensure it is regular (all rows have the same length).

## Installation

The **WordFinder** library is published as a NuGet package, making it easy to integrate into your project. To install it, use the following command:

```bash
dotnet add package WordFinder

Alternatively, you can clone this repository and add the source files to your project.
git clone https://github.com/mariocorzo/wordfinder.git
```

## Usage

Below is an example of how to use the **WordFinder** class:

```csharp
using WordFinder;

var matrix = new List<string>
{
    "hello",
    "world",
    "apple",
    "bread",
    "cider"
};

var wordStream = new List<string> { "hello", "bread", "apple", "world", "world" };

var wordFinder = new WordFinder(matrix);
var topWords = wordFinder.Find(wordStream);

foreach (var word in topWords)
{
    Console.WriteLine(word);
}
```

### Output:
```
world
hello
apple
bread
```

## Unit Tests

This project includes unit tests written with **xUnit**. To run the tests:

1. Open the project in your favorite IDE (e.g., Visual Studio).
2. Build the solution to restore dependencies.
3. Run the tests using the Test Explorer.

Alternatively, you can run the tests from the command line:

```bash
dotnet test
```

The tests cover:

- Validation of the matrix (e.g., ensuring all rows are of the same length).
- Case-insensitive and duplicate word handling in the word stream.
- Correct ranking and sorting of the top 10 words.
- Edge cases such as empty inputs, irregular matrices, and missing words.

## Project Structure

- **`WordFinder/`**: Contains the core implementation of the `WordFinder` class.
- **`Tests/`**: Contains the xUnit test suite for the library.

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests with improvements, bug fixes, or new features.

### To contribute:
1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Commit your changes and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

Thanks for using and supporting the **WordFinder** project! If you have suggestions or feature requests, don't hesitate to reach out.

