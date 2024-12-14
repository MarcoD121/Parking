using Humanizer;
using ParkingLib.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text.Json;

namespace ParkingRest.Model
{
    /// <summary>
    /// A class containing logic for retrieving vehicle information from a third party API
    /// </summary>
    public class APIClient
    {
        /// <summary>
        /// Method for retrieving vehicle information from the third party API - MotorAPI
        /// </summary>
        /// <param name="licensePlate">Unique ID for a requested car</param>
        /// <returns>A ParkedVehicle object containing relevant car information</returns>
        public async Task<ParkedVehicle?> LicensePlateInformationAsync(string licensePlate)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://v1.motorapi.dk"); // Replace with your actual base URL

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Add the X-AUTH-TOKEN header
            client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", "lcxt76xbeexm62sufabnv1aj3dgywaii"); // Replace with your actual token

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
