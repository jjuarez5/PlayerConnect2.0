using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlayerConnect
{
    public class User
    {
        // **START HERE, SEE HOW TO MAKE SOME REQUIRED AND SOME NOT AS IT'S AFFECTING PAYLOAD RESPONSE
        [JsonProperty(PropertyName="id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName="UserName")]
        public string Name { get; set; }
        [JsonProperty(PropertyName ="Friends")]
        public List<User> Friends { get; set; }

        [Required]
        [JsonProperty(PropertyName ="Email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty(PropertyName ="Password")]
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
