using System;
using System.Net.Http;
using System.Threading.Tasks;
using MoviesAPI.Models;
using Newtonsoft.Json.Linq;

namespace MoviesAPI
{
    public class MovieProcessor
    {
        private string uri;
        private string apiKey;

        public MovieProcessor()
        {            
            uri = ApiHelper.ApiClient.BaseAddress.ToString();
            apiKey = ApiHelper.ApiKey.ToString();
        }

        public async Task<JObject> SearchMovies(string searchString)
        {
            string searchUri = SearchUri(searchString);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(searchUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject jsonResult = JObject.Parse(result);
                    return jsonResult;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Movie> GetMovieById(string id)
        {
            string getByIdUri = GetByIdUri(id);
            

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(getByIdUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Movie movie = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(result);

                    return movie;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private string SearchUri(string searchString)
        {
            return uri + "?s=" + searchString + "&apiKey=" + apiKey;
        }

        private string GetByIdUri(string id)
        {
            return uri + "?i=" + id + "&apiKey=" + apiKey;
        }
    }
}
