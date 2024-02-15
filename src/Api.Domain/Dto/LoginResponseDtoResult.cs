

using Newtonsoft.Json;

namespace Api.Domain.Dto
{
    public class LoginResponseDtoResult
    {
        [JsonProperty("authenticated")]
        public bool authenticated { get; set; }
        [JsonProperty("createDate")]
        public string createDate { get; set; }
        [JsonProperty("expirationDate")]
        public string expirationDate { get; set; }
        [JsonProperty("acessToken")]
        public string acessToken { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }

    }
}
