using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RecipeServerApp
{
    class Program
    {        
        static RequestHandler requestHandler;

        static async Task Main(string[] args)
        {
            Program program = new Program();
            requestHandler = new RequestHandler();

            await program.Listen("http://localhost:4444/", 100);            
        }

        public async Task Listen(string prefix, int maxConcurrentRequests)
        {
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add(prefix);
            listener.Start();

            var requests = new HashSet<Task>();
            for (int i = 0; i < maxConcurrentRequests; i++)
                requests.Add(listener.GetContextAsync());

            while (true)
            {
                Task t = await Task.WhenAny(requests);
                requests.Remove(t);

                if (t is Task<HttpListenerContext>)
                {
                    var context = (t as Task<HttpListenerContext>).Result;
                    requests.Add(ProcessRequestAsync(context));
                    requests.Add(listener.GetContextAsync());
                }
            }
        }

        public async Task ProcessRequestAsync(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;

            string responseString = requestHandler.HandleRequest(context.Request);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;

            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}