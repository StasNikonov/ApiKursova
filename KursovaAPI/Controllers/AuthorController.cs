using KursovaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace KursovaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly SpotifyAuth _spotifyAuth;
        public AuthorController(SpotifyAuth spotifyAuth)
        {
            _spotifyAuth = spotifyAuth;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(string id)
        {
                string accessToken = await _spotifyAuth.GetAccessTokenAsync();
                var author = await GetAuthorDataFromSpotify(id, accessToken);

                if (author != null)
                {
                    return Ok(author);
                }
                else
                {
                    return NotFound();
                }
        }

        private async Task<Author> GetAuthorDataFromSpotify(string id, string accessToken)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync($"https://api.spotify.com/v1/artists/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var author = JsonConvert.DeserializeObject<Author>(content);
                    return author;
                }
                else
                {
                    return null;
                }
            }
        }
    }

}