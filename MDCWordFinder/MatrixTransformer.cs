namespace MDCWordFinder;

public class MatrixTransformer
{
    public static IEnumerable<string> Transpose(IEnumerable<string> matrix)
    {

        // Check matrix
        if (matrix == null || !matrix.Any())
            return [];

        // Get the rows as a list to allow index-based access.
        var rowList = matrix.ToList();
        int rows = rowList.Count;
        int cols = rowList[0].Length;

        // Validate that all rows have the same length.
        if (!rowList.All(row => row.Length == cols))
            throw new ArgumentException("All rows must have the same length.");

        // Build the transposed rows directly.
        var transposed = new string[cols];
        for (int col = 0; col < cols; col++)
        {
            var newRow = new char[rows];
            for (int row = 0; row < rows; row++)
            {
                newRow[row] = rowList[row][col];
            }
            transposed[col] = new string(newRow);
        }

        return transposed;
    }
}