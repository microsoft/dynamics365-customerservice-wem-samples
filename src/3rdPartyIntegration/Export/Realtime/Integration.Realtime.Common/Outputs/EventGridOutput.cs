// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Threading.Tasks;
using Azure.Messaging;
using Integration.Realtime.Common.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Integration.Realtime.Common.Outputs
{
    /// <summary>
    /// Represents an Event Grid output binding for a propagation event.
    /// </summary>
    public class EventGridOutput : OutputBase, IEventGridOutput
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventGridOutput"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">An instance of the registered logger.</param>
        public EventGridOutput(IConfiguration configuration, ILogger<EventGridOutput> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public override async Task WriteEvent<T>(T propagationEvent, Binder binder)
        {
            var cloudEvent = new CloudEvent(
                propagationEvent.OrganizationName,
                propagationEvent.EventFullName,
                propagationEvent);

            var outputeventGridAttributes = new Attribute[]
               {
                    new EventGridAttribute()
                    {
                        TopicEndpointUri = Constants.SettingConstants.EventGridEndpoint,
                        TopicKeySetting = Constants.SettingConstants.EventGridKey,
                    },
               };

            var eventCollector = await binder.BindAsync<IAsyncCollector<CloudEvent>>(outputeventGridAttributes);
            await eventCollector.AddAsync(cloudEvent);
            logger.LogInformation($"Sent event to Event Grid: {propagationEvent}");
        }
    }
}