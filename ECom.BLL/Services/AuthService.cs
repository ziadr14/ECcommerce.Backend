using ECom.BLL.DTOs;
using ECom.BLL.DTOs.Pagination;
using ECom.BLL.Interfaces;
using ECom.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            if (dto == null)
                return false;

            if (await _userManager.FindByNameAsync(dto.UserName) != null)
                return false;

            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return false;

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                DisoplayName = dto.DisplayName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return false;

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await SendEmail(
                email: user.Email,
                code: token,
                component: "active",
                subject: "Activate Your Email",
                message: "Please activate your email to complete registration"
            );

            return true;
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            if (dto == null)
                return null;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return null;

            if (!user.EmailConfirmed)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                dto.Password,
                false
            );

            if (!result.Succeeded)
                return null;

            return GenerateJwtToken(user);
        }

        public async Task<bool> ActivateAccountAsync(ActivateAccountDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Token))
                return false;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return false;

            if (user.EmailConfirmed)
                return true;

            var decodedToken = Uri.UnescapeDataString(dto.Token);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            return result.Succeeded;
        }


        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            if (dto == null)
                return;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Uri.EscapeDataString(token);

            await SendEmail(
                email: user.Email,
                code: encodedToken,
                component: "reset-password",
                subject: "Reset Your Password",
                message: "Click the button below to reset your password"
            );
        }




        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            if (dto == null)
                return false;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return false;

            var decodedToken = Uri.UnescapeDataString(dto.Token);

            var result = await _userManager.ResetPasswordAsync(
                user,
                decodedToken,
                dto.NewPassword
            );

            return result.Succeeded;
        }


        private string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Key"])
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(
                    int.Parse(_configuration["JWT:DurationInDays"])
                ),
                signingCredentials: new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task SendEmail(
            string email,
            string code,
            string component,
            string subject,
            string message)
        {
            var emailDTO = new EmailDto
            {
                To = email,
                From = _configuration["EmailSetting:From"],
                Subject = subject,
                Content = EmailStringBody.Build(
                    email,
                    code,
                    component,
                    subject,
                    message,
                    "Confirm Email"
                )
            };

            await _emailService.SendEmailAsync(emailDTO);
        }
    }
}
