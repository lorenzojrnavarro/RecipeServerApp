using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;

namespace ServerApp
{
    class DatabaseBookController : IBookController
    {
        //private readonly string connectionString = @"Data Source=192.168.4.42;Initial Catalog=MORGOTHS_COOKBOOK.MDF;User ID=RecipeAppServer;Password=moe380";
        private readonly string connectionString = @"Data Source=76.175.108.117;Initial Catalog=MORGOTHS_COOKBOOK.MDF;User ID=RecipeAppServer;Password=moe380";

        public DatabaseBookController()
        {

        }

        public BookCollection SearchBook(string searchParam)
        {
            throw new NotImplementedException();
        }
    }
}
