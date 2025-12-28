using ECom.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface ICategoryServices
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto> GetById(int id);

        Task CreateCategory(CategoryDto categoryDto);  

        Task Update(CategoryDto categoryDto);

        Task DeleteById(int id);
    }
}
