namespace MDCWordFinder.Tests;

public class UnitTest1
{
    [Fact]
    public void Constructor_ShouldInitializeMatrixCorrectly()
    {
        // Arrange
        var inputMatrix = new List<string> { "hello", "world" };

        // Act
        var wordFinder = new WordFinder(inputMatrix);

        // Assert
        Assert.Equal(inputMatrix, wordFinder.Matrix); // Verify that the words are identical.
    }

    [Fact]
    public void Matrix_ShouldNotAllowExternalModification()
    {
        // Arrange
        var inputMatrix = new List<string> { "hello", "world" };
        var wordFinder = new WordFinder(inputMatrix);

        // Act
        var externalMatrix = wordFinder.Matrix;
        externalMatrix[0] = "modified"; // External modification.

        // Assert
        Assert.NotEqual("modified", wordFinder.Matrix[0]); // The internal matrix should remain unchanged.
    }

    [Fact]
    public void Constructor_ShouldCreateDefensiveCopy()
    {
        // Arrange
        var inputMatrix = new List<string> { "hello", "world" };
        var wordFinder = new WordFinder(inputMatrix);

        // Act
        inputMatrix[0] = "modified"; // Modify the input matrix after creation.

        // Assert
        Assert.NotEqual("modified", wordFinder.Matrix[0]); // The internal matrix must not change.
    }

    [Fact]
    public void Constructor_ShouldHandleEmptyMatrix()
    {
        // Arrange
        var inputMatrix = new List<string>();

        // Act
        var wordFinder = new WordFinder(inputMatrix);

        // Assert
        Assert.Empty(wordFinder.Matrix); // The matrix must be empty.
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenMatrixIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new WordFinder(null));
    }

    [Fact]
    public void Constructor_ShouldAcceptRegularMatrix()
    {
        // Arrange
        var validMatrix = new List<string> { "abcd", "efgh", "ijkl" };

        // Act
        var wordFinder = new WordFinder(validMatrix);

        // Assert
        Assert.Equal(validMatrix, wordFinder.Matrix);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenMatrixIsNotRegular()
    {
        // Arrange
        var irregularMatrix = new List<string> { "abc", "de", "fghi" };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new WordFinder(irregularMatrix));
        Assert.Equal("The provided matrix is not regular. All rows must have the same length.", exception.Message);
    }

    [Fact]
    public void Find_ShouldReturnEmpty_WhenWordStreamIsEmpty()
    {
        // Arrange
        var matrix = new List<string>
            {
                "abcd",
                "efgh",
                "ijkl",
                "mnop"
            };
        var wordFinder = new WordFinder(matrix);

        var wordstream = new List<string>();

        // Act
        var result = wordFinder.Find(wordstream);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Find_ShouldReturnCorrectWords_WhenWordsAreInMatrix()
    {
        // Arrange
        var matrix = new List<string>
            {
                "abcd",
                "efgh",
                "ijkl",
                "mnop"
            };
        var wordFinder = new WordFinder(matrix);

        var wordstream = new List<string> { "abcd", "efg", "kl", "mnop" };

        // Act
        var result = wordFinder.Find(wordstream).ToList();

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Contains("abcd", result);
        Assert.Contains("mnop", result);
        Assert.Contains("efg", result);
        Assert.Contains("kl", result);
    }

    [Fact]
    public void Find_ShouldHandleDuplicateWordsInWordStream()
    {
        // Arrange
        var matrix = new List<string>
    {
        "appleR",
        "banana",
        "grapeG",
        "appleH"
    };
        var wordFinder = new WordFinder(matrix);

        var wordstream = new List<string> { "apple", "banana", "apple", "grape", "BANANA", "BaNaNa", "BANana" }; // 'apple' and 'banana' repeated.

        // Act
        var result = wordFinder.Find(wordstream).ToList();

        // Assert
        // Each valid word is expected to be counted only once in the result.
        Assert.Equal(["apple", "banana", "grape"], result);
    }

    [Fact]
    public void Find_ShouldBeCaseInsensitive_With4x4Matrix()
    {
        // Arrange
        var matrix = new List<string>
    {
        "zRed",
        "SdBW",
        "UaaT",
        "nORa"
    };
        var wordFinder = new WordFinder(matrix);

        var wordstream = new List<string> { "suN", "baR", "reD" };

        // Act
        var result = wordFinder.Find(wordstream).ToList();

        // Assert
        Assert.Equal(["bar", "red", "sun"], result);
    }

    [Fact]
    public void Find_ShouldReturnWordsOrderedByCountDescendingAndAlphabetically2()
    {
        // Arrange
        var matrix = new List<string>
    {
        "ZHOCKEYS",
        "MTMBRXFP",
        "QNJVUOGQ",
        "JGBZGBOX",
        "FOOTBALL",
        "HLXPYKFI",
        "TFEHGOLF",
        "QRUGBYZG"
    };
        var wordFinder = new WordFinder(matrix);

        var wordstream = new List<string> { "BOX", "GOLF", "RUGBY", "FOOTBALL", "HOCKEY" };

        // Act
        var result = wordFinder.Find(wordstream).ToList();

        // Assert
        Assert.Equal(["golf", "box", "rugby", "football", "hockey"], result);
    }
}
