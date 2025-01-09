using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MenuServices(Context _context, IMapper _mapper) : IMenuService
{
    public async Task<Responce<List<ReadMenuDTO>>> GetMenus()
    {
        var x = await _context.Menus
            .Include(m=>m.Restaurant)
            .Include(x=>x.OrderDetails).ToListAsync();
        var menus =  _mapper.Map<List<ReadMenuDTO>>(x);
        return new Responce<List<ReadMenuDTO>>(menus);
    }

    public async Task<Responce<ReadMenuDTO>> GetMenuById(int id)
    {
        var x = await _context.Menus
            .Include(m=>m.Restaurant)
            .Include(x=>x.OrderDetails).FirstOrDefaultAsync(m=>m.Id==id);
        if (x == null)
            return new Responce<ReadMenuDTO>(HttpStatusCode.NotFound, "Menu Not Found");
        var menu = _mapper.Map<ReadMenuDTO>(x);
        return new Responce<ReadMenuDTO>(menu);
    }

    public async Task<Responce<string>> CreateMenu(CreateMenuDTO menu)
    {
        var m = _mapper.Map<Menu>(menu);
        await _context.Menus.AddAsync(m);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Menu Created");
    }

    public async Task<Responce<string>> UpdateMenu(UpdateMenuDTO menu)
    {
        var x = await _context.Menus
            .Include(m=>m.Restaurant)
            .Include(x=>x.OrderDetails).FirstOrDefaultAsync(m=>m.Id==menu.Id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Menu Not Found");
        _mapper.Map(menu, x);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Menu updated");
    }

    public async Task<Responce<string>> DeleteMenu(int id)
    {
        var x = await _context.Menus
            .Include(m=>m.Restaurant)
            .Include(x=>x.OrderDetails).FirstOrDefaultAsync(m=>m.Id==id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Menu Not Found");
        _context.Menus.Remove(x);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Menu deleted");
    }

    public async Task<Responce<List<ReadMenuDTO>>> Task2GetMenusСheaper1000()
    {
        var x = _context.Menus
            .Include(m=>m.Restaurant)
            .Where(m=>m.Price<1000).AsQueryable();
        var menus = _mapper.Map<List<ReadMenuDTO>>(x);
        return new Responce<List<ReadMenuDTO>>(menus);
    }

    public async Task<Responce<List<GetMenusCategoryWithAveregPrice>>> Task4GetMenusCategoryWithAveregPrice()
    {
        var x =  _context.Menus.AsQueryable();
        var res = await x.GroupBy(x => x.Category)
            .Select(x => new GetMenusCategoryWithAveregPrice()
            {
                Category = x.Key,
                AvgPrice = x.Average(a => a.Price)
            }).ToListAsync();
        return new Responce<List<GetMenusCategoryWithAveregPrice>>(res);
    }

    public async Task<Responce<GetCategoryHasMoreMenus>> Task10GetCategoryHasMoreMenus()
    {
        var x =  _context.Menus.AsQueryable();
        var res = await x.GroupBy(x => x.Category)
            .Select(x=> new GetCategoryHasMoreMenus()
            {
                Category = x.Key,
                Menus = _mapper.Map<List<UpdateMenuDTO>>(x.ToList())
            }).OrderByDescending(x=>x.Menus.Count).FirstAsync();
        return new Responce<GetCategoryHasMoreMenus>(res);
    }
}