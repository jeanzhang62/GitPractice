using AutomobileMaintenanceTracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AutomobileMaintenanceTracker.Helpers
{
    public class CarsService
    {
        readonly string baseUri = "http://localhost:50404/api/CarsApi/";

        public List<Car> GetCars()
        {
            using (HttpClient httpClient = new HttpClient())
            {
               var response = httpClient.GetStringAsync(baseUri);

                return JsonConvert.DeserializeObject<List<Car>>(response.Result);
            }
        }

        public Car GetCarById(int id)
        {
            string uri = baseUri + id;
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<Car>(response.Result);
            }
        }

    }
}