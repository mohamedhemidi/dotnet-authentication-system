using System;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace backend_core.Domain.Models
{

    public partial class FacebookUserInfoResult
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("picture")]
        public FacebookPicture? Picture { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }
    }

    public partial class FacebookPicture
    {
        [JsonPropertyName("data")]
        public FacebookPictureData? Data { get; set; }
    }

    public partial class FacebookPictureData
    {
        [JsonPropertyName("height")]
        public long Height { get; set; }

        [JsonPropertyName("is_silhouette")]
        public bool IsSilhouette { get; set; }

        [JsonPropertyName("url")]
        public Uri? Url { get; set; }

        [JsonPropertyName("width")]
        public long Width { get; set; }
    }
}
