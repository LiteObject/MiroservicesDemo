namespace MiroservicesDemo.Order.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using MiroservicesDemo.Order.Data;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly OrderDbContext context;


        public OrdersController(ILogger<OrdersController> logger, OrderDbContext context)
        {
            this.logger = logger;
            this.context = context;
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
        public async Task<IActionResult> Get(int id)
        {
            var order = await context.Orders.FindAsync(id);

            if (order is null)
            {
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

            await context.Orders.AddAsync(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }
    }
}
