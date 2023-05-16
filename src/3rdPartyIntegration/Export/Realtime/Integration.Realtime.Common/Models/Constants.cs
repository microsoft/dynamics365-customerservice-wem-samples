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
    }
}