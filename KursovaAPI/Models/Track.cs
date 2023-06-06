using Newtonsoft.Json;

namespace KursovaAPI.Models
{
    public class Track
    {
        public string name { get; set; }
        public List<ArtistsTrack> artists { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public int duration_ms { get; set; }
        public ExternalURLAlbum external_urls { get; set; }
    }
    public class ArtistsTrack
    {
        public string name { get; set; }
    }
    public class ExternalURLTrack
    {
        [JsonProperty("spotify")]
        public string href { get; set; }
    }
}

