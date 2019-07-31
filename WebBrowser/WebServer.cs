using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebBrowser
{
    public class WebServer
    {
        HttpListener httpListener;
        FileSystem fileSystem;
        public WebServer()
        {
            httpListener = new HttpListener();
            fileSystem = new FileSystem();
        }

        public void AddPrefix(string prefix)
        {
            httpListener.Prefixes.Add(prefix);
        }

        public void Start()
        {
            httpListener.Start();
            while(true)
            {
                HttpListenerContext context = httpListener.GetContext();
                string fileContents = fileSystem.GetFileContents(context.Request.RawUrl);
                if (fileContents == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    RenderResponse(context, "");
                }
                else
                {
                    PrepareResponse(context, fileContents);
                    RenderResponse(context, fileContents);
                }
            }
        }

        private void PrepareResponse(HttpListenerContext context, string fileContents)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(fileContents);
        }


        private void RenderResponse(HttpListenerContext context, string fileContents)
        {
            using (Stream stream = context.Response.OutputStream)
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(fileContents);
                }
            }
            Console.WriteLine("Rendered {0}", context.Request.RawUrl);
        }
    }
}

