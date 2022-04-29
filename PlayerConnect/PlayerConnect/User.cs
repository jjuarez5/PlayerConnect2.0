using Newtonsoft.Json;

namespace PlayerConnect
{
    public class User
    {
        [JsonProperty(PropertyName="Id", Required = Required.Default)]
        public string Id { get; set; }
        [JsonProperty(PropertyName="UserName", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty(PropertyName ="Friends", Required = Required.Default)]
        public List<User> Friends { get; set; }
        [JsonProperty(PropertyName ="Email", Required = Required.Always)]
        public string Email { get; set; }
        [JsonProperty(PropertyName ="Password", Required = Required.Always)]
        public string Password { get; set; }

        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                return true;
            }

            return false;
        }
    }
}
