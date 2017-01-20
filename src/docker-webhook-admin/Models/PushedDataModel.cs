using Newtonsoft.Json;

namespace Docker.Webhook.Admin.Models
{
    public class PushedDataModel
    {
        [JsonProperty("images")]
        public string[] Images { get; set; }
    }
}