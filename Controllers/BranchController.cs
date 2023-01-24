using System.Net;
using App02.Data;
using App02.DTO;
using App02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchController : Controller
{
        private readonly App02DbContext _dbContext;

    public BranchController( App02DbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Branch>>> Index()
    {
        var result = await _dbContext.Branches.Select(
            item => new Branch()
            {
                Id = item.Id,
                Name = item.Name,
                Address = item.Address
            }
        ).ToListAsync();

        return result.Count < 0 ? NotFound() : result;
    }

    [HttpGet("show")]
    public async Task<ActionResult<Branch>> Show(int id)
    {
        var result = await _dbContext.Branches.Select(
                item => new Branch()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        return result == null ?  NotFound() : result;
    }
    
    [HttpPost]
    public async Task<HttpStatusCode> Store(BranchDTO input)
    {
        var item = new Branch()
        {
            Name = input.Name,
            Address = input.Address,
        };

        _dbContext.Branches.Add(item);
        await _dbContext.SaveChangesAsync();

        return HttpStatusCode.Created;
    }
    
    [HttpPut ("update")]
    public async Task<HttpStatusCode> Update(Branch input)
    {
        var item = await _dbContext.Branches.FirstOrDefaultAsync(s => s.Id == input.Id);

        if (item != null)
        {
            item.Name = input.Name;
            item.Address = input.Address;
        }

        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("destroy/{id}")]
    public async Task<HttpStatusCode> Destroy(int id)
    {
        var item = new Branch()
        {
            Id = id
        };
        _dbContext.Branches.Attach(item);
        _dbContext.Branches.Remove(item);
        await _dbContext.SaveChangesAsync();
        
        return HttpStatusCode.OK;
    }
}