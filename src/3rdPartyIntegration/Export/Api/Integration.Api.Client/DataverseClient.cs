// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace Integration.Api.Client
{
    public class DataverseClient
    {
        public static void GetTimeOffTypes(string dynamicsUrl, string accessToken)
        {
            using (var serviceClient = new ServiceClient(
                new Uri(dynamicsUrl),
                (string resource) => Task.FromResult(accessToken),
                false
            ))
            {
                if (serviceClient.IsReady)
                {
                    var query = new QueryExpression("msdyn_timeofftype")
                    {
                        ColumnSet = new ColumnSet(true) // Retrieve all columns
                    };

                    var timeOffTypes = serviceClient.RetrieveMultiple(query);

                    Console.WriteLine("Time Off Types Data:");
                    foreach (var entity in timeOffTypes.Entities)
                    {
                        Console.WriteLine(JToken.FromObject(entity).ToString());
                    }
                }
                else
                {
                    Console.WriteLine("ServiceClient is not ready. Error: " + serviceClient.LastError);
                    return;
                }
            }
        }

        public static void GetTimeOffRequests(string dynamicsUrl, string accessToken)
        {
            using (var serviceClient = new ServiceClient(
                new Uri(dynamicsUrl),
                (string resource) => Task.FromResult(accessToken),
                false
            ))
            {
                if (serviceClient.IsReady)
                {
                    var query = new QueryExpression("msdyn_wemrequest")
                    {
                        ColumnSet = new ColumnSet(true), // Retrieve all columns
                        Criteria = new FilterExpression  // Only retrieve approved requests
                        {
                            Conditions =
                        {
                            // "msdyn_requeststatus": 6:Pending, 4:Approved, 5:Rejected
                            new ConditionExpression("msdyn_requeststatus", ConditionOperator.Equal, 4)
                        }
                        },
                        PageInfo = new PagingInfo  // Add paging support
                        {
                            PageNumber = 1,
                            Count = 2 // Number of records per page, using 2 for sample purposes
                        }
                    };

                    Console.WriteLine("Time Off Requests Data:");
                    while (true)
                    {
                        var timeOffRequests = serviceClient.RetrieveMultiple(query);

                        foreach (var entity in timeOffRequests.Entities)
                        {
                            Console.WriteLine(JToken.FromObject(entity).ToString());
                        }

                        if (timeOffRequests.MoreRecords)
                        {
                            query.PageInfo.PageNumber++;
                            query.PageInfo.PagingCookie = timeOffRequests.PagingCookie;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ServiceClient is not ready. Error: " + serviceClient.LastError);
                    return;
                }
            }
        }
    }
}