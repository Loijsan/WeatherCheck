using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FrostWarningSweden
{
    public class WeatherCheck
    {
        [FunctionName("WeatherCheck")]
        public void Run([TimerTrigger("0 0 18 * * * ")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
