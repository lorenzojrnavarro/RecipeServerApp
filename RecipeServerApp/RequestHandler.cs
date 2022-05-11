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
    public class RequestHandler
    {
        delegate string FunctionHandler(Dictionary<string, string> param);
        private readonly Dictionary<string, FunctionHandler> routeMap;
        private IBookController bookController;

        public RequestHandler()
        {
            routeMap = new Dictionary<string, FunctionHandler>();
            bookController = new APIBookController();

            routeMap.Add("GetBookDetail", HandleBookDetailSearch);
        }

        public string HandleRequest(HttpListenerRequest request)
        {
            string route = request.RawUrl.Substring(1);
            string text = new StreamReader(request.InputStream).ReadToEnd();

            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            if (text == string.Empty)
                return string.Empty;

            return routeMap[route].Invoke(dict);
        }

        private string HandleBookDetailSearch(Dictionary<string, string> param)
        {
            NonBlockingConsole.WriteLine("Searching for book with value: " + param["searchValue"]);
            return JsonConvert.SerializeObject(bookController.SearchBook(param["searchValue"]));
        }
    }
}
