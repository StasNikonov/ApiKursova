using KursovaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace KursovaAPI
{
    // Клас авторизації Spotify
    public class SpotifyAuth
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetAccessTokenAsync()
        {
            const string client_id = "ec2dacaf97394f618379f77052869aa3";
            const string client_secret = "a81dfacef22c4beca37668c48e39d9b3";

            string authString = $"{client_id}:{client_secret}";
            var base64AuthString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authString));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64AuthString);

            var requestParams = new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" }
        };

            var response = await client.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(requestParams));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(content)["access_token"].ToString();
                return token;
            }

            throw new Exception("Failed to obtain access token");
        }
    }
}


