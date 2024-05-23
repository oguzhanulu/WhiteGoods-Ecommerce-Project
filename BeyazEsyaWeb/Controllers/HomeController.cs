using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace BeyazEsyaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public IActionResult Index()
        {
            string veriyok = "Veri Bulunamadı";
            
            string resimyok = "veriyok.jpg";
            
            string connectionString = _configuration.GetConnectionString("MyConnectionString");
            List<Products> entities = new List<Products>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT TOP 3 * FROM Products  where isActive='true' ORDER BY NEWID() ";
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Verileri modele doldur
                            Products model = new Products
                            {
                                Details = reader["Details"] != DBNull.Value ? (string)reader["Details"] : veriyok,
                                Title = reader["Title"] != DBNull.Value ? (string)reader["Title"] : veriyok,
                                Color = reader["Color"] != DBNull.Value ? (string)reader["Color"] : veriyok,
                                id = reader["Id"] != DBNull.Value ? (int)reader["Id"] : 0,
                                Categoryid = reader["Categoryid"] != DBNull.Value ? (int)reader["Categoryid"] : 0,
                                Date = reader["Date"] != DBNull.Value ? (DateTime)reader["Date"]: DateTime.MinValue,
                                Price = reader["Price"] != DBNull.Value ? (int)reader["Price"] : 00000,
                                Photo = reader["Photo"] != DBNull.Value ? (string)reader["Photo"] : resimyok,
                                isActive= reader["isActive"] != DBNull.Value ? (bool)reader["isActive"] : false,


							};
                            

                            entities.Add(model);
                        }
                    }
                }
            }
            return View(entities);







        }
    }


}