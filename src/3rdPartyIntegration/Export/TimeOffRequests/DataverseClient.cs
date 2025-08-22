// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace TimeOff.SampleClient
{
    /// <inheritdoc/>
    public class DataverseClient : IDataverseClient
    {
        private readonly ServiceClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataverseClient"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public DataverseClient(AppSettings appSettings)
        {
            var connectionString = $"AuthType=OAuth;Url={appSettings.DynamicsUrl};RedirectUri={appSettings.RedirectUri};AppId={appSettings.ClientId};LoginPrompt=Auto";
            client = new ServiceClient(connectionString);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<msdyn_TimeOffType>> GetTimeOffTypes()
        {
            var query = new QueryExpression(msdyn_TimeOffType.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(true), // Retrieve all columns
            };

            var torTypes = await client.RetrieveMultipleAsync(query);
            return torTypes.Entities.Select(e => e.ToEntity<msdyn_TimeOffType>());
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<msdyn_WemRequest>> GetTimeOffRequests(bool approvedRequestsOnly)
        {
            var tor = new List<msdyn_WemRequest>();
            var query = new QueryExpression(msdyn_WemRequest.EntityLogicalName)
            {
                ColumnSet = new ColumnSet(true), // Retrieve all columns
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression(msdyn_WemRequest.Fields.msdyn_RequestType, ConditionOperator.Equal, (int)msdyn_WemRequest_msdyn_RequestType.Timeoff),
                    },
                },
                PageInfo = new PagingInfo
                {
                    PageNumber = 1,
                    Count = 5000, // Number of records per page, max is 5000
                },
            };

            if (approvedRequestsOnly)
            {
                query.Criteria.AddCondition(msdyn_WemRequest.Fields.msdyn_RequestStatus, ConditionOperator.Equal, (int)msdyn_WemRequest_msdyn_RequestStatus.Approved);
            }

            do
            {
                var timeOffRequests = await client.RetrieveMultipleAsync(query);
                if (timeOffRequests == null || timeOffRequests.Entities == null || !timeOffRequests.Entities.Any())
                {
                    break; // No more records to process
                }

                tor.AddRange(timeOffRequests.Entities.Select(e => e.ToEntity<msdyn_WemRequest>()));

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
            while (true);

            return tor;
        }
    }
}