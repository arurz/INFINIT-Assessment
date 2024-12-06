using INFINIT.Configurations;
using INFINIT.DataLayer.Constants;
using Microsoft.Extensions.Options;
using INFINIT.ServiceLayer.LetterFrequencyCountProvider;
using System.Net.Http.Headers;
using System.Text.Json;
using INFINIT.DataLayer.Entities;

namespace INFINIT.ServiceLayer.GitHubStatisticsProvider
{
    public class GitHubStatisticsService : IGitHubStatisticsService
    {
        private readonly GitHubApiSettings _settings;
        private readonly ILetterFrequencyCounter _letterFrequencyCounter;

        public GitHubStatisticsService(IOptions<GitHubApiSettings> options, ILetterFrequencyCounter letterFrequencyCounter)
        {
            _settings = options.Value;
            _letterFrequencyCounter = letterFrequencyCounter;
        }

        /// <summary>
        /// Gets the frequency of letters in JavaScript/TypeScript files from the GitHub repository.
        /// </summary>
        /// <returns>A dictionary with letters as keys and their frequencies as values.</returns>
        public async Task<Dictionary<char, int>> GetLetterFrequenciesAsync()
        {
            var fileUrls = await GetJavaScriptAndTypeScriptFilesAsync();
            return await _letterFrequencyCounter.CountLetterFrequenciesAsync(fileUrls);
        }

        public async Task<List<string>> GetJavaScriptAndTypeScriptFilesAsync(string? path = null)
        {
            var response = await GetRepositoryContentAsync(path);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{ExceptionConstants.RequestErrorText}: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<GitHubItem>>(content)
                        ?? throw new JsonException($"{ExceptionConstants.ContentDeserializeError}");

            return await ProcessItemsAsync(items);
        }

        private async Task<List<string>> ProcessItemsAsync(IList<GitHubItem> items)
        {
            var files = new List<string>();
            var tasks = new List<Task<List<string>>>();

            foreach (var item in items)
            {
                if (item.Type == GitHubFileConstants.ItemTypeFile &&
                    (item.Name.EndsWith(GitHubFileConstants.FileExtentionJavaScript) ||
                     item.Name.EndsWith(GitHubFileConstants.FileExtentionTypeScript)))
                {
                    files.Add(item.DownloadUrl);
                }
                else if (item.Type == GitHubFileConstants.ItemTypeDirectory)
                {
                    tasks.Add(GetJavaScriptAndTypeScriptFilesAsync(item.Path));
                }
            }

            var subFiles = await Task.WhenAll(tasks);
            foreach (var subFileList in subFiles)
            {
                files.AddRange(subFileList);
            }

            return files;
        }

        private async Task<HttpResponseMessage> GetRepositoryContentAsync(string? path = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(ApiConstants.ProductName, ApiConstants.ProductVersion));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiConstants.TokenHeaderName, _settings.Token);

                var url = string.IsNullOrWhiteSpace(path)
                    ? $"{_settings.GitHubApiUrl}/{_settings.Owner}/{_settings.Repository}/{ApiConstants.GitHubUrlContents}"
                    : $"{_settings.GitHubApiUrl}/{_settings.Owner}/{_settings.Repository}/{ApiConstants.GitHubUrlContents}/{path}";

                return await client.GetAsync(url);
            }
        }
    }
}
