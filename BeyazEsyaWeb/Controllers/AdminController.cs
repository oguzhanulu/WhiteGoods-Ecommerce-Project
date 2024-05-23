using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BeyazEsyaWeb.Controllers
{
	public class AdminController : Controller
	{
		private readonly IConfiguration _configuration;

		public AdminController(IConfiguration configuration)
		{
			_configuration = configuration;

		}
		public IActionResult Index()
		{
			string connectionString = _configuration.GetConnectionString("MyConnectionString");
			var UrunSayi = 0;
			var iletisayisi = 0;
			var AboneSayisi = 0;

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				UrunSayi = GetTabloCount(connection, "Products");
				iletisayisi = GetTabloCount(connection, "Notification");
				AboneSayisi = GetTabloCount(connection, "Subscribe");
			}
			var model = new AdminDatas
			{
				UrunSayi = UrunSayi,
				iletisayisi = iletisayisi,
				AboneSayisi = AboneSayisi
			};
			return View(model);

		}
		private int GetTabloCount(SqlConnection connection, string tableName)
		{
			var query = $"SELECT COUNT(*) FROM {tableName}";
			using (var command = new SqlCommand(query, connection))
			{
				return (int)command.ExecuteScalar();

			}
		}
	}
}
