// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents an event that denotes a change in the agent status.
    /// </summary>
    public class AgentStatusEvent : PropagationEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentStatusEvent"/> class.
        /// </summary>
        public AgentStatusEvent()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the agent name.
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the agent status.
        /// </summary>
        public string AgentStatus { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in UTC when the agent status was changed.
        /// </summary>
        public DateTime? StatusChangeTime { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"{StatusChangeTime}, {AgentName}, {AgentStatus}";
    }
}