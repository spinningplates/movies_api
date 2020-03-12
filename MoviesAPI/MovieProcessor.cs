using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MoviesAPI.Data;
using MoviesAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

        public async Task<String> GetMovieById(int id)
        {
            string getByIdUri = GetByIdUri(id);
            

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(getByIdUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsAsync<String>();
                    return result;
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

        private string GetByIdUri(int id)
        {
            return uri + "?i=" + id + "&apiKey" + apiKey;
        }
    }
}
