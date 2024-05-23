namespace BeyazEsyaWeb.Models
{
    public class Products
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
        public bool isActive { get; set; }
        public DateTime Date { get; set; }
        public int Categoryid { get; set; }
        public string Photo { get; set; }
        public string ProductDetails { get; set; }
        
    }
}
