using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CovidDataGraph
{
    public class OwidService : IRepository
    {
        private const string GITHUB_REPO = "https://raw.githubusercontent.com/owid/covid-19-data/master/public/data/owid-covid-data.json";
        private readonly HttpClient _client;
        public OwidService(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> Get<T>()
        {
            var response = await _client.GetAsync(GITHUB_REPO);
            var content = await response.Content.ReadAsStreamAsync();
            using (StreamReader stream = new StreamReader(content))
            {
                using (JsonReader reader = new JsonTextReader(stream))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(reader);
                }
            }
        }
    }
}
