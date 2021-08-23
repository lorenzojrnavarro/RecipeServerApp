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

        public string GetRecipes(string recipeName)
        {
            List<Recipe> list;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                list = connection.Query<Recipe>("dbo.SelectRecipeByFilter @RecipeName", new { RecipeName = recipeName }).ToList();
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
