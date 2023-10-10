using MDWebCoreAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MDWebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OrderController(MyDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _dbContext.OrderMasters.Include(o => o.OrderDetail).ToList();

            return Ok(orders);
        }


        [HttpGet("{orderId}")]
        public IActionResult GetOrder(int orderId)
        {
            var order = _dbContext.OrderMasters.Include(o => o.OrderDetail).FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);

        }


        [HttpPost]
        public async Task<IActionResult> PostOrder()
        {

            if (ModelState.IsValid)
            {
                var customerName = HttpContext.Request.Form["CustomerName"];
                var OrderDate = DateTime.Parse(HttpContext.Request.Form["OrderDate"]);
                var IsComplete = bool.Parse(HttpContext.Request.Form["IsComplete"]);


                var order = new OrderMaster()
                {
                    CustomerName = customerName,
                    OrderDate = OrderDate,
                    IsComplete = IsComplete
                };

                IFormFile imageFile = HttpContext.Request.Form.Files["ImageFile"];
                if (imageFile != null)
                {
                    var filename = Guid.NewGuid().ToString() + (imageFile.FileName);
                    var path = Path.Combine(_hostEnvironment.WebRootPath ?? string.Empty, "uploads", filename);

                    order.ImagePath = path;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    string orderDetailJson = HttpContext.Request.Form["OrderDetail"];
                    if (!string.IsNullOrEmpty(orderDetailJson))
                    {

                        List<OrderDetail> orderDetailList = JsonConvert.DeserializeObject<List<OrderDetail>>(orderDetailJson);


                        order.OrderDetail.AddRange(orderDetailList);
                    }
                }
                _dbContext.OrderMasters.Add(order);
                await _dbContext.SaveChangesAsync();
            }
            return Ok();

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromForm] OrderMaster order)
        {
            order = _dbContext.OrderMasters.Include(o => o.OrderDetail).FirstOrDefault(o => o.OrderId == id);

            if (ModelState.IsValid)
            {
                var cust = HttpContext.Request.Form["CustomerName"];
                var date = HttpContext.Request.Form["OrderDate"];
                var status = HttpContext.Request.Form["IsComplete"];
                IFormFile imageFile = HttpContext.Request.Form.Files["ImageFile"];
                var orderDetailJson = HttpContext.Request.Form["OrderDetail"];

                if (!string.IsNullOrEmpty(cust) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(status) && imageFile != null && !string.IsNullOrEmpty(orderDetailJson))
                {
                    order.CustomerName = cust;
                    order.OrderDate = DateTime.Parse(date);
                    order.IsComplete = bool.Parse(status);

                    var filename = Guid.NewGuid().ToString() + (imageFile.FileName);
                    var path = Path.Combine(_hostEnvironment.WebRootPath ?? string.Empty, "uploads", filename);

                    order.ImagePath = path;
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    var updatedOrderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(orderDetailJson);
                    foreach (var updatedOrderDetail in updatedOrderDetails)
                    {
                        var existingOrderDetail = order.OrderDetail.FirstOrDefault(od => od.DetailId == updatedOrderDetail.DetailId);
                        if (existingOrderDetail != null)
                        {
                            existingOrderDetail.ProductId = updatedOrderDetail.ProductId;
                            existingOrderDetail.Quantity = updatedOrderDetail.Quantity;
                            existingOrderDetail.Price = updatedOrderDetail.Price;
                        }
                        else
                        {
                            var newOrderDetail = new OrderDetail
                            {
                                ProductId = updatedOrderDetail.ProductId,
                                Quantity = updatedOrderDetail.Quantity,
                                Price = updatedOrderDetail.Price,
                            };

                            order.OrderDetail.Add(newOrderDetail);
                        }

                    }
                }
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }



        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var order = _dbContext.OrderMasters.FirstOrDefault(o => o.OrderId == orderId);
            _dbContext.OrderMasters.Remove(order);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

}
