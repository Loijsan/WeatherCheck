using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCaller
{
    public class NotificationSender
    {
        private static readonly HttpClient client = new HttpClient();
        private const string ApiUrl = "https://api.pushover.net/1/messages.json";

        private readonly string _appToken;
        private readonly string _userKey;

        public NotificationSender(string appToken, string userKey)
        {
            _appToken = appToken;
            _userKey = userKey;
        }

        public async Task SendPushNotification(string title, string message)
        {
            var values = new Dictionary<string, string>
        {
            { "token", _appToken },
            { "user", _userKey },
            { "title", title },
            { "message", message }
        };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(ApiUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
