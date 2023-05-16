// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace Integration.Realtime.Common.Models
{
    /// <summary>
    /// Represents a post entity image value.
    /// </summary>
    public class PostEntityImageValue
    {
        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        public List<KeyValuePair<string, object>> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the formatted values.
        /// </summary>
        public List<KeyValuePair<string, string>> FormattedValues { get; set; }

        /// <summary>
        /// Gets or sets the logical name for the entity.
        /// </summary>
        public string LogicalName { get; set; }

        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        public Guid Id { get; set; }
    }
}