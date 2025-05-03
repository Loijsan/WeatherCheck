using ApiCaller.JsonObjects;
using Newtonsoft.Json;

namespace ApiCaller
{
    public class Caller
    {
        public async Task<string> TempCaller(string latitude, string longitude)
        {
            string weatherResult;
            var result = await ApiPostCaller(latitude, longitude);

            if (result is not null)
            {
                //Console.WriteLine("Kommer jag hit?");
                JsonExtractor(result);
                return weatherResult = result.ToString();
                // Deserialisera svaret
            }
            return weatherResult = "kommer jag hit?";
        }
        /// <summary>
        /// Makes the API call to the SMHI API.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        private async Task<string> ApiPostCaller(string latitude, string longitude)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/"+longitude+"/lat/"+latitude+"/data.json");

                var getTask = client.GetAsync(client.BaseAddress);
                getTask.Wait();

                if (getTask.Result.IsSuccessStatusCode)
                {
                    var result = await getTask.Result.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    Console.WriteLine(getTask.Result.ReasonPhrase);
                    return null;
                }
            }
        }
        /// <summary>
        /// This method is used to extract the data from the json response. It only returns the valid datapoints not to overpopulate the list.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<TimeSeries> JsonExtractor(string result)
        {
            var weather = JsonConvert.DeserializeObject<WeatherData>(result);
            
            if (weather is null)
            {
                return null;
            }

            // Stop the extraction after +9 hours (Thank you ChatGPT)
            DateTime now = DateTime.Now;
            DateTime endTime = now.AddHours(9);

            // Filtrera fram endast tidsserier inom önskad period
            var filteredTimes = weather.timeSeries
                .Where(ts => ts.validTime >= now && ts.validTime <= endTime)
                .ToList();

            // Example from chatGPT 
            foreach (var ts in filteredTimes)
            {
                var tempParam = ts.parameters.FirstOrDefault(p => p.name == "t");
                if (tempParam != null)
                {
                    Console.WriteLine($"Tid: {ts.validTime}, Temperatur: {tempParam.values[0]} {tempParam}");
                }
            }

            return filteredTimes;
        }
    }
}
