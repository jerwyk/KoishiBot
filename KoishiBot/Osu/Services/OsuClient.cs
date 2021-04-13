using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace KoishiBot.Osu.Services
{
    public class OsuClient
    {
        private string _apiKey;
        private string _baseUri = "https://osu.ppy.sh/api/";

        public OsuClient()
        {
            _apiKey = Environment.GetEnvironmentVariable("OsuApi", EnvironmentVariableTarget.User);
        }

        private async Task<IRestResponse> GetApiResponseAsync(string endpoint, params (string key, string value)[] param)
        {
            string uri = _baseUri + endpoint;
            RestClient client = new RestClient(uri);
            RestRequest request = new RestRequest(Method.GET);
            request.AddParameter("k", _apiKey);

            foreach(var p in param)
            {
                request.AddParameter(p.key, p.value);
            }

            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(request, (response) => taskCompletionSource.SetResult(response));
            return await taskCompletionSource.Task.ConfigureAwait(false);

        }

        public async Task<dynamic> GetUser(string username)
        {
            var response = await GetApiResponseAsync("get_user", ("u", username));
            return JArray.Parse(response.Content);
        }

    }
}
