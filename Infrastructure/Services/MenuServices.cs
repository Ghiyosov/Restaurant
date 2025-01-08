using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MenuServices(Context _context) : IMenuService
{
    public async Task<Responce<List<ReadMenuDTO>>> GetMenus()
    {
        var x = _context.Menus
            .Include(m=>m.Restaurant).AsQueryable();
        var menus = await x.Select(x => new ReadMenuDTO()
        {
            Id = x.Id,
            RestaurantId = x.Restaurant.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Category = x.Category,
            IsAvailable = x.IsAvailable,
            PreparationTime = x.PreparationTime,
            Weight = x.Weight,
            PhotoUrl = x.PhotoUrl,
            Restaurant = new UpdateRestaurantDTO()
            {
                Id = x.Restaurant.Id,
                Name = x.Restaurant.Name,
                Address = x.Restaurant.Address,
                Rating = x.Restaurant.Rating,
                WorkingHours = x.Restaurant.WorkingHours,
                Description = x.Restaurant.Description,
                ContactPhone = x.Restaurant.ContactPhone,
                IsActive = x.Restaurant.IsActive,
                MinOrderAmount = x.Restaurant.MinOrderAmount,
                DeliveryPrice = x.Restaurant.DeliveryPrice
            }
        }).ToListAsync();
        return new Responce<List<ReadMenuDTO>>(menus);
    }

    public async Task<Responce<ReadMenuDTO>> GetMenuById(int id)
    {
        var x = await _context.Menus
            .Include(m=>m.Restaurant).FirstOrDefaultAsync(m=>m.Id==id);
        if (x == null)
            return new Responce<ReadMenuDTO>(HttpStatusCode.NotFound, "Menu Not Found");
        var menu = new ReadMenuDTO()
        {
            Id = x.Id,
            RestaurantId = x.Restaurant.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Category = x.Category,
            IsAvailable = x.IsAvailable,
            PreparationTime = x.PreparationTime,
            Weight = x.Weight,
            PhotoUrl = x.PhotoUrl,
            Restaurant = new UpdateRestaurantDTO()
            {
                Id = x.Restaurant.Id,
                Name = x.Restaurant.Name,
                Address = x.Restaurant.Address,
                Rating = x.Restaurant.Rating,
                WorkingHours = x.Restaurant.WorkingHours,
                Description = x.Restaurant.Description,
                ContactPhone = x.Restaurant.ContactPhone,
                IsActive = x.Restaurant.IsActive,
                MinOrderAmount = x.Restaurant.MinOrderAmount,
                DeliveryPrice = x.Restaurant.DeliveryPrice
            }
        };
        return new Responce<ReadMenuDTO>(menu);
    }

    public async Task<Responce<string>> CreateMenu(CreateMenuDTO menu)
    {
        var m = new Menu()
        {
            RestaurantId = menu.RestaurantId,
            Name = menu.Name,
            Description = menu.Description,
            Price = menu.Price,
            Category = menu.Category,
            IsAvailable = menu.IsAvailable,
            PreparationTime = menu.PreparationTime,
            Weight = menu.Weight,
            PhotoUrl = menu.PhotoUrl
        };
        await _context.Menus.AddAsync(m);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Menu Created");
    }

    public async Task<Responce<string>> UpdateMenu(UpdateMenuDTO menu)
    {
        var x = await _context.Menus
            .Include(m=>m.Restaurant).FirstOrDefaultAsync(m=>m.Id==menu.Id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Menu Not Found");
        x.RestaurantId = menu.RestaurantId;
        x.Name = menu.Name;
        x.Description = menu.Description;
        x.Price = menu.Price;
        x.Category = menu.Category;
        x.IsAvailable = menu.IsAvailable;
        x.PreparationTime = menu.PreparationTime;
        x.Weight = menu.Weight;
        x.PhotoUrl = menu.PhotoUrl;
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Menu updated");
    }

    public async Task<Responce<string>> DeleteMenu(int id)
    {
        var x = await _context.Menus
            .Include(m=>m.Restaurant).FirstOrDefaultAsync(m=>m.Id==id);
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
        var menus = await x.Select(x => new ReadMenuDTO()
        {
            Id = x.Id,
            RestaurantId = x.Restaurant.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Category = x.Category,
            IsAvailable = x.IsAvailable,
            PreparationTime = x.PreparationTime,
            Weight = x.Weight,
            PhotoUrl = x.PhotoUrl,
            Restaurant = new UpdateRestaurantDTO()
            {
                Id = x.Restaurant.Id,
                Name = x.Restaurant.Name,
                Address = x.Restaurant.Address,
                Rating = x.Restaurant.Rating,
                WorkingHours = x.Restaurant.WorkingHours,
                Description = x.Restaurant.Description,
                ContactPhone = x.Restaurant.ContactPhone,
                IsActive = x.Restaurant.IsActive,
                MinOrderAmount = x.Restaurant.MinOrderAmount,
                DeliveryPrice = x.Restaurant.DeliveryPrice
            }
        }).ToListAsync();
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
                Menus = x.Select(x=> new UpdateMenuDTO()
                {
                    Id = x.Id,
                    RestaurantId = x.Restaurant.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Category = x.Category,
                    IsAvailable = x.IsAvailable,
                    PreparationTime = x.PreparationTime,
                    Weight = x.Weight,
                    PhotoUrl = x.PhotoUrl,
                }).ToList()
            }).OrderByDescending(x=>x.Menus.Count).FirstAsync();
        return new Responce<GetCategoryHasMoreMenus>(res);
    }
}