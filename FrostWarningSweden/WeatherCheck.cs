using System;
using ApiCaller;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FrostWarningSweden
{
    public class WeatherCheck
    {
        [FunctionName("WeatherCheck")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // 0 */1 * * * * - every minute - for testing purpose
            // 0 0 18 * * * - at 18 every day
            // Save our home position
            string latitude = "57.844579";
            string longitude = "11.896699";

            Caller caller = new();
            
            string result = caller.TempCaller(latitude, longitude).ToString();

            // Give the push to pushover - how is this done?
        }
    }
}
