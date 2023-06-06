using KursovaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace KursovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly SpotifyAuth _spotifyAuth;
        public TrackController(SpotifyAuth spotifyAuth)
        {
            _spotifyAuth = spotifyAuth;
        }

        [HttpGet("{id_track}")]
        public async Task<ActionResult<Track>> GetTrack(string id_track)
        {

                string accessToken = await _spotifyAuth.GetAccessTokenAsync();
                var track = await GetTrackDataFromSpotify(id_track, accessToken);

                if (track != null)
                {
                    return Ok(track);
                }
                else
                {
                    return NotFound();
                }
        }

        private async Task<Track> GetTrackDataFromSpotify(string id_track, string accessToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync($"https://api.spotify.com/v1/tracks/{id_track}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var track = JsonConvert.DeserializeObject<Track>(content);
                    return track;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}
