using Domain.DTOs;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface ICourierService
{
    public Task<Responce<List<ReadCourierDTO>>> GetCouriers();
    public Task<Responce<ReadCourierDTO>> GetCourierById(int id);
    public Task<Responce<string>> CreateCourier(CreateCourierDTO createCourierDTO);
    public Task<Responce<string>> UpdateCourier(UpdateCourierDTO updateCourierDTO);
    public Task<Responce<string>> DeleteCourier(int id);
    public Task<Responce<List<ReadCourierDTO>>> Task7GetTop5Couriers();
}