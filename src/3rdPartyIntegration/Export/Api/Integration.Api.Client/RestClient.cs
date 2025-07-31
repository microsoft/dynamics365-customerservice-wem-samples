// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Integration.Api.Client
{
    public static class RestClient
    {
        public static async Task GetTimeOffTypes(string dynamicsUrl, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                var requestUrl = $"{dynamicsUrl}/api/data/v9.2/msdyn_timeofftypes";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await httpClient.GetAsync(requestUrl);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Time Off Types Data:");
                Console.WriteLine(JToken.Parse(content).ToString());
            }
        }
        public static async Task GetTimeOffRequests(string dynamicsUrl, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                // "msdyn_requeststatus": 6:Pending, 4:Approved, 5:Rejected
                var requestUrl = $"{dynamicsUrl}/api/data/v9.2/msdyn_wemrequests?$top=100&$filter=msdyn_requeststatus eq 4&$orderby=modifiedon desc";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await httpClient.GetAsync(requestUrl);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Time Off Requests Data:");
                Console.WriteLine(JToken.Parse(content).ToString());
            }
        }
    }
}