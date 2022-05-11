using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServerApp
{
    class Book
    {
        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("title_long")]
        public string Title_Long { get; private set; }

        [JsonProperty("date_published")]
        public string Date_Published { get; private set; }

        [JsonProperty("authors")]
        public string[] Authors { get; private set; }

        [JsonProperty("publisher")]
        public string Publisher { get; private set; }


        [JsonProperty("image")]
        public string Image { get; private set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; private set; }


        [JsonProperty("subjects")]
        public string[] Subjects { get; private set; }

        [JsonProperty("isbn")]
        public string ISBN { get; private set; }

        [JsonProperty("isbn13")]
        public string ISBN13 { get; private set; }
    }
}
