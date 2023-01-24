using System.Net;
using App02.Data;
using App02.DTO;
using App02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController : Controller
{
    private readonly App02DbContext _dbContext;

    public SaleController(App02DbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Sale>>> Index()
    {
        var result = await _dbContext.Sales.Select(
            item => new Sale()
            {
                Id = item.Id,
                AspNetUserId = item.AspNetUserId,
                ProductId = item.ProductId,
                PurchasedAt = item.PurchasedAt,
                Product = item.Product
            }
        ).ToListAsync();

        return result.Count < 0 ? NotFound() : result;
    }

    [HttpGet("show")]
    public async Task<ActionResult<Sale>> Show(int id)
    {
        var result = await _dbContext.Sales.Select(
                item => new Sale()
                {
                    Id = item.Id,
                    AspNetUserId = item.AspNetUserId,
                    ProductId = item.ProductId,
                    PurchasedAt = item.PurchasedAt,
                    Product = item.Product
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        return result == null ?  NotFound() : result;
    }
    
    [HttpPost]
    public async Task<HttpStatusCode> Store(SaleStoreDTO input)
    {
        var item = new Sale()
        {
            AspNetUserId = input.AspNetUserId,
            ProductId = input.ProductId,
            PurchasedAt =  DateTime.Now,
        };

        _dbContext.Sales.Add(item);
        
        var itemRel1 = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == input.ProductId);
        if (itemRel1 is { Stock: > 0 }) itemRel1.Stock -= 1; else return HttpStatusCode.BadRequest;
        
        var itemRel2 = await _dbContext.Inventories.FirstOrDefaultAsync(s => s.ProductId == input.ProductId);
        if (itemRel2 != null)
        {
            var itemRel2Temp = new Inventory()
            {
                Id = itemRel2.Id
            };
            _dbContext.Inventories.Attach(itemRel2Temp);
            _dbContext.Inventories.Remove(itemRel2Temp);
        }

        await _dbContext.SaveChangesAsync();
        
        return HttpStatusCode.Created;
    }
    
    [HttpPut]
    public async Task<HttpStatusCode> Update(Sale input)
    {
        var item = await _dbContext.Sales.FirstOrDefaultAsync(s => s.Id == input.Id);

        if (item != null)
        {
            item.AspNetUserId = input.AspNetUserId;
            item.ProductId = input.ProductId;
            item.PurchasedAt = input.PurchasedAt;
        }

        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("{id}")]
    public async Task<HttpStatusCode> Destroy(int id)
    {
        var item = new Sale()
        {
            Id = id
        };
        _dbContext.Sales.Attach(item);
        _dbContext.Sales.Remove(item);
        await _dbContext.SaveChangesAsync();
        
        return HttpStatusCode.OK;
    }
}