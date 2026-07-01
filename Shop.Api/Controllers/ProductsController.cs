using Shop.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.DTOs;
using Shop.Api.Models;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpPost]
public async Task<IActionResult> Create(ProductCreateDto dto)
{
    var product = new Product
    {
        Name = dto.Name,
        Price = dto.Price,
        Quantity = dto.Quantity
    };

    await _repository.AddAsync(product);

    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
}

[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var product = await _repository.GetByIdAsync(id);

    if (product == null)
        return NotFound();

    return Ok(product);
}


[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
{
    var product = await _repository.GetByIdAsync(id);

    if (product == null)
        return NotFound();

    product.Name = dto.Name;
    product.Price = dto.Price;
    product.Quantity = dto.Quantity;

    await _repository.UpdateAsync(product);

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var product = await _repository.GetByIdAsync(id);

    if (product == null)
        return NotFound();

    await _repository.DeleteAsync(product);

    return NoContent();
}

}