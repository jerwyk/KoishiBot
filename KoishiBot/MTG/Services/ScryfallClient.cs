using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using KoishiBot.MTG.Models;

namespace KoishiBot.MTG.Services
{
    class ScryfallClient
    {

        private string _baseUrl = "https://api.scryfall.com/";

        public ScryfallClient()
        {
            
        }

        private async Task<IRestResponse> GetApiResponseAsync(string endpoint, params (string key, string value)[] param)
        {
            string url = _baseUrl + endpoint;
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);

            foreach (var p in param)
            {
                request.AddParameter(p.key, p.value);
            }

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

            client.ExecuteAsync(request, (response) => taskCompletionSource.SetResult(response));
            return await taskCompletionSource.Task.ConfigureAwait(false);

        }

        private async Task<IRestResponse> GetApiUrlResponseAsync(string fullUrl)
        {
            RestClient client = new RestClient(fullUrl);
            RestRequest request = new RestRequest(Method.GET);

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

            client.ExecuteAsync(request, (response) => taskCompletionSource.SetResult(response));
            return await taskCompletionSource.Task.ConfigureAwait(false);

        }

        public async Task<dynamic> FetchUri(string uri)
        {
            var response = await GetApiUrlResponseAsync(uri);
            return JObject.Parse(response.Content);
        }


        public async Task<SimpleCard> SearchCardSimple(string name)
        {
            var response = await GetApiResponseAsync("cards/search", ("q", name));
            dynamic card = JObject.Parse(response.Content);

            return new SimpleCard(Convert.ToString(card.data[0].name), Convert.ToString(card.data[0].image_uris.normal), Convert.ToString(card.data[0].scryfall_uri));

        }

        public async Task<dynamic> SearchCard(string query, int page = 1)
        {
            var response = await GetApiResponseAsync("cards/search", ("q", query), ("page", page.ToString()));
            return JObject.Parse(response.Content);
        }

        public async Task<SimpleCard> NameCardSimple(string name)
        {
            var response = await GetApiResponseAsync("cards/named", ("fuzzy", name));
            dynamic content = JObject.Parse(response.Content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return new SimpleCard(Convert.ToString(content.name), Convert.ToString(content.image_uris.normal), Convert.ToString(content.scryfall_uri));
            else
                throw new Exception(Convert.ToString(content.details));

        }

        public async Task<dynamic> NameCard(string name)
        {
            var response = await GetApiResponseAsync("cards/named", ("fuzzy", name));
            dynamic content = JObject.Parse(response.Content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return content;
            else
                throw new Exception(Convert.ToString(content.details));

        }

    }
}
