// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Integration.Realtime.Common
{
    /// <summary>
    /// Represents a set of helper methods.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Gets the time difference in milliseconds between two timestamps.
        /// </summary>
        /// <param name="t1">The first time stamp.</param>
        /// <param name="t2">The second timestamp.</param>
        /// <returns>A <see cref="double"/> representing the time difference in milliseconds.</returns>
        public static double GetTimeDifferenceInMs(DateTime? t1, DateTime? t2)
        {
            if (!t1.HasValue || !t2.HasValue)
            {
                return -1;
            }

            return (t1.Value - t2.Value).TotalMilliseconds;
        }

        /// <summary>
        /// Gets the json serializer settings.
        /// </summary>
        /// <returns>An instance of <see cref="JsonSerializerSettings"/> representing the serializer settings.</returns>
        public static JsonSerializerSettings GetSerializerSettings()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new PascalCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false,
                        ProcessDictionaryKeys = false,
                    },
                },
                DateParseHandling = DateParseHandling.DateTime,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
            };

            serializerSettings.Converters.Add(new StringEnumConverter
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            });
            return serializerSettings;
        }
    }
}