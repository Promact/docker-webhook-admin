using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Docker.Webhook.Admin.Models
{
    public class DockerOptions
    {
        private string _dockerApiUrl;

        public string DockerApiUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_dockerApiUrl))
                {
                    if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        _dockerApiUrl = "npipe://./pipe/docker_engine_windows";
                    else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        _dockerApiUrl = "unix:///var/run/docker.sock";
                    }
                }
                return _dockerApiUrl;
            }
            set
            {
                _dockerApiUrl = value;
            }
        }
    }
}
