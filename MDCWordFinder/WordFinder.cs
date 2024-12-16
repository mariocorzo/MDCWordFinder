using System.Collections.Concurrent;

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
        _transposedMatrix = MatrixTransformer.Transpose(matrixList).ToList();
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        if (wordstream == null)
            return [];

        var wordCounts = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Remove duplicates, ensure case-insensitivity and convert the words to lowercase
        var uniqueWordstream = wordstream.Distinct(StringComparer.OrdinalIgnoreCase).Select(word => word.ToLowerInvariant()).ToList();

        Parallel.ForEach(uniqueWordstream, word =>
        {
            int wordCount = 0;

            // Count in horizontal rows
            foreach (var row in _matrix)
            {
                wordCount += CountOccurrences(row, word);
            }

            // Count in vertical rows
            foreach (var col in _transposedMatrix)
            {
                wordCount += CountOccurrences(col, word);
            }

            wordCounts.AddOrUpdate(word, wordCount, (key, oldValue) => oldValue + wordCount);
        });

        return wordCounts
            .Where(kv => kv.Value > 0)
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key)
            .Take(TopCount)
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
