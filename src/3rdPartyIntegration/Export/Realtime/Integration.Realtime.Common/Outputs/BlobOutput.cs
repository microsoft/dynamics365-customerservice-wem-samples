// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Specialized;
using Integration.Realtime.Common.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Integration.Realtime.Common.Outputs
{
    /// <summary>
    /// Represents an Azure Blob Storage output binding for a propagation event.
    /// </summary>
    public class BlobOutput : OutputBase, IBlobOutput
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobOutput"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">An instance of the registered logger.</param>
        public BlobOutput(IConfiguration configuration, ILogger<BlobOutput> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public override async Task WriteEvent<T>(T propagationEvent, Binder binder)
        {
            var blobnameTemplate = configuration.GetValue<string>(Constants.SettingsBlobNameSetting);
            var outputBlobName = string.Format(
                CultureInfo.InvariantCulture,
                blobnameTemplate,
                propagationEvent.EventName,
                DateTime.UtcNow.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture));

            var outputBlobAttributes = new Attribute[]
            {
                new BlobAttribute(outputBlobName, FileAccess.Write),
                new StorageAccountAttribute(Constants.SettingsBlobSettingPrefix),
            };

            var blobClient = await binder.BindAsync<AppendBlobClient>(outputBlobAttributes);

            await blobClient.CreateIfNotExistsAsync();

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes($"{propagationEvent}\r\n")))
            {
                await blobClient.AppendBlockAsync(stream);
            }

            logger.LogInformation($"Appended event '{propagationEvent}' to blob '{outputBlobName}'");
        }
    }
}