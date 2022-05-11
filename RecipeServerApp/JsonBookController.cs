using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class JsonBookController : IBookController
    {
        public Book[] books;

        private const string jsonPath = "Books.json";

        public JsonBookController()
        {
            StreamReader stream = new StreamReader(jsonPath);
            string json = stream.ReadToEnd();

            JObject o = JObject.Parse(json);
            books = JsonConvert.DeserializeObject<Book[]>(o["books"].ToString());

            stream.Close();
        }

        public BookCollection SearchBook(string searchParam)
        {
            throw new NotImplementedException();
        }
    }
}
