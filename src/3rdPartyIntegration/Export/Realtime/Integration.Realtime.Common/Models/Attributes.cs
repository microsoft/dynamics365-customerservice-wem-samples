using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Realtime.Common.Models
{
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
        /// Represents the attribute name of started on
        /// </summary>
        public const string StartedOn = "msdyn_startedon";
    }
}