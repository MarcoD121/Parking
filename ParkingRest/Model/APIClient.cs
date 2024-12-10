using Humanizer;
using ParkingLib.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text.Json;

namespace ParkingRest.Model
{
    public class APIClient
    {
        public async Task<ParkedVehicle?> LicensePlateInformationAsync(string licensePlate)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://v1.motorapi.dk"); // Replace with your actual base URL

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Add the X-AUTH-TOKEN header
            client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", "tbpjbzwav91qhkhmg1agzp0qc0ozzry4"); // Replace with your actual token

            // Get data response
            var response = await client.GetAsync($"/vehicles/{licensePlate}"); // Replace with your actual endpoint
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                { 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                };
                var jsonString = await response.Content.ReadAsStringAsync();
                var dataObject = JsonSerializer.Deserialize<ParkedVehicle>(jsonString, options);
                dataObject.ActiveParked = new TimeForParkedVehicle(DateTime.Now);
                if (dataObject.Type.Contains("bil"))
                {
                    dataObject.NumberOfWheels = 4;
                }
                else
                {
                    dataObject.NumberOfWheels = 2;
                }

                return dataObject;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }
    }
}
