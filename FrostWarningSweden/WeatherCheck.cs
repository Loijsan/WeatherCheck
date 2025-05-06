using System;
using System.Threading.Tasks;
using ApiCaller;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FrostWarningSweden
{
    public class WeatherCheck
    {
        [FunctionName("WeatherCheck")]
        public async Task RunAsync([TimerTrigger("0 0 16 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // 0 */1 * * * * - every minute - for testing purpose
            // 0 0 18 * * * - at 18 every day - well, that is UTC... I want SE and summertime, so 16...
            // Save our home position
            string latitude = "57.844579";
            string longitude = "11.896699";
            Caller caller = new();

            string result = caller.TempCaller(latitude, longitude).GetAwaiter().GetResult();
            //Console.WriteLine(result);

            // Pushover
            string pushoverAppToken = Environment.GetEnvironmentVariable("PUSHOVER_APP_TOKEN");
            string pushoverUserKey = Environment.GetEnvironmentVariable("PUSHOVER_USER_KEY");

            NotificationSender sender = new(pushoverAppToken, pushoverUserKey);
            await sender.SendPushNotification("Vädervarning", result);

        }

    }
}
