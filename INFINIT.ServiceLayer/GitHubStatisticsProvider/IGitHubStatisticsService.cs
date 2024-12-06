namespace INFINIT.ServiceLayer.GitHubStatisticsProvider
{
    public interface IGitHubStatisticsService
    {
        Task<Dictionary<char, int>> GetLetterFrequenciesAsync();
        Task<List<string>> GetJavaScriptAndTypeScriptFilesAsync(string? path = null);
    }
}
