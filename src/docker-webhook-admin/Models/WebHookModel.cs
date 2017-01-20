using System;
using Newtonsoft.Json;

namespace Docker.Webhook.Admin.Models
{
    public class WebHookModel
    {
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("pushed_at")]
        public string PushedAt { get; set; }

        [JsonProperty("push_data")]
        public PushedDataModel PushedData { get; set; }

        [JsonProperty("pusher")]
        public string Pusher { get; set; }

        [JsonProperty("repository")]
        public RepositoryModel Repository { get; set; }
    }
}
/*
{
  "callback_url": "https://registry.hub.docker.com/u/svendowideit/testhook/hook/2141b5bi5i5b02bec211i4eeih0242eg11000a/",
  "push_data": {
    "images": [
        "27d47432a69bca5f2700e4dff7de0388ed65f9d3fb1ec645e2bc24c223dc1cc3",
        "51a9c7c1f8bb2fa19bcd09789a34e63f35abb80044bc10196e304f6634cc582c",
        "..."
    ],
    "pushed_at": 1.417566161e+09,
    "pusher": "trustedbuilder"
  }  
} 
 */
