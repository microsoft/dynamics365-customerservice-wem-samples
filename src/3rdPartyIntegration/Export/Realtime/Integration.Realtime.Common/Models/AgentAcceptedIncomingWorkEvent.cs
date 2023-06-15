// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents an event that denotes an agent accepted incoming work item event.
    /// </summary>
    public class AgentAcceptedIncomingWorkEvent : PropagationEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentAcceptedIncomingWorkEvent"/> class.
        /// </summary>
        public AgentAcceptedIncomingWorkEvent()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the agent name.
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string IsAgentAccepted { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in UTC when the wrap-up initiated.
        /// </summary>
        public DateTime? StartedOn { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"{AgentName} accepted incoming {Channel} on {StartedOn}";
    }
}