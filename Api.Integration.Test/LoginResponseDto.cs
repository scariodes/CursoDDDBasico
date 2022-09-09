using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Api.Integration.Test
{
    public class LoginResponseDto
    {
        [JsonProperty("authenticated")]
        public bool Authenticated { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("UserName")]
        public string userName { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}