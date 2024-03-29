﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents an event that denotes an agent initiated conference session.
    /// </summary>
    public class AgentConsultEvent : PropagationEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentConsultEvent"/> class.
        /// </summary>
        public AgentConsultEvent()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the initiating agent name.
        /// </summary>
        public string InitiatingAgentName { get; set; }

        /// <summary>
        /// Gets or sets the joined agent name.
        /// </summary>
        public string JoinedAgentName { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in UTC when the agent status was changed.
        /// </summary>
        public DateTime? JoinedOn { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"Consult session initiated by {InitiatingAgentName}, joined by {JoinedAgentName} on {JoinedOn}. [Org: {OrganizationName}. EventDelay:{EventDelayInMs}ms]";
    }
}