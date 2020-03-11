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

        public MovieProcessor()
        {            
            uri = ApiHelper.ApiClient.BaseAddress.ToString();
        }

        public async Task<ApiConfig> LoadToken()
        {
            HttpContent content = TokenContent();
            string loginUri = LoginUri();
            

            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(loginUri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    ApiConfig newtoken = await response.Content.ReadAsAsync<ApiConfig>();
                    return newtoken;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<MovieTitle> LoadIds(string token)
        {
            string movieUpdatesUri = MovieUpdatesUri();
            ApiHelper.ApiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(movieUpdatesUri))
            {
                
                if (response.IsSuccessStatusCode)
                {
                    MovieTitle movieTitles = await response.Content.ReadAsAsync<MovieTitle>();
                    Console.WriteLine(movieTitles);
                    return movieTitles;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }

        }

        private HttpContent TokenContent()
        {
            var payload = new Dictionary<string, string>
            {
              {"apiKey", ApiHelper.ApiKey},
              {"userName", ApiHelper.UserName},
              {"userKey", ApiHelper.UserKey}
            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");
            return content;
        }

        private string LoginUri()
        {
            return uri + "login";
        }

        private string MovieUpdatesUri()
        {
            return uri + "movieupdates";
        }
    }
}
