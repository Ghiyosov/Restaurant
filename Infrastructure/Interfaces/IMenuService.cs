using Domain.DTOs;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IMenuService
{
    public Task<Responce<List<ReadMenuDTO>>> GetMenus();
    public Task<Responce<ReadMenuDTO>> GetMenuById(int id);
    public Task<Responce<string>> CreateMenu(CreateMenuDTO menu);
    public Task<Responce<string>> UpdateMenu(UpdateMenuDTO menu);
    public Task<Responce<string>> DeleteMenu(int id);
    public Task<Responce<List<ReadMenuDTO>>> Task2GetMenusСheaper1000();
    public Task<Responce<List<GetMenusCategoryWithAveregPrice>>> Task4GetMenusCategoryWithAveregPrice();
    public Task<Responce<GetCategoryHasMoreMenus>> Task10GetCategoryHasMoreMenus();
}