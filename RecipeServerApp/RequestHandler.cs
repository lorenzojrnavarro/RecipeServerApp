using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecipeServerApp
{
    public class RequestHandler
    {
        delegate string FunctionHandler(Dictionary<string, string> param);
        private readonly Dictionary<string, FunctionHandler> routeMap;
        private IRecipeController recipeController;
        
        public RequestHandler()
        {
            routeMap = new Dictionary<string, FunctionHandler>();
            recipeController = new DatabaseRecipeController();

            routeMap.Add("recipe", HandleRecipeIDLookup);
            routeMap.Add("recipeRandom", HandleRandomRecipe);
            routeMap.Add("recipeSearch", HandleRecipeSearch);
            routeMap.Add("recipeUpload", HandleRecipeUpload);            
            routeMap.Add("userFavorites", HandleUserFavoriteRecipeLookup);
        }

        public string HandleRequest(HttpListenerRequest request)
        {
            string route = request.RawUrl.Substring(2);
            string text = new StreamReader(request.InputStream).ReadToEnd();

            //var dict = text.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries)
            //   .Select(part => part.Split('='))
            //   .ToDictionary(split => split[0], split => split[1]);

            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            return routeMap[route].Invoke(dict);
        }

        private string HandleRecipeIDLookup(Dictionary<string, string> param)
        {
            return recipeController.GetRecipeByID(param["recipeID"]);
        }

        private string HandleRandomRecipe(Dictionary<string, string> param)
        {
            return recipeController.GetRandomRecipe();
        }

        private string HandleUserFavoriteRecipeLookup(Dictionary<string, string> param)
        {
            return recipeController.GetUserFavoriteRecipes(param["userFavorites"]);
        }

        private string HandleRecipeSearch(Dictionary<string, string> param)
        {
            return recipeController.GetRecipes(param["recipeName"], param["recipeAllergens"]);
        }

        private string HandleRecipeUpload(Dictionary<string, string> param)
        {   
            string recipeImageName = Guid.NewGuid().ToString() + param["recipeName"] + ".png";
            AmazonUploader.SendMyFileToS3(param["recipeImageBytes"], "morgothscookbookbucket", recipeImageName);

            string imagePath = AmazonUploader.bucketPath + recipeImageName;
            recipeController.AddNewRecipe(param["recipeName"], imagePath, param["recipeIngredients"], param["recipeAllergens"], param["recipeProcedure"], param["recipeCalories"]);
            return string.Empty;
        }
    }    
}
