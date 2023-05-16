// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents a step event.
    /// </summary>
    /// <remarks>This is the event that is sent over from dataverse.</remarks>
    public class StepEvent : EventBase
    {
        /// <summary>
        /// Gets or sets the message name.
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// Gets or sets the operation id.
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// Gets or sets the primary entity id for this event.
        /// </summary>
        public Guid PrimaryEntityId { get; set; }

        /// <summary>
        /// Gets or sets the primary entity name for this event.
        /// </summary>
        public string PrimaryEntityName { get; set; }

        /// <summary>
        /// Gets or sets the post entity images.
        /// </summary>
        public List<KeyValuePair<string, PostEntityImageValue>> PostEntityImages { get; set; }
    }
}