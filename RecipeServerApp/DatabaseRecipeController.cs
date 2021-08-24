using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;

namespace RecipeServerApp
{
    class DatabaseRecipeController : IRecipeController
    {
        //private readonly string connectionString = @"Data Source=192.168.4.42;Initial Catalog=MORGOTHS_COOKBOOK.MDF;User ID=RecipeAppServer;Password=moe380";
        private readonly string connectionString = @"Data Source=76.175.108.117;Initial Catalog=MORGOTHS_COOKBOOK.MDF;User ID=RecipeAppServer;Password=moe380";

        public DatabaseRecipeController()
        {
            
        }

        public string GetRecipes(string recipeName, string allergenIds)
        {
            List<Recipe> list;
            List<Recipe> filteredList = new List<Recipe>();

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                list = connection.Query<Recipe>("dbo.SelectRecipeByFilter @RecipeName", new { RecipeName = recipeName }).ToList();
            }

            List<string> splitAllergenIds = allergenIds.Split(',').ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string[] recipeAllergens = list[i].AllergenTags.Split(',');
                bool containsAllergen = false;

                for (int j = 0; j < recipeAllergens.Length; j++)
                {
                    if (splitAllergenIds.Contains(recipeAllergens[j]))
                    {
                        containsAllergen = true;
                        break;
                    }
                }

                if (!containsAllergen) filteredList.Add(list[i]);
            }

            return JsonConvert.SerializeObject(filteredList);
        }

        public string GetUserFavoriteRecipes(string ids)
        {
            List<Recipe> list;
            int[] idList = JsonConvert.DeserializeObject<int[]>(ids);            

            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@UserFavorites", new[] { 17 } }
            };

            DynamicParameters parameters = new DynamicParameters(dict);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                list = connection.Query<Recipe>("dbo.SelectUserFavorites", parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return JsonConvert.SerializeObject(list);
        }

        public string GetRecipeByID(string id)
        {
            Recipe recipe;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                recipe = connection.Query<Recipe>("dbo.SelectRecipe @RecipeID", new { RecipeID = id }).First();
            }

            return JsonConvert.SerializeObject(recipe);
        }

        public string GetRandomRecipe()
        {
            Recipe recipe;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                recipe = connection.Query<Recipe>("dbo.SelectRandomRecipe").First();
            }

            return JsonConvert.SerializeObject(recipe);
        }

        public void AddNewRecipe(string recipeName, string recipeURL, string recipeIngredients, string recipeAllergens, string recipeProcedure, string recipeCalories)
        { 
            Dictionary<string, object> dict = new Dictionary<string, object>()
            {
                { "@RecipeName", recipeName },
                { "@RecipeProcedure", recipeProcedure },
                { "@RecipeIngredients", recipeIngredients },
                { "@RecipeImageURL", recipeURL},
                { "@RecipeCalories", Convert.ToInt32(recipeCalories) },
                { "@RecipeAllergens", recipeAllergens }

            };

            DynamicParameters parameters = new DynamicParameters(dict);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Query("dbo.AddRecipe", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
