using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Docker.Webhook.Admin.Utility
{
    public class StaticHtmlActionResult : IActionResult
    {
        private const string HtmlMediaType = "text/html";

        private readonly string _path;             
        
        public StaticHtmlActionResult(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            this._path = path;            
        }
                       

        public Task ExecuteResultAsync(ActionContext context)
        {
            string html;
            using (Stream manifestResourceStream = File.OpenRead(_path))
            {
                using (StreamReader streamReader = new StreamReader(manifestResourceStream))
                {
                    html = streamReader.ReadToEnd();
                }
            }
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);            
            httpResponseMessage.Content = new StringContent(html, Encoding.UTF8, "text/html");            
            return Task.FromResult<HttpResponseMessage>(httpResponseMessage);
        }
    }
}
