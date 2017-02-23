using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Webhook.Admin.Controllers
{
    [Produces("application/json")]
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly IDockerClient _dockerClient;

        public ImageController(IDockerClient dockerClient)
        {
            _dockerClient = dockerClient;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetImages()
        {
            return Ok(await _dockerClient.Images.ListImagesAsync(new ImagesListParameters() {All = true}));
        }
    }
}