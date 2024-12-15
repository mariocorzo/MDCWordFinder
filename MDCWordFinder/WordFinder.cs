namespace MDCWordFinder;

public class WordFinder
{
    private const int TopCount = 10;
    private readonly List<string> _matrix;
    private readonly List<string> _transposedMatrix;

    public List<string> Matrix => [.. _matrix];

    public WordFinder(IEnumerable<string> matrix)
    {
        if (matrix == null)
            throw new ArgumentNullException(nameof(matrix), "Matrix cannot be null.");

        var matrixList = matrix.ToList();

        if (!IsRegularMatrix(matrixList))
            throw new ArgumentException("The provided matrix is not regular. All rows must have the same length.");

        _matrix = matrixList;
        this._transposedMatrix = MatrixTransformer.Transpose(matrixList).ToList();
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {

        // Remove duplicate words and convert all to lowercase.
        var uniqueWordstream = wordstream
            .Select(word => word.ToLowerInvariant()) // Ensure case-insensitivity.
            .Distinct()                             // Remove duplicates.
            .ToList();

        var wordCounts = uniqueWordstream.ToDictionary(word => word, _ => 0);

        // Count occurrences in horizontal and vertical rows in parallel.
        Parallel.ForEach(uniqueWordstream, word =>
        {
            int wordCount = 0;

            // Count in horizontal rows.
            foreach (var row in _matrix)
            {
                wordCount += CountOccurrences(row, word);
            }

            // Count in vertical rows (transposed).
            foreach (var col in _transposedMatrix)
            {
                wordCount += CountOccurrences(col, word);
            }

            // Use a thread-safe block to update the dictionary.
            lock (wordCounts)
            {
                wordCounts[word] += wordCount;
            }
        });

        return wordCounts
            .Where(kv => kv.Value > 0)        // Filter words with count > 0.
            .OrderByDescending(kv => kv.Value) // Sort by value in descending order.
            .ThenBy(kv => kv.Key)             // Break ties by sorting keys alphabetically.
            .Take(TopCount)                   // Select the first 'topCount' entries.
            .Select(kv => kv.Key);
    }


    private static bool IsRegularMatrix(List<string> matrix)
    {
        if (!matrix.Any()) return true; // An empty matrix is valid.

        var length = matrix[0].Length; // Length of the first row.
        return matrix.All(row => row.Length == length); // Validate that all rows have the same length.
    }

    private static int CountOccurrences(string text, string word)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(word))
            return 0;

        int count = 0;
        int index = text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
        while (index != -1)
        {
            count++;
            index = text.IndexOf(word, index + 1, StringComparison.OrdinalIgnoreCase);
        }
        return count;
    }
}
