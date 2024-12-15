namespace MDCWordFinder.Tests;

public class MatrixTransfomerTests
{
    [Fact]
    public void Transpose_ShouldReturnTransposedMatrix_WhenMatrixIsRegular()
    {
        // Arrange
        var inputMatrix = new List<string> { "abcd", "efgh", "ijkl" };
        var expectedMatrix = new List<string> { "aei", "bfj", "cgk", "dhl" };

        // Act
        var result = MatrixTransformer.Transpose(inputMatrix);

        // Assert
        Assert.Equal(expectedMatrix, result);
    }

    [Fact]
    public void Transpose_ShouldReturnEmptyMatrix_WhenMatrixIsNull()
    {
        // Act
        var result = MatrixTransformer.Transpose(null);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Transpose_ShouldThrowArgumentException_WhenMatrixIsIrregular()
    {
        // Arrange
        var inputMatrix = new List<string> { "abc", "de", "fgh" };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => MatrixTransformer.Transpose(inputMatrix));
        Assert.Equal("All rows must have the same length.", exception.Message);
    }

    [Fact]
    public void Transpose_ShouldReturnSingleColumnMatrix_WhenMatrixHasSingleRow()
    {
        // Arrange
        var inputMatrix = new List<string> { "abcd" };
        var expectedMatrix = new List<string> { "a", "b", "c", "d" };

        // Act
        var result = MatrixTransformer.Transpose(inputMatrix);

        // Assert
        Assert.Equal(expectedMatrix, result);
    }

    [Fact]
    public void Transpose_ShouldReturnSingleRowMatrix_WhenMatrixHasSingleColumn()
    {
        // Arrange
        var inputMatrix = new List<string> { "a", "b", "c", "d" };
        var expectedMatrix = new List<string> { "abcd" };

        // Act
        var result = MatrixTransformer.Transpose(inputMatrix);

        // Assert
        Assert.Equal(expectedMatrix, result);
    }

    [Fact]
    public void Transpose_ShouldHandleMatrixWithSpecialCharacters()
    {
        // Arrange
        var inputMatrix = new List<string> { "@#$", "!&*", "123" };
        var expectedMatrix = new List<string> { "@!1", "#&2", "$*3" };

        // Act
        var result = MatrixTransformer.Transpose(inputMatrix);

        // Assert
        Assert.Equal(expectedMatrix, result);
    }

    [Fact]
    public void Transpose_ShouldWorkCorrectly_ForSquareMatrix()
    {
        // Arrange
        var inputMatrix = new List<string> { "123", "456", "789" };
        var expectedMatrix = new List<string> { "147", "258", "369" };

        // Act
        var result = MatrixTransformer.Transpose(inputMatrix);

        // Assert
        Assert.Equal(expectedMatrix, result);
    }

    [Fact]
    public void Transpose_ShouldWorkCorrectly_ForRectangularMatrixHorizontal()
    {
        // Arrange
        var inputMatrix = new List<string> { "1234", "5678" };
        var expectedMatrix = new List<string> { "15", "26", "37", "48" };

        // Act
        var result = MatrixTransformer.Transpose(inputMatrix);

        // Assert
        Assert.Equal(expectedMatrix, result);
    }

    [Fact]
    public void Transpose_ShouldWorkCorrectly_ForRectangularMatrixVertical()
    {
        // Arrange
        var inputMatrix = new List<string> { "12", "34", "56", "78" };
        var expectedMatrix = new List<string> { "1357", "2468" };

        // Act
        var result = MatrixTransformer.Transpose(inputMatrix);

        // Assert
        Assert.Equal(expectedMatrix, result);
    }

}