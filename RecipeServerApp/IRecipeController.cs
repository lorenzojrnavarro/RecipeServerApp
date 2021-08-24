using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeServerApp
{
    interface IRecipeController
    {
        string GetRecipes(string recipeName, string allergenIds);
        string GetRandomRecipe();
        string GetUserFavoriteRecipes(string ids);
        string GetRecipeByID(string id);
        void AddNewRecipe(string recipeName, string recipeURL, string recipeIngredients, string recipeAllergens, string recipeProcedure, string recipeCalories);
    }
}
