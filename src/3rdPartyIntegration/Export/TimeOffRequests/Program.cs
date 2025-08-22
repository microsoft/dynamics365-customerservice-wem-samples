// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TimeOff.SampleClient
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main()
        {
            LoadConfiguration();
            RegisterServices();

            var dvClient = serviceProvider.GetRequiredService<IDataverseClient>();

            // Get time off types
            var torTypes = await dvClient.GetTimeOffTypes();

            // Print time off types
            Console.WriteLine("Time Off Types");
            Console.WriteLine("==============");
            foreach (var torType in torTypes)
            {
                Console.WriteLine($"Id: {torType.Id} | Name: {torType.msdyn_Name}");
            }

            Console.WriteLine(" ");

            // Get all time off requests
            var torRequests = await dvClient.GetTimeOffRequests(false);

            // Print all time off requests
            Console.WriteLine("Time Off Requests");
            Console.WriteLine("=================");
            foreach (var torRequest in torRequests)
            {
                Console.WriteLine($"Id: {torRequest.Id} Name: {torRequest.msdyn_Name}");
                Console.WriteLine($"  Start: {torRequest.msdyn_StartTime} End: {torRequest.msdyn_EndTime}");
                Console.WriteLine($"  Status: {torRequest.msdyn_RequestStatus}");
                Console.WriteLine($"  Time off type: {torRequest.msdyn_TimeOffType.Id}");
                Console.WriteLine($"  Is full day: {torRequest.msdyn_IsFullDay}");
                Console.WriteLine($"  Agent (Bookable Resource): {torRequest.msdyn_Agent.Id}");
                Console.WriteLine(" ");
            }
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

            services.AddSingleton(configuration);
            services.AddSingleton(CreateSettings());
            services.AddSingleton<IDataverseClient, DataverseClient>();

            serviceProvider = services.BuildServiceProvider();
        }

        private static AppSettings CreateSettings()
        {
            var settings = new AppSettings();
            configuration.Bind(settings);

            return settings;
        }
    }
}