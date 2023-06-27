// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents the base class for all events that will be forwarded
    /// to a distributor, such as Event Grid.
    /// </summary>
    public abstract class PropagationEventBase : EventBase
    {
        /// <summary>
        /// Gets or sets the delay in milliseconds between the time the operation was created
        /// and the time it was received.
        /// </summary>
        public double EventDelayInMs { get; set; }

        /// <summary>
        /// Gets the name for this event.
        /// </summary>
        public virtual string EventName => GetType().Name;

        /// <summary>
        /// Gets the full name for this event.
        /// </summary>
        public virtual string EventFullName => GetType().FullName;
    }
}