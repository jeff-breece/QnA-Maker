using System;
using System.Net.Http;
using System.Text;

namespace QnA_Maker
{
    class Program
    {
        private const string endpointVar = "https://qnamakerproof.azurewebsites.net/qnamaker";
        private const string endpointKeyVar = "115e9db1-fac5-4a82-8df1-fde1a6829fbb";
        private const string kbIdVar = "e522b40c-8785-4c49-a348-6daaa475a91d";

        // Your QnA Maker resource endpoint.
        // From Publish Page: HOST
        // Example: https://YOUR-RESOURCE-NAME.azurewebsites.net/
        //private static string endpoint = Environment.GetEnvironmentVariable(endpointVar);
        // Authorization endpoint key
        // From Publish Page
        // Note this is not the same as your QnA Maker subscription key.
        //private static string endpointKey = Environment.GetEnvironmentVariable(endpointKeyVar);
        //private static string kbId = Environment.GetEnvironmentVariable(kbIdVar);

        static void Main(string[] args)
        {
            // https://qnamakerproof.azurewebsites.net/qnamaker/knowledgebases/e522b40c-8785-4c49-a348-6daaa475a91d/generateAnswer
            // Authorization: EndpointKey 115e9db1-fac5-4a82-8df1-fde1a6829fbb
            string endpoint = endpointVar;
            string endpointKey = endpointKeyVar;
            string kbId = kbIdVar;

            var uri = endpoint + "/knowledgebases/" + kbId + "/generateAnswer";

            // JSON format for passing question to service
            string question = @"{'question': 'Luis','top': 3}";
            try{
            // Create http client
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // POST method
                request.Method = HttpMethod.Post;

                // Add host + service to get full URI
                request.RequestUri = new Uri(uri);

                // set question
                request.Content = new StringContent(question, Encoding.UTF8, "application/json");

                // set authorization
                request.Headers.Add("Authorization", "EndpointKey " + endpointKey);

                // Send request to Azure service, get response
                var response = client.SendAsync(request).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;

                // Output JSON response
                Console.WriteLine(jsonResponse);
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
