using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BeyazEsyaWeb.Controllers
{
	public class loginController : Controller
	{
		private readonly IConfiguration _configuration;

		public loginController(IConfiguration configuration)
		{
			_configuration = configuration;

		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> LoginControl([FromBody] Users user)
		{
			string connectionString = _configuration.GetConnectionString("MyConnectionString");

			{
				using (var connection = new SqlConnection(connectionString))
				{

					connection.Open();
					string sqlQuery = "SELECT COUNT (*) FROM Users WHERE Email = @email AND Password = @password;";
					using (var command = new SqlCommand(sqlQuery, connection))
					{
						command.Parameters.AddWithValue("@email", user.email);
						command.Parameters.AddWithValue("@password", user.password);
						
						var result =(int)await command.ExecuteScalarAsync();
						if (result > 0)
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
		[HttpPost]
		public IActionResult Subscribe([FromBody] Subscribe sub)
		{
			sub.Time = DateTime.Now;
			string connectionString = _configuration.GetConnectionString("MyConnectionString");

			{
				using (var connection = new SqlConnection(connectionString))
				{

					connection.Open();
					string sqlQuery = "INSERT INTO Subscribe(EMail,Time) VALUES (@Email,@Time) ";
					using (var command = new SqlCommand(sqlQuery, connection))
					{

						command.Parameters.AddWithValue("@Email", sub.Email);
						command.Parameters.AddWithValue("@Time", sub.Time);
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
