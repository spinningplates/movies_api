using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace MoviesAPI
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static string Uri { get; set; }
        public static string ApiKey { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();

            InitializeKeys();            
            ApiClient.BaseAddress = new Uri(Uri);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
        }

        public static void InitializeKeys()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            IConfigurationSection ApiSection = config.GetSection("OmdbCredentials");
            Uri = ApiSection["Uri"];
            ApiKey = ApiSection["ApiKey"];
        }
    }
}
