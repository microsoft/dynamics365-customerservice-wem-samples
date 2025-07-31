// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Identity.Client;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Integration.Api.Client
{
    /// <summary>
    /// Represents the console app.
    /// </summary>
    public static class Program
    {
        // read the configuration settings from App.config
        public static readonly string TenantId = ConfigurationManager.AppSettings["TenantId"] ?? throw new InvalidOperationException("TenantId is not configured.");
        public static readonly string ClientId = ConfigurationManager.AppSettings["ClientId"] ?? throw new InvalidOperationException("ClientId is not configured.");
        public static readonly string CertThumbprint = ConfigurationManager.AppSettings["CertThumbprint"] ?? throw new InvalidOperationException("CertThumbprint is not configured.");
        public static readonly string DynamicsUrl = (ConfigurationManager.AppSettings["DynamicsUrl"]?.TrimEnd('/'))
            ?? throw new InvalidOperationException("DynamicsUrl is not configured.");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main()
        {
            try
            {
                // Load certificate from store
                var certificate = GetCertificateFromStore(CertThumbprint);

                // Build confidential client
                var app = ConfidentialClientApplicationBuilder.Create(ClientId)
                    .WithCertificate(certificate)
                    .WithAuthority(new Uri($"https://login.microsoftonline.com/{TenantId}"))
                    .Build();

                // Acquire token
                var scopes = new string[] { $"{DynamicsUrl}/.default" };
                var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

                // Get time off types using REST client
                RestClient.GetTimeOffTypes(DynamicsUrl, result.AccessToken).GetAwaiter().GetResult();

                // Get approved time off requests using REST client
                RestClient.GetTimeOffRequests(DynamicsUrl, result.AccessToken).GetAwaiter().GetResult();

                // Get time off types using dataverse client
                DataverseClient.GetTimeOffTypes(DynamicsUrl, result.AccessToken);

                // Get approved time off requests using dataverse client
                DataverseClient.GetTimeOffRequests(DynamicsUrl, result.AccessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a certificate from the store using the specified thumbprint.
        /// </summary>
        /// <param name="thumbprint">The thumbprint of the certificate.</param>
        /// <returns>The retrieved certificate.</returns>
        static X509Certificate2 GetCertificateFromStore(string thumbprint)
        {
            using (var store = new X509Store(StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                if (certs.Count == 0)
                {
                    throw new Exception("Certificate not found.");
                }

                return certs[0];
            }
        }
    }
}
