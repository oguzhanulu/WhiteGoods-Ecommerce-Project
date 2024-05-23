using BeyazEsyaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BeyazEsyaWeb.Controllers
{
	public class AddProductController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public AddProductController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			_configuration = configuration;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile photo)
		{
			if (photo != null && photo.Length > 0)
			{

				var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);
				TempData["foto"] = photo.FileName;
				// Dosyayı kaydedin
				using (var fileStream = new FileStream(imagePath, FileMode.Create))
				{
					await photo.CopyToAsync(fileStream);
				}

				return Ok("Dosya başarıyla yüklendi.");
			}

			return BadRequest("Geçersiz dosya.");
		}

		[HttpPost]
		public IActionResult AddProduct([FromBody] Products urun)
		{
			urun.isActive = true;
			urun.Date= DateTime.Now;
			urun.Photo= TempData["foto"] as string;

			string connectionString = _configuration.GetConnectionString("MyConnectionString");

			{
				using (var connection = new SqlConnection(connectionString))
				{

					connection.Open();
					string sqlQuery = "INSERT INTO Products(Title,Details,Price,Color,isActive,Date,Categoryid,Photo,ProductDetails) VALUES (@Title,@Details,@Price,@Color,@isActive,@Date,@Categoryid,@Photo,@ProductDetails) ";
					using (var command = new SqlCommand(sqlQuery, connection))
					{
						command.Parameters.AddWithValue("@Title", urun.Title);
						command.Parameters.AddWithValue("@Details", urun.Details);
						command.Parameters.AddWithValue("@Price", urun.Price);
						command.Parameters.AddWithValue("@Color", urun.Color);
						command.Parameters.AddWithValue("@isActive", urun.isActive);
						command.Parameters.AddWithValue("@Date", urun.Date);
						command.Parameters.AddWithValue("@Categoryid", urun.Categoryid);
						command.Parameters.AddWithValue("@Photo", urun.Photo);
						command.Parameters.AddWithValue("@ProductDetails", urun.ProductDetails);
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
