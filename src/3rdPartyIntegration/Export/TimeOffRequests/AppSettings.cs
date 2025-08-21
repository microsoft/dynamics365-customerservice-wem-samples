// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace TimeOff.SampleClient
{
    /// <summary>
    /// Represents the application settings for the Time Off Sample Client.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the Azure Active Directory Application (client) ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the redirect URI for OAuth authentication.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the Dynamics 365 instance URL.
        /// </summary>
        public string DynamicsUrl { get; set; }
    }
}