// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Newtonsoft.Json;

namespace Integration.Realtime.Webhook
{
    /// <summary>
    /// Represents the base class for a function.
    /// </summary>
    public abstract class FunctionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionBase"/> class.
        /// </summary>
        /// <param name="jsonSerializerSettings">The json serializer settings.</param>
        protected FunctionBase(JsonSerializerSettings jsonSerializerSettings) => JsonSerializerSettings = jsonSerializerSettings;

        /// <summary>
        /// Gets the json serializer settings.
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; private set; }
    }
}