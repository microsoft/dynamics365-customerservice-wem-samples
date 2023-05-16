// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Integration.Realtime.Common;
using Integration.Realtime.Common.Outputs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Integration.Realtime.Webhook.Startup))]

namespace Integration.Realtime.Webhook
{
    /// <summary>
    /// Represents the function startup class.
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <inheritdoc/>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Add json serializer settings
            builder.Services.AddSingleton(Helpers.GetSerializerSettings());

            // Add output binders
            builder.Services.AddSingleton<IBlobOutput, BlobOutput>();
            builder.Services.AddSingleton<IEventGridOutput, EventGridOutput>();
        }
    }
}