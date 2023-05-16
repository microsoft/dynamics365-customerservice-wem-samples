// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Azure.Messaging;
using Integration.Realtime.Common.Models;
using Microsoft.Azure.Relay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Realtime.Client
{
    /// <summary>
    /// Represents the console app.
    /// </summary>
    public static class Program
    {
        private static IConfiguration configuration;
        private static IServiceProvider serviceProvider;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The commandline args supplied to the application.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            LoadConfiguration();
            RegisterServices();

            var settings = serviceProvider.GetRequiredService<AppSettings>();

            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(settings.SasKeyName, settings.SasKey);
            var listener = new HybridConnectionListener(
                new Uri(string.Format(CultureInfo.InvariantCulture, "sb://{0}/{1}", settings.RelayNamespace, settings.HybridConnectionName)),
                tokenProvider);

            Console.WriteLine("Press Enter to exit.");

            // Subscribe to the status events.
            listener.Connecting += (o, e) => Console.WriteLine("Connecting");
            listener.Offline += (o, e) => Console.WriteLine("Offline");
            listener.Online += (o, e) => Console.WriteLine("Online");

            // Provide an HTTP request handler
            listener.RequestHandler = (context) =>
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    var body = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        return;
                    }

                    var binaryData = BinaryData.FromString(body);
                    var cloudEvent = CloudEvent.Parse(binaryData);

                    if (cloudEvent == null)
                    {
                        Console.WriteLine("Could not parse incoming message.");
                        Console.WriteLine(body);
                        return;
                    }

                    var statusEvent = cloudEvent.Data.ToObjectFromJson<AgentStatusEvent>();
                    DisplayEventDetails(statusEvent);
                }

                // Do something with context.Request.Url, HttpMethod, Headers, InputStream...
                context.Response.StatusCode = HttpStatusCode.NoContent;

                // The context MUST be closed here
                context.Response.Close();
            };

            // Opening the listener establishes the control channel to
            // the Azure Relay service. The control channel is continuously
            // maintained, and is reestablished when connectivity is disrupted.
            await listener.OpenAsync();
            Console.WriteLine("Server listening");

            // Start a new thread that will continuously read the console.
            await Console.In.ReadLineAsync();

            // Close the listener after you exit the processing loop.
            await listener.CloseAsync();
        }

        private static void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appSettings.json");
            configuration = builder.Build();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton(CreateSettings());

            serviceProvider = services.BuildServiceProvider();
        }

        private static AppSettings CreateSettings()
        {
            var settings = new AppSettings();
            configuration.Bind(settings);

            return settings;
        }

        private static void DisplayEventDetails(params AgentStatusEvent[] statusEvents)
        {
            foreach (var statusEvent in statusEvents)
            {
                Console.WriteLine($"{statusEvent.AgentName} changed to '{statusEvent.AgentStatus}' at zulu {statusEvent.StatusChangeTime}. [Org: {statusEvent.OrganizationName}. EventDelay:{statusEvent.EventDelayInMs}ms]");
            }
        }
    }
}