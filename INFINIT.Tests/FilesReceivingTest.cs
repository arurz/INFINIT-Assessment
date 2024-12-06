using INFINIT.Configurations;
using INFINIT.ServiceLayer.GitHubStatisticsProvider;
using INFINIT.ServiceLayer.LetterFrequencyCountProvider;
using Microsoft.Extensions.Options;
using Moq;

namespace INFINIT.Tests
{
    public class FilesReceivingTest
    {
        private readonly IGitHubStatisticsService _gitHubStatisticsService;
        private readonly ILetterFrequencyCounter _letterFrequencyCounter;
        private readonly Mock<IOptions<GitHubApiSettings>> _mockOptions;

        public FilesReceivingTest()
        {
            _mockOptions = new Mock<IOptions<GitHubApiSettings>>();
            _mockOptions.Setup(o => o.Value).Returns(new GitHubApiSettings
            {
                GitHubApiUrl = "https://api.github.com/repos",
                Owner = "lodash",
                Repository = "lodash",
                Token = "YourToken"
            });
            _letterFrequencyCounter = new LetterFrequencyCounter();
            _gitHubStatisticsService = new GitHubStatisticsService(_mockOptions.Object, _letterFrequencyCounter);
        }

        [Fact]
        public void CheckFilesRecieving()
        {
            //Arrange
            var expected = new List<string>()
            {
                "https://raw.githubusercontent.com/lodash/lodash/main/.markdown-doctest-setup.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/dist/lodash.core.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/dist/lodash.core.min.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/dist/lodash.fp.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/dist/lodash.fp.min.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/dist/lodash.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/dist/lodash.min.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/dist/mapping.fp.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/fp/_baseConvert.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/fp/_convertBrowser.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/fp/_mapping.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/fp/placeholder.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/common/file.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/common/mapping.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/common/minify.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/common/uglify.options.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/common/util.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/fp/build-dist.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/fp/build-doc.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/fp/build-modules.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/main/build-dist.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/main/build-doc.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/main/build-modules.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lib/main/build-site.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/lodash.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/perf/asset/perf-ui.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/perf/perf.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/test/asset/test-ui.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/test/asset/worker.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/test/remove.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/test/saucelabs.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/test/test-fp.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/test/test.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/backbone.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/collection.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/events.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/model.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/noconflict.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/router.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/setup/dom-setup.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/setup/environment.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/sync.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/backbone/test/view.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/firebug-lite/src/firebug-lite-debug.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/json-js/json2.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/test/arrays.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/test/chaining.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/test/collections.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/test/cross-document.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/test/functions.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/test/objects.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/test/utility.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/underscore-min.js",
                "https://raw.githubusercontent.com/lodash/lodash/main/vendor/underscore/underscore.js"
            };

            //Act
            var result = _gitHubStatisticsService.GetJavaScriptAndTypeScriptFilesAsync();

            //Assert
            Equals(expected, result);
        }
    }
}
