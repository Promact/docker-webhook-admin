using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Docker.Webhook.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace Docker.Webhook.Admin.Controllers
{
    [Produces("application/json")]
    [Route("api/webhook")]
    public class WebHookController : Controller
    {
        private readonly IOptions<Files> _configuration;

        public WebHookController(IOptions<Files> configuration)
        {
            _configuration = configuration;
        }

        [Route("test")]
        public async Task<IActionResult> HubAsync(WebHookModel webHook)
        {
            Console.WriteLine("=========Method Start==========");
            string webhookData = "";
            using (StreamReader reader = new StreamReader(this.Request.Body))
            {
                webhookData = await reader.ReadToEndAsync();
            }
            Console.WriteLine("=========Received Webhook==========");
            System.IO.File.AppendAllText("/var/log/webhook.out.log", webhookData);
            WebHookModel model = JsonConvert.DeserializeObject<WebHookModel>(webhookData);
            Console.WriteLine("=========Deserialize Data==========");
            await Task.Run(() =>
            {
                Console.WriteLine("=========Task Started==========");
                string processFile = "/bin/bash";
                ProcessStartInfo processStartInfo = new ProcessStartInfo(processFile)
                {
                    LoadUserProfile = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    Arguments = "-c " + _configuration.Value.ScriptFile
                };
                var process = new Process
                {
                    EnableRaisingEvents = true,
                    StartInfo = processStartInfo
                };
                try
                {
                    Console.WriteLine("=========Starting Process==========");
                    process.Start();
                    Console.WriteLine("=========Process executed with 0==========");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("=========Exception==========");
                    Console.WriteLine(ex.StackTrace);
                    System.IO.File.AppendAllText("/var/log/webhook.err.log", ex.Message);
                    System.IO.File.AppendAllText("/var/log/webhook.err.log", ex.StackTrace);
                }
                finally
                {
                    string errors = process.StandardError.ReadToEnd();
                    string output = process.StandardOutput.ReadToEnd();

                    System.IO.File.AppendAllText("/var/log/webhook.err.log", errors);
                    System.IO.File.AppendAllText("/var/log/webhook.out.log", output);
                    process.Dispose();
                }
                Console.WriteLine("=========Task Executed==========");
            });            
            return Ok();
        }
    }
}