﻿// Copyright (c) Microsoft Corporation.
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
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.PostImageNodes.AgentStatusHistoryImage);
            if (postImage.Key == null)
            {
                return null;
            }

            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.AgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = Constants.Attributes.Unknown;
            }

            var agentStatus = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.PresenceId).Value;
            if (string.IsNullOrWhiteSpace(agentStatus))
            {
                agentStatus = Constants.Attributes.Unknown;
            }

            var startTimeString = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.StartTime).Value;
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
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.PostImageNodes.AgentConferenceModeImage);
            if (postImage.Key == null)
            {
                return null;
            }

            // Check if Mode is consult
            var mode = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.Mode).Value;
            if (string.IsNullOrWhiteSpace(mode) || mode != Constants.Attributes.ModeConsult)
            {
                return null;
            }

            // Agent joined consult session
            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.AgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = Constants.Attributes.Unknown;
            }

            // Agent initiated consult session
            var ownerName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.OwnerId).Value;
            if (string.IsNullOrWhiteSpace(ownerName))
            {
                ownerName = Constants.Attributes.Unknown;
            }

            // Consult session created on
            var createdOnString = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.CreatedOn).Value;
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
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.PostImageNodes.AgentAfterWorkImage);
            if (postImage.Key == null)
            {
                return null;
            }

            // Check if status reason is wrap-up
            var statusReason = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.StatusCode).Value;
            if (string.IsNullOrWhiteSpace(statusReason) || statusReason != Constants.Attributes.StatusCodeWrapup)
            {
                return null;
            }

            // Agent name
            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.ActiveAgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = Constants.Attributes.Unknown;
            }

            // After work initiated on
            var wrapUpInitiatedOnstring = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.WrapupInitiatedOn).Value;
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
            var postImage = stepEvent.PostEntityImages.FirstOrDefault(p => p.Key == Constants.PostImageNodes.AgentAcceptIncomingWorkImage);
            if (postImage.Key == null)
            {
                return null;
            }

            // Check if agent accepted incoming work
            var agentAccepted = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.IsAgentAccepted).Value;
            if (string.IsNullOrWhiteSpace(agentAccepted) || agentAccepted != "Yes")
            {
                return null;
            }

            // channel
            var channel = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.Channel).Value;
            if (string.IsNullOrWhiteSpace(channel))
            {
                channel = Constants.Attributes.Unknown;
            }

            // Agent name
            var agentName = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.ActiveAgentId).Value;
            if (string.IsNullOrWhiteSpace(agentName))
            {
                agentName = Constants.Attributes.Unknown;
            }

            // After work initiated on
            var startedOnString = postImage.Value.FormattedValues.FirstOrDefault(f => f.Key == Constants.Attributes.StartedOn).Value;
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