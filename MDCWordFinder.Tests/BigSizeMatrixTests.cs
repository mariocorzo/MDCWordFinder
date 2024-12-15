namespace MDCWordFinder.Tests
{
    public class BigSizeMatrixTests
    {
        [Fact]
        public void Find_ShouldReturnCorrectWords_From64x64Matrix()
        {
            // Arrange: Create a 64x64 matrix.
            var matrix = GenerateMatrix(64, 64);

            // Add some known words to verify the result.
            var knownWords = new List<string>
            {
                "hello", // horizontal
                "world", // vertical
                "test"   // Not found in the matrix.
            };

            // Insert the known words.
            InsertWordHorizontally(matrix, "hello", row: 5, startColumn: 10);
            InsertWordVertically(matrix, "world", column: 20, startRow: 15);

            var wordFinder = new WordFinder(matrix);

            // Act: Call the `Find` method.
            var result = wordFinder.Find(knownWords);

            // Assert: Validate the result.
            Assert.Contains("hello", result);
            Assert.Contains("world", result);
            Assert.DoesNotContain("test", result);
        }

        private static List<string> GenerateMatrix(int rows, int columns)
        {
            var random = new Random();
            var matrix = new List<string>();

            for (int i = 0; i < rows; i++)
            {
                var row = new char[columns];
                for (int j = 0; j < columns; j++)
                {
                    row[j] = (char)random.Next('a', 'z' + 1); // Random character from 'a' to 'z'.
                }
                matrix.Add(new string(row));
            }

            return matrix;
        }

        private static void InsertWordHorizontally(List<string> matrix, string word, int row, int startColumn)
        {
            var charArray = matrix[row].ToCharArray();
            for (int i = 0; i < word.Length; i++)
            {
                charArray[startColumn + i] = word[i];
            }
            matrix[row] = new string(charArray);
        }

        private static void InsertWordVertically(List<string> matrix, string word, int column, int startRow)
        {
            for (int i = 0; i < word.Length; i++)
            {
                var charArray = matrix[startRow + i].ToCharArray();
                charArray[column] = word[i];
                matrix[startRow + i] = new string(charArray);
            }
        }
    }
}
