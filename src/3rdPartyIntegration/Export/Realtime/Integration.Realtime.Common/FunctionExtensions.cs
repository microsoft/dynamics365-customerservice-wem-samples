// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        /// <summary>
        /// Converts a step event to an agent consult event.
        /// </summary>
        /// <param name="stepEvent">The step event to convert.</param>
        /// <returns>An instance of <see cref="AgentConsultEvent"/> representing the converted event.</returns>
        public static AgentConsultEvent ToAgentConferenceModeEvent(this StepEvent stepEvent)
        {
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.AgentConferenceModeImage);
            if (postImage.Key == null)
            {
                return null;
            }

            // Check if Mode is consult
            var mode = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesMode).Value;
            if (string.IsNullOrWhiteSpace(mode) || mode != Constants.AttributeModeConsult)
            {
                return null;
            }

            // Agent joined consult session
            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesAgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = "Unknown";
            }

            // Agent initiated consult session
            var ownerName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesOwnerId).Value;
            if (string.IsNullOrWhiteSpace(ownerName))
            {
                ownerName = "Unknown";
            }

            // Consult session created on
            var createdOnString = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesCreatedOn).Value;
            DateTime? createdOn = !string.IsNullOrWhiteSpace(createdOnString) ?
                                    DateTime.Parse(
                                        createdOnString,
                                        CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal) :
                                    null;

            return new AgentConsultEvent
            {
                BusinessUnitId = stepEvent.BusinessUnitId,
                OrganizationId = stepEvent.OrganizationId,
                OrganizationName = stepEvent.OrganizationName,
                OperationCreatedOn = stepEvent.OperationCreatedOn,
                InitiatingAgentName = ownerName,
                JoinedAgentName = agentName,
                JoinedOn = createdOn,
                EventDelayInMs = Helpers.GetTimeDifferenceInMs(DateTime.UtcNow, createdOn),
            };
        }

        /// <summary>
        /// Converts a step event to an agent after contact event.
        /// </summary>
        /// <param name="stepEvent">The step event to convert.</param>
        /// <returns>An instance of <see cref="AgentAfterWorkEvent"/> representing the converted event.</returns>
        public static AgentAfterWorkEvent ToAgentAfterWorkEvent(this StepEvent stepEvent)
        {
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.AgentAfterWorkImage);
            if (postImage.Key == null)
            {
                return null;
            }

            // Check is voice channel
            var channel = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesChannel).Value;
            if (string.IsNullOrWhiteSpace(channel) || channel != Constants.AttributesChannelVoiceCall)
            {
                return null;
            }

            // Check if status reason is wrap-up
            var statusReason = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesStatusCode).Value;
            if (string.IsNullOrWhiteSpace(statusReason) || statusReason != Constants.AttributesStatusCodeWrapup)
            {
                return null;
            }

            // Agent name
            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesActiveAgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = "Unknown";
            }

            // After work initiated on
            var wrapUpInitiatedOnstring = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesWrapupInitiatedOn).Value;
            DateTime? wrapUpInitiatedOn = !string.IsNullOrWhiteSpace(wrapUpInitiatedOnstring) ?
                                   DateTime.Parse(
                                       wrapUpInitiatedOnstring,
                                       CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal) :
                                   null;

            return new AgentAfterWorkEvent
            {
                BusinessUnitId = stepEvent.BusinessUnitId,
                OrganizationId = stepEvent.OrganizationId,
                OrganizationName = stepEvent.OrganizationName,
                OperationCreatedOn = stepEvent.OperationCreatedOn,
                AgentName = agentName,
                Status = statusReason,
                WrapUpInitiatedOn = wrapUpInitiatedOn,
                EventDelayInMs = Helpers.GetTimeDifferenceInMs(DateTime.UtcNow, wrapUpInitiatedOn),
            };
        }

        /// <summary>
        /// Converts a step event to an agent after contact event.
        /// </summary>
        /// <param name="stepEvent">The step event to convert.</param>
        /// <returns>An instance of <see cref="AgentAcceptedIncomingWorkEvent"/> representing the converted event.</returns>
        public static AgentAcceptedIncomingWorkEvent ToAgentAcceptIncomingWorkEvent(this StepEvent stepEvent)
        {
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.AgentAcceptIncomingWorkImageNode);
            if (postImage.Key == null)
            {
                return null;
            }

            // Check if agent accepted incoming work
            var agentAccepted = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributeAgentAccepted).Value;
            if (string.IsNullOrWhiteSpace(agentAccepted) || agentAccepted != "Yes")
            {
                return null;
            }

            // channel
            var channel = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesChannel).Value;
            if (string.IsNullOrWhiteSpace(channel))
            {
                channel = "Unknown";
            }

            // Agent name
            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributesActiveAgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = "Unknown";
            }

            // After work initiated on
            var startedOnString = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.AttributeStartedOn).Value;
            DateTime? startedOn = !string.IsNullOrWhiteSpace(startedOnString) ?
                                   DateTime.Parse(
                                       startedOnString,
                                       CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal) :
                                   null;

            return new AgentAcceptedIncomingWorkEvent
            {
                BusinessUnitId = stepEvent.BusinessUnitId,
                OrganizationId = stepEvent.OrganizationId,
                OrganizationName = stepEvent.OrganizationName,
                OperationCreatedOn = stepEvent.OperationCreatedOn,
                AgentName = agentName,
                Channel = channel,
                StartedOn = startedOn,
                EventDelayInMs = Helpers.GetTimeDifferenceInMs(DateTime.UtcNow, startedOn),
            };
        }
    }
}