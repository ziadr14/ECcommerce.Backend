using ECom.BLL.DTOs;
using ECom.BLL.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<string?> LoginAsync(LoginDto dto);

        Task<bool> ActivateAccountAsync(ActivateAccountDto dto);

        Task ForgotPasswordAsync(ForgotPasswordDto dto);

        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);


    }
}
