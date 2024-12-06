using System.Text.Json.Serialization;

namespace INFINIT.DataLayer.Entities
{
    public class GitHubItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("path")]
        public string Path { get; set; } = default!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;

        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; } = default!;
    }
}
