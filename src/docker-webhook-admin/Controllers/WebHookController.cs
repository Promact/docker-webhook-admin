using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Docker.Webhook.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace Docker.Webhook.Admin.Controllers
{
    [Produces("application/json")]
    [Route("api/webhook")]
    public class WebHookController : Controller
    {
        private readonly IDockerClient _dockerClient;
        private readonly ILogger<WebHookController> _logger;
        public WebHookController(IDockerClient dockerClient,ILogger<WebHookController> logger)
        {
            _dockerClient = dockerClient;
            _logger = logger;
        }

        [Route("test")]
        public async Task<IActionResult> HubAsync(WebHookModel webHook)
        {
            
            await _dockerClient.Images.PullImageAsync(new ImagesPullParameters()
            {
                All = true,
                Parent = "promact/oauth-server",
                Tag = "dev"
            }, null);

            var runningContainer = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters() { All = true });

            await _dockerClient.Containers.RestartContainerAsync(runningContainer.First(x => x.Names.Contains("oauth")).ID, new ConatinerRestartParameters(), CancellationToken.None);
            return Ok();
        }

        public async Task<IActionResult> CreateWebHookAsync()
        {
            return Ok();
        }
    }
}