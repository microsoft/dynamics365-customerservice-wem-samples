// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Realtime.Common.Models
{
    public class AgentAfterWorkEvent : PropagationEventBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentAfterWorkEvent"/> class.
        /// </summary>
        public AgentAfterWorkEvent()
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
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in UTC when the wrap-up initiated.
        /// </summary>
        public DateTime? WrapUpInitiatedOn { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"{Status} initiated by {AgentName} on {WrapUpInitiatedOn}";
    }
}