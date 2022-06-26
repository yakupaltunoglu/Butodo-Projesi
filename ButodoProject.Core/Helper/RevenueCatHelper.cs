using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using RestSharp;

namespace ButodoProject.Core.Helper
{
    public static class RevenueCatHelper
    {
        private static string apiKey = "aCgAErTTpzzSJgOOrrvydIzjHRIRCnqp";
        private static string baseUrl = "https://api.revenuecat.com";

        public static string GetUser(string userId)
        {
            var postData = "";
            var client = new HttpClient();
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/v1/subscribers/" + userId);

            requestMessage.Method = HttpMethod.Get;
            requestMessage.Headers.Add("Accept", "application/json");
            //requestMessage.Headers.Add("Content-Type", "application/json");
            requestMessage.Headers.Add("Authorization", "Bearer " + apiKey);
            requestMessage.Content = new StringContent(postData, Encoding.UTF8, "application/json");

            //var response = client.SendAsync(requestMessage).GetAwaiter().GetResult();
            var response = client.SendAsync(requestMessage).Result.Content.ReadAsStringAsync();

            return response.Result;
        }
    }
}
