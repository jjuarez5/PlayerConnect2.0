using Newtonsoft.Json;

namespace PlayerConnect
{
    public class User
    {
        [JsonProperty(PropertyName="Id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName="UserName")]
        public string Name { get; set; }
        [JsonProperty(PropertyName ="Friends")]
        public List<User> Friends { get; set; }
    }
}
