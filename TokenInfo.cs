using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;

namespace AUTOAPI
{
        public class TokenInfo
        {
            public string? access_token { get; set; }
            public string? token_type { get; set; }
            public int expires_in { get; set; }

            [JsonPropertyName(".issued")]
            public string? issued { get; set; }

            [JsonPropertyName(".expires")]
            public string? expires { get; set; }

            public DateTime? ExpirationTime { get; set; }
        }
}
