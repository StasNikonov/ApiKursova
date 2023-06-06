using Newtonsoft.Json;

namespace KursovaAPI.Models
{
    public class Author
    {
        public ExternalURL external_urls { get; set; }
        public Followers followers { get; set; }
        public string name { get; set; }
        public List<string> genres { get; set; }
    }

    public class ExternalURL
    {
        [JsonProperty("spotify")]
        public string href { get; set; }
    }
    public class Followers
    {
        public string href { get; set; }
        public int total { get; set; }
    }
}

