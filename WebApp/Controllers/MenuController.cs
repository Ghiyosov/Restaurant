using Domain.DTOs;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class MenuController(IMenuService _service)
{
    [HttpGet("GetMenus")]
    public async Task<Responce<List<ReadMenuDTO>>> GetMenus()
        => await _service.GetMenus();
    
    [HttpGet("GetMenus/{id}")]
    public async Task<Responce<ReadMenuDTO>> GetMenuById(int id)
        => await _service.GetMenuById(id);
    
    [HttpPost("CreateMenu")]
    public async Task<Responce<string>> CreateMenu(CreateMenuDTO menu)
        => await _service.CreateMenu(menu);
    
    [HttpPut("UpdateMenu")]
    public async Task<Responce<string>> UpdateMenu(UpdateMenuDTO menu) 
        => await _service.UpdateMenu(menu);
    
    [HttpDelete("DeleteMenu/{id}")]
    public async Task<Responce<string>> DeleteMenu(int id) 
        => await _service.DeleteMenu(id);

    [HttpGet("Task2GetMenusСheaper1000")]
    public async Task<Responce<List<ReadMenuDTO>>> Task2GetMenusСheaper1000()
        => await _service.Task2GetMenusСheaper1000();
    
    [HttpGet("Task4GetMenusCategoryWithAveregPrice")]
    public async Task<Responce<List<GetMenusCategoryWithAveregPrice>>> Task4GetMenusCategoryWithAveregPrice()
        => await _service.Task4GetMenusCategoryWithAveregPrice();
    
    [HttpGet("Task10GetCategoryHasMoreMenus")]
    public async Task<Responce<GetCategoryHasMoreMenus>> Task10GetCategoryHasMoreMenus()
        => await _service.Task10GetCategoryHasMoreMenus();
}