using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    // Helpers are ideal for things that are not necessarily related to one specific VM. For example, database connectivity can exist in a helper.
    public class AccuWeatherHelper
    {
        public const string apiKey = "vMe4bnPWPw007GizM1EgwylgR13v5yqS";
        public const string baseURL = "http://dataservice.accuweather.com";
        public const string autoCompleteEndpoint = "/locations/v1/cities/autocomplete/?apikey={0}&q={1}";
        public const string currentConditionsEndpoint = "/currentconditions/v1/{0}?apikey={1}";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();
            string requestURL = baseURL + string.Format(autoCompleteEndpoint, apiKey, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(requestURL);
                string json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            string requestURL = baseURL + string.Format(currentConditionsEndpoint, cityKey, apiKey);

            using (HttpClient client = new HttpClient()) 
            { 
                var response = await client.GetAsync(autoCompleteEndpoint);
                string json = await response.Content.ReadAsStringAsync();
                // Even if you pass just one cityKey, the JSON will return a list. Hence List<> and .First() to get the first city.
                currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}