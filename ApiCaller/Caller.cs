namespace ApiCaller
{
    public class Caller
    {
        public async void TempCaller(string latitude, string longitude)
        {
            var result = await ApiPostCaller(latitude, longitude);

            Console.WriteLine("Stop! Hammer time!");
        }
        private async Task<string> ApiPostCaller(string latitude, string longitude)
        {
            //StringContent stringContent = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/"+longitude+"/lat/"+latitude+"/data.json");

                var postTask = client.PostAsync(client.BaseAddress, null);
                postTask.Wait();

                if (postTask.Result.IsSuccessStatusCode)
                {
                    var result = await postTask.Result.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    Console.WriteLine(postTask.Result.ReasonPhrase);
                    return null;
                }
            }
        }
    }
}