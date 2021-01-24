namespace MiroservicesDemo.Order.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using MiroservicesDemo.Order.Data;
    using MiroservicesDemo.Order.Shared;
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly OrderDbContext context;
        private readonly IEmailService emailService;

        public OrdersController(ILogger<OrdersController> logger, OrderDbContext context, IEmailService emailService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await context.Orders.ToListAsync();

            if (orders is null || !orders.Any())
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await context.Orders.FindAsync(id);

            if (order is null)
            {
                await this.emailService.SendEmailAsync("system@email.com", "devs@email.com", "Order deatils requested with invalid id", $"Order deatils requested with invalid id: {id}.");
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Domain.Entities.Order order) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            try
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

                var emailBody = JsonSerializer.Serialize(order);
                await this.emailService.SendEmailAsync("system@email.com", "devs@email.com", "A new order has been creation", emailBody);
            }
            catch (Exception ex)
            {
                await this.emailService.SendEmailAsync("system@email.com", "devs@email.com", "Create Order Exception", ex.ToString());
            }
            
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }
    }
}
