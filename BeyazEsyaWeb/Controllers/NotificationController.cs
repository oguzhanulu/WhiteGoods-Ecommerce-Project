using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BeyazEsyaWeb.Controllers
{
	public class NotificationController : Controller
	{
		private readonly IConfiguration _configuration;

		public NotificationController(IConfiguration configuration)
		{
			_configuration = configuration;

		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddMessage([FromBody] Models.Notification Noti)
		{
			string connectionString = _configuration.GetConnectionString("MyConnectionString");

			{
				using (var connection = new SqlConnection(connectionString))
				{

					connection.Open();
					string sqlQuery = "INSERT INTO Notification(isim,Mail,Konu,Mesaj) VALUES (@isim,@mail,@konu,@mesaj) ";
					using (var command = new SqlCommand(sqlQuery, connection))
					{
						command.Parameters.AddWithValue("@isim", Noti.isim);
						command.Parameters.AddWithValue("@mail", Noti.mail);
						command.Parameters.AddWithValue("@konu", Noti.konu);
						command.Parameters.AddWithValue("@mesaj", Noti.Mesaj);
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
		[HttpPost]
		public IActionResult Subscribe([FromBody] Subscribe sub)
		{
			sub.Time= DateTime.Now;
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

