// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeOff.SampleClient
{
    /// <summary>
    /// Represents the dataverse client interface.
    /// </summary>
    public interface IDataverseClient
    {
        /// <summary>
        /// Gets the defined time off types from the Dynamics 365 organization.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<msdyn_TimeOffType>> GetTimeOffTypes();

        /// <summary>
        /// Gets the time off requests from the Dynamics 365 organization.
        /// </summary>
        /// <param name="approvedRequestsOnly">Indicates whether to retrieve only approved time off requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.  </returns>
        Task<IEnumerable<msdyn_WemRequest>> GetTimeOffRequests(bool approvedRequestsOnly);
    }
}
