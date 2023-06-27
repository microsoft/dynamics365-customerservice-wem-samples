// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents settings constants.
        /// </summary>
        public static class SettingConstants
        {
            /// <summary>
            /// Represents the setting prefix for blob storage settings.
            /// </summary>
            public const string SettingsBlobSettingPrefix = "BlobStorage";

            /// <summary>
            /// Represents the setting name for the blob name.
            /// </summary>
            public const string SettingsBlobNameSetting = "BlobName";

            /// <summary>
            /// Represents the setting for event grid endpoint setting.
            /// </summary>
            public const string SettingsEventGridEndpointSetting = "EventGridEndpoint";

            /// <summary>
            /// Represents the setting for event grid key setting.
            /// </summary>
            public const string SettingsEventGridKeySetting = "EventGridKey";
        }

        /// <summary>
        /// Represents attribute names.
        /// </summary>
        public static class Attributes
        {
            /// <summary>
            /// Represents the attribute name of the agent id.
            /// </summary>
            public const string AgentId = "msdyn_agentid";

            /// <summary>
            /// Represents the attribute name of the presence id.
            /// </summary>
            public const string PresenceId = "msdyn_presenceid";

            /// <summary>
            /// Represents the attribute name of the start time.
            /// </summary>
            public const string StartTime = "msdyn_starttime";

            /// <summary>
            /// Represents the key of the agent mode.
            /// </summary>
            public const string Mode = "msdyn_mode";

            /// <summary>
            /// Represents the value of agent mode 'consult'.
            /// </summary>
            public const string ModeConsult = "Consult";

            /// <summary>
            /// Represents the attribute name of the owner id.
            /// </summary>
            public const string OwnerId = "ownerid";

            /// <summary>
            /// Represents the attribute name of the created on.
            /// </summary>
            public const string CreatedOn = "createdon";

            /// <summary>
            /// Represents the attribute name of the channel.
            /// </summary>
            public const string Channel = "msdyn_channel";

            /// <summary>
            /// Represents the attribute name of the statuscode.
            /// </summary>
            public const string StatusCode = "statuscode";

            /// <summary>
            /// Represents the value of statuscode attribute.
            /// </summary>
            public const string StatusCodeWrapup = "Wrap-up";

            /// <summary>
            /// Represents the attribute name of the active agent id.
            /// </summary>
            public const string ActiveAgentId = "msdyn_activeagentid";

            /// <summary>
            /// Represents the attribute name of the wrapup initiated on.
            /// </summary>
            public const string WrapupInitiatedOn = "msdyn_wrapupinitiatedon";

            /// <summary>
            /// Represents the attribute name of agent accepted.
            /// </summary>
            public const string IsAgentAccepted = "msdyn_isagentaccepted";

            /// <summary>
            /// Represents the attribute name of started on.
            /// </summary>
            public const string StartedOn = "msdyn_startedon";

            /// <summary>
            /// Represents unknown constant.
            /// </summary>
            public const string Unknown = "Unknown";
        }

        /// <summary>
        /// Represents post image node constants.
        /// </summary>
        public static class PostImageNodes
        {
            /// <summary>
            /// Represents the key of agent status history image node.
            /// </summary>
            public const string AgentStatusHistoryImage = "AgentStatusHistoryImage";

            /// <summary>
            /// Represents the key of the agent conference mode image node.
            /// </summary>
            public const string AgentConferenceModeImage = "CMWPostImage";

            /// <summary>
            /// Represents the attribute name of the agent after work image node.
            /// </summary>
            public const string AgentAfterWorkImage = "AAWPostImage";

            /// <summary>
            /// Represents the node name of the agent accept incoming work post image.
            /// </summary>
            public const string AgentAcceptIncomingWorkImage = "AAIWPostImage";
        }
    }
}