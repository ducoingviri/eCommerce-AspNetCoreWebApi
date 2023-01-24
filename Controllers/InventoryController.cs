using System.Net;
using App02.Data;
using App02.DTO;
using App02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : Controller
{
    private readonly App02DbContext _dbContext;

    public InventoryController( App02DbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Inventory>>> Index()
    {
        var result = await _dbContext.Inventories.Select(
            item => new Inventory()
            {
                Id = item.Id,
                BranchId = item.BranchId,
                ProductId = item.ProductId,
                Branch = item.Branch,
                Product = item.Product
            }
        ).ToListAsync();

        return result.Count < 0 ? NotFound() : result;
    }

    [HttpGet("show")]
    public async Task<ActionResult<Inventory>> Show(int id)
    {
        var result = await _dbContext.Inventories.Select(
                item => new Inventory()
                {
                    Id = item.Id,
                    BranchId = item.BranchId,
                    ProductId = item.ProductId,
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        return result == null ?  NotFound() : result;
    }
    
    [HttpPost]
    public async Task<HttpStatusCode> Store(InventoryDTO input)
    {
        var item = new Inventory()
        {
            BranchId = input.BranchId,
            ProductId = input.ProductId,
        };

        _dbContext.Inventories.Add(item);
        
        var itemRel1 = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == input.ProductId);
        if (itemRel1 is { Stock: >= 0 }) itemRel1.Stock += 1;

        await _dbContext.SaveChangesAsync();
        
        return HttpStatusCode.Created;
    }
    
    [HttpPut]
    public async Task<HttpStatusCode> Update(Inventory input)
    {
        var item = await _dbContext.Inventories.FirstOrDefaultAsync(s => s.Id == input.Id);

        if (item != null)
        {
            item.BranchId = input.BranchId;
            item.ProductId = input.ProductId;
        }

        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("{id}")]
    public async Task<HttpStatusCode> Destroy(int id)
    {
        var item = new Inventory()
        {
            Id = id
        };
        _dbContext.Inventories.Attach(item);
        _dbContext.Inventories.Remove(item);
        await _dbContext.SaveChangesAsync();
        
        return HttpStatusCode.OK;
    }
}