
using Newtonsoft.Json;

namespace KursovaAPI.Models
{
    public class Album
    {
        public string name { get; set; }
        public string album_type { get; set; }
        public int total_tracks { get; set; }
        public List<string> available_markets { get; set; }
        public ExternalURLAlbum external_urls { get; set; }
        public string release_date { get; set; }
        public List<ArtistsAlbum> artists { get; set; }

        public Album()
        {
            artists = new List<ArtistsAlbum>();
        }
    }
    public class ExternalURLAlbum
    {
        [JsonProperty("spotify")]
        public string href { get; set; }
    }
    public class ArtistsAlbum
    {
        public string name { get; set; }
        public string type { get; set; }
    }
}

