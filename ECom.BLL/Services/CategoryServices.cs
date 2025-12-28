using AutoMapper;
using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using ECom.DAL.Entities;
using ECom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServices(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }




        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync();

                return _mapper.Map<IEnumerable<CategoryDto>>(categories);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoryDto> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetById(id);

                if (category == null)
                    return null;

                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task CreateCategory(CategoryDto categoryDto)
        {
            try
            {
                var newCategory = _mapper.Map<Category>(categoryDto);

                await _unitOfWork.Categories.AddAsync(newCategory);

                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }





        public async Task Update(CategoryDto categoryDto)
        {
            try
            {
                var existingCategory = await _unitOfWork.Categories.GetById(categoryDto.Id);
                if(existingCategory == null)
                    throw new Exception("Category not found");

                _mapper.Map<Category>(existingCategory);

                await _unitOfWork.Categories.UpdateAsync(existingCategory);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task DeleteById(int id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetById(id);
                if (category == null)
                    throw new KeyNotFoundException($"No category found with ID {id}");

                _unitOfWork.Categories.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
