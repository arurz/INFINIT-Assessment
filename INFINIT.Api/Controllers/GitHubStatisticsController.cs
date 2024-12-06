using Microsoft.AspNetCore.Mvc;
using INFINIT.ServiceLayer.GitHubStatisticsProvider;

namespace INFINIT.Api.Controllers
{
    [ApiController]
    [Route("/api/github/statistic")]
    public class GitHubStatisticsController : ControllerBase
    {
        private readonly IGitHubStatisticsService _gitHubStatisticsService;

        public GitHubStatisticsController(IGitHubStatisticsService gitHubStatisticsService)
        {
            _gitHubStatisticsService = gitHubStatisticsService;
        }

        /// <summary>
        /// Gets the letter frequencies from GitHub repository files.
        /// </summary>
        /// <returns>Dictionary of letters and their frequencies.</returns>
        [HttpGet("lettersfrequency")]
        public async Task<IActionResult> GetLettersFrequency()
        {
            var result = await _gitHubStatisticsService.GetLetterFrequenciesAsync();
            return Ok(result);
        }
    }
}
