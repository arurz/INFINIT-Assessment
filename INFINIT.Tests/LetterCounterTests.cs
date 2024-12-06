using INFINIT.ServiceLayer.LetterFrequencyCountProvider;

namespace INFINIT.Tests
{
    public class LetterCounterTests
    {
        private readonly ILetterFrequencyCounter _letterFrequencyCounter;
        public LetterCounterTests()
        {
            _letterFrequencyCounter = new LetterFrequencyCounter();
        }

        [Fact]
        public void CheckCountAfterFilesMerge()
        {
            //Arrange
            List<string> fileUrls =
            [
                "https://raw.githubusercontent.com/lodash/lodash/main/.markdown-doctest-setup.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/fp/_convertBrowser.js"
            ];

            var expected = new Dictionary<char, int>
            {
                { 'o', 65 },
                { 'e', 60 },
                { 't', 53 },
                { 's', 46 },
                { 'r', 46 },
                { 'n', 43 },
                { 'a', 36 },
                { 'l', 28 },
                { 'i', 28 },
                { 'c', 27 },
                { 'u', 20 },
                { 'd', 19 },
                { 'b', 17 },
                { 'h', 14 },
                { 'p', 13 },
                { 'v', 13 },
                { 'f', 12 },
                { 'm', 7 },
                { 'g', 6 },
                { 'w', 4 },
                { 'x', 4 },
                { 'j', 4 },
                { 'q', 3 },
                { 'y', 2 }
            };

            //Act
            var result = _letterFrequencyCounter.CountLetterFrequenciesAsync(fileUrls);

            //Assert
            Equals(expected, result);
        }

        [Fact]
        public void CountLettersFromRemoteFile()
        {
            //Arrange
            List<string> fileUrls =
            [
                "https://raw.githubusercontent.com/lodash/lodash/main/.markdown-doctest-setup.js"
            ];

            var expected = new Dictionary<char, int>
            {
                { 's', 17 },
                { 'l', 16 },
                { 'e', 14 },
                { 'o', 13 },
                { 'a', 11 },
                { 'r', 8 },
                { 'b', 7 },
                { 'd', 6 },
                { 't', 6 },
                { 'g', 6 },
                { 'c', 5 },
                { 'i', 4 },
                { 'u', 4 },
                { 'h', 3 },
                { 'n', 3 },
                { 'q', 2 },
                { 'j', 2 },
                { 'p', 1 },
                { 'x', 1 },
                { 'm', 1 },
                { 'f', 1 }
            };

            //Act
            var result = _letterFrequencyCounter.CountLetterFrequenciesAsync(fileUrls);

            //Assert
            Equals(expected, result);
        }
    }
}
