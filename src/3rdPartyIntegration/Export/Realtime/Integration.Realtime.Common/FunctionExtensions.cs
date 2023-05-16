// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;
using System.Linq;
using Integration.Realtime.Common.Models;

namespace Integration.Realtime.Common
{
    /// <summary>
    /// Represents a set of extension methods.
    /// </summary>
    public static class FunctionExtensions
    {
        /// <summary>
        /// Converts a step event to an agent status event.
        /// </summary>
        /// <param name="stepEvent">The step event to convert.</param>
        /// <returns>An instance of <see cref="AgentStatusEvent"/> representing the converted event.</returns>
        public static AgentStatusEvent ToAgentStatusEvent(this StepEvent stepEvent)
        {
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.AgentStatusHistoryImage);
            if (postImage.Key == null)
            {
                return null;
            }

            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesAgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = "Unknown";
            }

            var agentStatus = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesPresenceId).Value;
            if (string.IsNullOrWhiteSpace(agentStatus))
            {
                agentStatus = "Unknown";
            }

            var startTimeString = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesStartTime).Value;
            DateTime? startTime = !string.IsNullOrWhiteSpace(startTimeString) ?
                                    DateTime.Parse(
                                        startTimeString,
                                        CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal) :
                                    null;

            return new AgentStatusEvent
            {
                BusinessUnitId = stepEvent.BusinessUnitId,
                OrganizationId = stepEvent.OrganizationId,
                OrganizationName = stepEvent.OrganizationName,
                OperationCreatedOn = stepEvent.OperationCreatedOn,
                AgentName = agentName,
                AgentStatus = agentStatus,
                StatusChangeTime = startTime,
                EventDelayInMs = Helpers.GetTimeDifferenceInMs(DateTime.UtcNow, startTime),
            };
        }
    }
}