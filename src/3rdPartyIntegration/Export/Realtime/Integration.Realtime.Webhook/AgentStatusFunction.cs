// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.IO;
using System.Threading.Tasks;
using Integration.Realtime.Common;
using Integration.Realtime.Common.Models;
using Integration.Realtime.Common.Outputs;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Integration.Realtime.Webhook
{
    /// <summary>
    /// Represents the Azure Function that handles agent status updates.
    /// </summary>
    public class AgentStatusFunction : FunctionBase
    {
        private readonly IBlobOutput blobOutput;
        private readonly IEventGridOutput eventGridOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentStatusFunction"/> class.
        /// </summary>
        /// <param name="serializerSettings">The Json serializer settings.</param>
        /// <param name="settingsOption">The option set for service settings.</param>
        /// <param name="blobOutput">The blob outputter.</param>
        /// <param name="eventGridOutput">The event grid outputter.</param>
        public AgentStatusFunction(
            JsonSerializerSettings serializerSettings,
            IBlobOutput blobOutput,
            IEventGridOutput eventGridOutput)
            : base(serializerSettings)
        {
            this.blobOutput = blobOutput;
            this.eventGridOutput = eventGridOutput;
        }

        /// <summary>
        /// Webhook Function invoked on every agent status update.
        /// </summary>
        /// <param name="req">The http request.</param>
        /// <param name="binder">The output binder.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [FunctionName("AgentStatus")]
        public async Task AgentStatusUpdated(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            Binder binder,
            ILogger logger)
        {
            logger?.LogInformation("Start processing AgentState..");

            var requestBody = string.Empty;
            using (var reader = new StreamReader(req.Body))
            {
                requestBody = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(requestBody))
            {
                logger?.LogWarning("Request body was empty.");
                return;
            }

            var stepEvent = JsonConvert.DeserializeObject<StepEvent>(requestBody, JsonSerializerSettings);
            if (stepEvent == null)
            {
                logger.LogWarning("Input step event was not in the expected schema.");
                return;
            }

            var agentStatusEvent = stepEvent.ToAgentStatusEvent();

            // Blob output task
            var blobTask = Task.Run(async () => await blobOutput.WriteEvent(agentStatusEvent, binder));

            // Event Grid output task
            var eventGridTask = Task.Run(async () => await eventGridOutput.WriteEvent(agentStatusEvent, binder));

            // Wait for both tasks to complete
            await Task.WhenAll(blobTask, eventGridTask);
        }
    }
}