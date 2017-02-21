using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Webhook.Admin.Models
{
    public class DockerOptions
    {
        public string DockerApiUrl { get; set; } = "/var/run/docker.sock";
    }
}
