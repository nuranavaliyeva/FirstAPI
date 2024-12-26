﻿using FirstAPI.DTOs.Category;
using FirstAPI.DTOs.Color;
using FirstAPI.DTOs.Product;
using FirstAPI.Repositories.Implementations;
using FirstAPI.Repositories.Interfaces;
using FirstAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Services.Implementations
{
    public class ColorService:IColorService
    {
        private readonly IColorRepository _colorRepository;
        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }
        public async Task<bool> CreateAsync(CreateColorDTO colorDTO)
        {
            if (await _colorRepository.AnyAsync(c => c.Name == colorDTO.Name))
            {
                return false;
            }

            await _colorRepository.AddAsync(new Color { Name = colorDTO.Name });
            await _colorRepository.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<GetColorDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<GetColorDTO> colors = await _colorRepository
            .GetAll(skip: (page - 1) * take, take: take)
            .Select
            (
                c => new GetColorDTO
                {
                    Id = c.Id,
                    Name = c.Name

                }
            ).ToListAsync();
            return colors;
        }
        public async Task<GetColorDetailDTO> GetByIdAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);

            if (color is null) return null;

            GetColorDetailDTO colorDetailsDTO = new()
            {
                Id = color.Id,
                Name = color.Name,

            };

            return colorDetailsDTO;
        }
        public async Task UpdateAsync(int id, UpdateColorDTO colorDTO)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color is null) throw new Exception("Not Found");

            if (await _colorRepository
                .AnyAsync(c => c.Name == colorDTO.Name && c.Id == id))
            {
                throw new Exception("Alredy Exists");
            }

            color.Name = colorDTO.Name;
            _colorRepository.Update(color);
            await _colorRepository.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            Color color = await _colorRepository.GetByIdAsync(id);
            if (color is null) throw new Exception("Not Found");

            _colorRepository.Delete(color);
            await _colorRepository.SaveChangesAsync();
        }
    }

}



