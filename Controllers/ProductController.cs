using System.Net;
using App02.Data;
using App02.DTO;
using App02.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly App02DbContext _dbContext;

    public ProductController( App02DbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Product>>> Index()
    {
        var result = await _dbContext.Products.Select(
            item => new Product()
            {
                Id = item.Id,
                Code = item.Code,
                Description = item.Description,
                Price = item.Price,
                Photo = item.Photo,
                Stock = item.Stock
            }
        ).ToListAsync();

        return result.Count < 0 ? NotFound() : result;
    }

    [HttpGet("show")]
    public async Task<ActionResult<Product>> Show(int id)
    {
        var result = await _dbContext.Products.Select(
                item => new Product()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Description = item.Description,
                    Price = item.Price,
                    Photo = item.Photo,
                    Stock = item.Stock
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        return result == null ?  NotFound() : result;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<HttpStatusCode> Store(ProductDTO input)
    {
        var item = new Product()
        {
            Code = input.Code,
            Description = input.Description,
            Price = input.Price,
            Photo = input.Photo,
            Stock = input.Stock
        };

        _dbContext.Products.Add(item);
        await _dbContext.SaveChangesAsync();

        return HttpStatusCode.Created;
    }
    
    [HttpPut ("update")]
    public async Task<HttpStatusCode> Update(Product input)
    {
        var item = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == input.Id);

        if (item != null)
        {
            item.Code = input.Code;
            item.Description = input.Description;
            item.Price = input.Price;
            item.Photo = input.Photo;
            item.Stock = input.Stock;
        }

        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("destroy/{id}")]
    public async Task<HttpStatusCode> Destroy(int id)
    {
        var item = new Product()
        {
            Id = id
        };
        _dbContext.Products.Attach(item);
        _dbContext.Products.Remove(item);
        await _dbContext.SaveChangesAsync();
        
        return HttpStatusCode.OK;
    }
    
    [HttpGet("find-by-description")]
    public async Task<ActionResult<List<Product>>> FindByDescription(string value)
    {
        var result = await _dbContext.Products.Select(
            item => new Product()
            {
                Id = item.Id,
                Code = item.Code,
                Description = item.Description,
                Price = item.Price,
                Photo = item.Photo,
                Stock = item.Stock
            }
        )
            .Where(a => a.Description.Contains(value))
            .ToListAsync();

        return result.Count < 0 ? NotFound() : result;
    }
}