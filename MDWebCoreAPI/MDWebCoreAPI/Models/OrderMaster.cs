using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MDWebCoreAPI.Models
{
    public class OrderMaster
    {
        [Key]
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool? IsComplete { get; set; }

        public virtual List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
    }
}
