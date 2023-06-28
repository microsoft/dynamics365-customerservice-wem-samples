// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.IO;
using System.Threading.Tasks;
using Integration.Realtime.Common;
using Integration.Realtime.Common.Outputs;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Integration.Realtime.Webhook.Functions
{
    /// <summary>
    /// Represents the Azure Function that handles agent conference mode.
    /// </summary>
    public class AgentConferenceFunction : FunctionBase
    {
        private readonly IBlobOutput blobOutput;
        private readonly IEventGridOutput eventGridOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentConferenceFunction"/> class.
        /// </summary>
        /// <param name="serializerSettings">The Json serializer settings.</param>
        /// <param name="settingsOption">The option set for service settings.</param>
        /// <param name="blobOutput">The blob outputter.</param>
        /// <param name="eventGridOutput">The event grid outputter.</param>
        public AgentConferenceFunction(
            JsonSerializerSettings serializerSettings,
            IBlobOutput blobOutput,
            IEventGridOutput eventGridOutput)
            : base(serializerSettings)
        {
            this.blobOutput = blobOutput;
            this.eventGridOutput = eventGridOutput;
        }

        /// <summary>
        /// Webhook Function invoked when agent initiates consult with another agent.
        /// </summary>
        /// <param name="req">The http request.</param>
        /// <param name="binder">The output binder.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [FunctionName("AgentConferenceMode")]
        public async Task AgentConferenceModeInitiated(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            Binder binder,
            ILogger logger)
        {
            logger?.LogInformation("Start processing Agent conference mode..");

            var requestBody = string.Empty;
            using (var reader = new StreamReader(req.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            if (!Helpers.ValidateInput(requestBody, logger, JsonSerializerSettings, out var stepEvent))
            {
                return;
            }

            var agentConferenceModeEvent = stepEvent.ToAgentConferenceModeEvent();

            // Blob output task
            var blobTask = Task.Run(async () => await blobOutput.WriteEvent(agentConferenceModeEvent, binder));

            // Event Grid output task
            var eventGridTask = Task.Run(async () => await eventGridOutput.WriteEvent(agentConferenceModeEvent, binder));

            // Wait for both tasks to complete
            await Task.WhenAll(blobTask, eventGridTask);
        }
    }
}