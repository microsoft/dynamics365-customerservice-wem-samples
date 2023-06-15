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
        /// Represents the key of agent status history image node.
        /// </summary>
        public const string AgentStatusHistoryImage = "AgentStatusHistoryImage";

        /// <summary>
        /// Represents the attribute name of the agent id.
        /// </summary>
        public const string AttributesAgentId = "msdyn_agentid";

        /// <summary>
        /// Represents the attribute name of the presence id.
        /// </summary>
        public const string AttributesPresenceId = "msdyn_presenceid";

        /// <summary>
        /// Represents the attribute name of the start time.
        /// </summary>
        public const string AttributesStartTime = "msdyn_starttime";

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

        /// <summary>
        /// Represents the key of the agent conference mode image node.
        /// </summary>
        public const string AgentConferenceModeImage = "CMWPostImage";

        /// <summary>
        /// Represents the key of the agent mode.
        /// </summary>
        public const string AttributesMode = "msdyn_mode";

        /// <summary>
        /// Represents the value of agent mode 'consult'.
        /// </summary>
        public const string AttributeModeConsult = "Consult";

        /// <summary>
        /// Represents the attribute name of the owner id.
        /// </summary>
        public const string AttributesOwnerId = "msdyn_ownerid";

        /// <summary>
        /// Represents the attribute name of the created on.
        /// </summary>
        public const string AttributesCreatedOn = "createdon";

        /// <summary>
        /// Represents the attribute name of the agent after work image node.
        /// </summary>
        public const string AgentAfterWorkImage = "AAWPostImage";

        /// <summary>
        /// Represents the attribute name of the channel
        /// </summary>
        public const string AttributesChannel = "msdyn_channel";

        /// <summary>
        /// Represents the value of channel attribute.
        /// </summary>
        public const string AttributesChannelVoiceCall = "; Voice call;";

        /// <summary>
        /// Represents the attribute name of the statuscode;
        /// </summary>
        public const string AttributesStatusCode = "statuscode";

        /// <summary>
        /// Represents the value of statuscode attribute
        /// </summary>
        public const string AttributesStatusCodeWrapup = "Wrap-up";

        /// <summary>
        /// Represents the attribute name of the active agent id.
        /// </summary>
        public const string AttributesActiveAgentId = "msdyn_activeagentid";

        /// <summary>
        /// Represents the attribute name of the wrapup initiated on
        /// </summary>
        public const string AttributesWrapupInitiatedOn = "msdyn_wrapupinitiatedon";

        /// <summary>
        /// Represents the node name of the agent accept incoming work post image.
        /// </summary>
        public const string AgentAcceptIncomingWorkImageNode = "AAIWPostImage";

        /// <summary>
        /// Represents the attribute name of agent accepted.
        /// </summary>
        public const string AttributeAgentAccepted = "msdyn_isagentaccepted";

        /// <summary>
        /// Represents the attribute name of started on
        /// </summary>
        public const string AttributeStartedOn= "msdyn_startedon";
    }
}