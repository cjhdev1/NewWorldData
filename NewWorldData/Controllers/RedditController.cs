using Microsoft.AspNetCore.Mvc;
using NewWorldData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewWorldData.Controllers
{
    public class RedditController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<RedditModel> Reddits { get; set; }
        const string REDDIT_BASE_URL = "https://newworldfans.com/api/v1/dev_tracker?page=1&source=reddit";
        public RedditController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{REDDIT_BASE_URL}");
            message.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                Reddits = await JsonSerializer.DeserializeAsync<IEnumerable<RedditModel>>(responseStream);
            }
            
            return View(Reddits);
        }
    }
}
