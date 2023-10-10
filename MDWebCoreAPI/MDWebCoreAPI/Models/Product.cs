using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MDWebCoreAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderDetail> Details { get; set; }
    }
}
