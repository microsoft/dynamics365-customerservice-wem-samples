﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Threading.Tasks;
using Integration.Realtime.Common.Models;
using Microsoft.Azure.WebJobs;

namespace Integration.Realtime.Common.Outputs
{
    /// <summary>
    /// Represents an Event Grid output binding for a propagation event.
    /// </summary>
    public interface IEventGridOutput
    {
        /// <summary>
        /// Writes the event to the output binding.
        /// </summary>
        /// <typeparam name="T">The type of the event.</typeparam>
        /// <param name="propagationEvent">The event to write.</param>
        /// <param name="binder">The output binder.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task WriteEvent<T>(T propagationEvent, Binder binder)
            where T : PropagationEventBase;
    }
}