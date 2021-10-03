using Microsoft.AspNetCore.Mvc;
using NewWorldData.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewWorldData.Controllers
{
    public class TwitterController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<TwitterModel> Tweets { get; set; }
        const string TWITTER_BASE_URL = "https://newworldfans.com/api/v1/dev_tracker?page=1&source=twitter";
        public TwitterController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{TWITTER_BASE_URL}");
            message.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                Tweets = await JsonSerializer.DeserializeAsync<IEnumerable<TwitterModel>>(responseStream);
            }

            return View(Tweets);
        }
    }
}
