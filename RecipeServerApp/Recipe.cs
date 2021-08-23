using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RecipeServerApp
{
    class Recipe
    {
        [JsonProperty]
        public int ID { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }
        
        [JsonProperty]
        public int Calories { get; private set; }

        [JsonProperty]
        public string ImageURL { get; private set; }

        [JsonProperty]
        public string Ingredients { get; private set; }

        [JsonProperty]
        public string Instructions { get; private set; }
    }
}
