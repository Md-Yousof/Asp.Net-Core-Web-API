using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MDWebCoreAPI.Models
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public OrderMaster OrderMaster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
