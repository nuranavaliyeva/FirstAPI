using FirstAPI.DTOs.Category;
using FirstAPI.DTOs.Color;

namespace FirstAPI.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<GetColorDTO>> GetAllAsync(int page, int take);
        Task<GetColorDetailDTO> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateColorDTO categoryDTO);
        Task UpdateAsync(int id, UpdateColorDTO categoryDTO);
        Task DeleteAsync(int id);
    }
}
