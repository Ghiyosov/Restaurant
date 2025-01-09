using Domain.DTOs;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class CourierController(ICourierService _service)
{
    [HttpGet("GetCouriers")]
    public async Task<Responce<List<ReadCourierDTO>>> GetCouriers()
        => await _service.GetCouriers();
    
    [HttpGet("GetCourierById/{id}")]
    public async Task<Responce<ReadCourierDTO>> GetCourierById(int id)
        => await _service.GetCourierById(id);
    
    [HttpPost("CreateCourier")]
    public async Task<Responce<string>> CreateCourier(CreateCourierDTO createCourierDTO)
        => await _service.CreateCourier(createCourierDTO);
    
    [HttpPut("UpdateCourier")]
    public async Task<Responce<string>> UpdateCourier(UpdateCourierDTO updateCourierDTO)
        => await _service.UpdateCourier(updateCourierDTO);
    
    [HttpDelete("DeleteCourier/{id}")]
    public async Task<Responce<string>> DeleteCourier(int id)
        => await _service.DeleteCourier(id);
    
    [HttpGet("Task8GetTop5Couriers")]
    public async Task<Responce<List<ReadCourierDTO>>> Task8GetTop5Couriers()
        => await _service.Task8GetTop5Couriers();

}