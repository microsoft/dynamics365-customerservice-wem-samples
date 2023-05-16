// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents the base class for all customer service events.
    /// </summary>
    public abstract class EventBase
    {
        /// <summary>
        /// Gets or sets the business unit.id.
        /// </summary>
        public Guid BusinessUnitId { get; set; }

        /// <summary>
        /// Gets or sets the organization id.
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the organization name.
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in UTC when the operation record was created.
        /// </summary>
        public DateTime? OperationCreatedOn { get; set; }
    }
}