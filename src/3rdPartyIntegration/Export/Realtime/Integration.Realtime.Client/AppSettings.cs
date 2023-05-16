// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace Integration.Realtime.Client
{
    /// <summary>
    /// Represents the application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the relay name space.
        /// </summary>
        public string RelayNamespace { get; set; }

        /// <summary>
        /// Gets or sets the hybrid connection name.
        /// </summary>
        public string HybridConnectionName { get; set; }

        /// <summary>
        /// Gets or sets the sas key name.
        /// </summary>
        public string SasKeyName { get; set; }

        /// <summary>
        /// Gets or sets the sas key.
        /// </summary>
        public string SasKey { get; set; }
    }
}