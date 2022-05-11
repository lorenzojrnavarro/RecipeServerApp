using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class APIBookController : IBookController
    {        
        const string WEBSERVICE_URL = "https://api2.isbndb.com/";
        const string key = "47116_002ba0f088d6858661207934435c037d";

        public BookCollection SearchBook(string searchParam) 
        {
            BookCollection collection = null;
            try
            {
                StringBuilder urlBuilder = new StringBuilder(WEBSERVICE_URL);
                urlBuilder.Append(@"books/");
                urlBuilder.Append(searchParam);

                var webRequest = WebRequest.Create(urlBuilder.ToString());

                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.ContentType = "application/json";
                    webRequest.Headers["Authorization"] = key;

                    //Get the response 
                    WebResponse webResponse = webRequest.GetResponse();
                    Stream receiveStream = webResponse.GetResponseStream();
                    StreamReader reader = new StreamReader(receiveStream);

                    string content = reader.ReadToEnd();

                    collection = JsonConvert.DeserializeObject<BookCollection>(content);
                  
                }
            }
            catch (Exception ex)
            {
                NonBlockingConsole.WriteLine(ex.ToString());
            }

            return collection;
        }

    }
}
