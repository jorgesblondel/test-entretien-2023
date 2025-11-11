using CiteoInterview.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CiteoInterview.Api.Controllers;

[ApiController]
[Route("api")]
public class ProductsController : ControllerBase
{
    private readonly ShopDbContext _context;
    public ProductsController(ShopDbContext context)
    {
        _context = context;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProduct(CancellationToken cancellationToken)
    {
        var products = await _context.Product.ToListAsync(cancellationToken).ConfigureAwait(false);

        return Ok(products);
    }

    [HttpPost("saved")]
    public async Task<IActionResult> CreateProduct([FromBody]Product product, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(product.Name))
            return BadRequest("Name can not be empty");

        await _context.Product.AddAsync(product).ConfigureAwait(false);
        _context.SaveChanges();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute]int id)
    {
       var product =  await _context.Product.FindAsync(id).ConfigureAwait(false); 

        return product != null ? Ok(product) : NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductPriceByClientId([FromRoute] int id)
    {
        var montant = await _context.Order.Where(x => x.CustomerId == id)
                                          .SelectMany(x => x.OrderItem)
                                          .SumAsync(x => x.Product.Price * x.Quantity);

        return Ok(montant);

    }
}
