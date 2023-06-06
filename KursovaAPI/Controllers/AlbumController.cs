using KursovaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace KursovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly SpotifyAuth _spotifyAuth;
        public AlbumController(SpotifyAuth spotifyAuth)
        {
            _spotifyAuth = spotifyAuth;
        }

        [HttpGet("{id_album}")]
        public async Task<ActionResult<Album>> GetAlbum(string id_album)
        {
                string accessToken = await _spotifyAuth.GetAccessTokenAsync();
                var album = await GetAlbumDataFromSpotify(id_album, accessToken);

                if (album != null)
                {
                    return Ok(album);
                }
                else
                {
                    return NotFound();
                }
        }

        private async Task<Album> GetAlbumDataFromSpotify(string id_album, string accessToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync($"https://api.spotify.com/v1/albums/{id_album}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var album = JsonConvert.DeserializeObject<Album>(content);
                    return album;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}
