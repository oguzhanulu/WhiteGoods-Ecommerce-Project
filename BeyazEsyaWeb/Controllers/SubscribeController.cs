using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BeyazEsyaWeb.Controllers
{
	public class SubscribeController : Controller
	{
		private readonly IConfiguration _configuration;

		public SubscribeController(IConfiguration configuration)
		{
			_configuration = configuration;

		}

		public IActionResult Index()
		{
			string veriyok = "Veri Bulunamadı";

			string connectionString = _configuration.GetConnectionString("MyConnectionString");
			List<Subscribe> entities = new List<Subscribe>();
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string sqlQuery = "SELECT * FROM Subscribe";
				using (var command = new SqlCommand(sqlQuery, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							// Verileri modele doldur
							Subscribe model = new Subscribe
							{
								id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
								Email = reader["email"] != DBNull.Value ? (string)reader["email"] : veriyok,
								Time = reader["Time"] != DBNull.Value ? (DateTime)reader["Time"] : DateTime.MinValue,



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
