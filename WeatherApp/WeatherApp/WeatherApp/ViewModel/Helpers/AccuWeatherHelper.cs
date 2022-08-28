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
    public class AccuWeatherHelper
    {
        public const string API_KEY = "sJw37oEFGFuNrgv92I2VostuG8q0VtN0";

        public const string BASE_URL = "http://dataservice.accuweather.com/";

        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                try
                {
                    cities = JsonConvert.DeserializeObject<List<City>>(json);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                try
                {
                    currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return currentConditions;
        }
    }
}
