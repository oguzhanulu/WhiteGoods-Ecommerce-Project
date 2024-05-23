using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BeyazEsyaWeb.Controllers
{
	public class AddUserController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public AddUserController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			_configuration = configuration;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			string veriyok = "Veri Bulunamadı";

			string connectionString = _configuration.GetConnectionString("MyConnectionString");
			List<Users> entities = new List<Users>();
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				string sqlQuery = "SELECT * FROM Users ";
				using (var command = new SqlCommand(sqlQuery, connection))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							// Verileri modele doldur
							Users model = new Users
							{
								name = reader["Name"] != DBNull.Value ? (string)reader["Name"] : veriyok,
								password = reader["Password"] != DBNull.Value ? (string)reader["Password"] : veriyok,
								email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : veriyok,

							};


							entities.Add(model);
						}
					}
				}
			}
			return View(entities);


		}

		public IActionResult AddUser([FromBody] Users users)
		{
			string connectionString = _configuration.GetConnectionString("MyConnectionString");

			{
				using (var connection = new SqlConnection(connectionString))
				{

					connection.Open();
					string sqlQuery = "INSERT INTO Users(Name,Password,Email) VALUES (@Name,@Password,@Email) ";
					using (var command = new SqlCommand(sqlQuery, connection))
					{
						command.Parameters.AddWithValue("@Name", users.name);
						command.Parameters.AddWithValue("@Password", users.password);
						command.Parameters.AddWithValue("@Email", users.email);
						
						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							return Ok();

						}
						else
						{
							return BadRequest();
						}

					}

				}
			}

			
		}
	}
}





