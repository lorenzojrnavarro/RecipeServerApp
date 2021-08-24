using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeServerApp
{
    class JsonRecipeController : IRecipeController
    {
        public Recipe[] recipes;

        private const string jsonPath = "Recipes.json";

        public JsonRecipeController()
        {
            StreamReader stream = new StreamReader(jsonPath);
            string json = stream.ReadToEnd();

            JObject o = JObject.Parse(json);
            recipes = JsonConvert.DeserializeObject<Recipe[]>(o["recipes"].ToString());

            stream.Close();
        }

        public string GetRecipes(string recipeName, string allergenIds)
        {
            return JsonConvert.SerializeObject(recipes);
        }

        public string GetUserFavoriteRecipes(string ids)
        {
            throw new NotImplementedException();
        }

        public string GetRandomRecipe()
        {
            throw new NotImplementedException();
        }

        public string GetRecipeByID(string id)
        {
            throw new NotImplementedException();
        }
        public void AddNewRecipe(string recipeName, string recipeURL, string recipeIngredients, string recipeAllergens, string recipeProcedure, string recipeCalories)
        {

        }
    }
}
