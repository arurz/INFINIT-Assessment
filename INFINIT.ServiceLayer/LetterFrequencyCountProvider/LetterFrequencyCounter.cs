using INFINIT.DataLayer.Constants;
using System.Collections.Concurrent;

namespace INFINIT.ServiceLayer.LetterFrequencyCountProvider
{
    public class LetterFrequencyCounter : ILetterFrequencyCounter
    {
        /// <summary>
        /// Counts the frequencies of letters from a list of file URLs.
        /// </summary>
        /// <param name="fileUrls">List of URLs to process.</param>
        /// <returns>A dictionary with letters as keys and their frequencies as values.</returns>
        public async Task<Dictionary<char, int>> CountLetterFrequenciesAsync(List<string> fileUrls)
        {
            var frequencies = new ConcurrentDictionary<char, int>();
            var tasks = fileUrls.Select(url => ProcessFileAsync(url, frequencies)).ToList();

            await Task.WhenAll(tasks);

            return frequencies
                .OrderByDescending(pair => pair.Value)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private async Task ProcessFileAsync(string fileUrl, ConcurrentDictionary<char, int> frequencies)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = await client.GetStringAsync(fileUrl);
                    
                    foreach (var ch in content)
                    {
                        if (char.IsLetter(ch))
                        {
                            var letter = char.ToLower(ch);
                            frequencies.AddOrUpdate(letter, 1, (letter, count) => count + 1);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"{ExceptionConstants.FileRetrieveError}: {fileUrl}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ExceptionConstants.FileProcessingError}: {fileUrl}", ex);
            }
        }
    }
}
