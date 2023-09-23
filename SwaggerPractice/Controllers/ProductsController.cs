using Microsoft.AspNetCore.Mvc;
using SwaggerPractice.Models;

namespace SwaggerPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    /// <summary>
    /// Get All Products
    /// </summary>
    /// <returns>Get All Products</returns>
    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return Products;
    }

    /// <summary>
    /// Get a specific product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns an ActionResult of type Product</returns>
    /// <response code="200">Returns the requested Product</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public Task<OkObjectResult> Get(Guid id)
    {
        return Task.FromResult(Ok(Products.FirstOrDefault(x => x.Id.Equals(id))));
    }

    private static IEnumerable<Product> Products => new List<Product>()
    {
        new Product()
        {
            ProductName = "Apple",
            Price = 200
        },
        new Product()
        {
            ProductName = "Orange",
            Price = 250
        },
        new Product()
        {
            ProductName = "Pineapple",
            Price = 100
        },
        new Product()
        {
            ProductName = "Dragon Fruit",
            Price = 200
        },
        new Product()
        {
            ProductName = "Banana",
            Price = 120
        },
        new Product()
        {
            ProductName = "Mango",
            Price = 350
        },
        new Product()
        {
            ProductName = "Lichen",
            Price = 150
        }
    };
}