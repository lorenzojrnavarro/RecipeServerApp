using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class BookCollection
    {
        [JsonProperty("total")]
        int bookCount;

        [JsonProperty]
        Book[] books;
    }
}
