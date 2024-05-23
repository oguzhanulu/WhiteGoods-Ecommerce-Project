using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BeyazEsyaWeb.Controllers
{
	public class FeedbackController : Controller
	{
		private readonly IConfiguration _configuration;

		public FeedbackController(IConfiguration configuration)
		{
			_configuration = configuration;

		}

		public IActionResult Index()
		{
			string veriyok = "Veri Bulunamadı";

			string connectionString = _configuration.GetConnectionString("MyConnectionString");
            List<Models.Notification> entities = new List<Models.Notification>();
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string sqlQuery = "SELECT * FROM Notification";
				using (var command = new SqlCommand(sqlQuery, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
                            // Verileri modele doldur
                            Models.Notification model = new Models.Notification
							{
								id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
								isim = reader["isim"] != DBNull.Value ? (string)reader["isim"] : veriyok,
								mail = reader["mail"] != DBNull.Value ? (string)reader["mail"] : veriyok,
								konu = reader["konu"] != DBNull.Value ? (string)reader["konu"] : veriyok,
								Mesaj = reader["mesaj"] != DBNull.Value ? (string)reader["mesaj"] : veriyok,


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

