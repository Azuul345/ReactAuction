using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReactAuction.Data;
using ReactAuction.DTO.Models;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;

namespace ReactAuction.DTO.Services.Implementations
{
    // This service contains the main logic for registering and logging in users.
    public class UserService : IUserService
    {
        private readonly AppDbContext _contex;

        public UserService(AppDbContext contex)
        {
            _contex = contex;
        }



        public async Task<UserResponse?> RegisterAsync(UserRegisterRequest request)
        {
            var existingUser = await _contex.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                return null;
            }

            var passwordHash = HashPassword(request.Password);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                IsAdmin = false,
                IsActive = true
            };

            _contex.Users.Add(user);
            await _contex.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                IsActive = user.IsActive
            };

        }


        public async Task<UserResponse?> LoginAsync(UserLoginRequest request)
        {
            var user = await _contex.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return null;
            }

            if (!user.IsActive)
            {
                return null;
            }

            var hashedInputPassword = HashPassword(request.Password);

            if (user.PasswordHash != hashedInputPassword)
            {
                return null;
            }

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                IsActive = user.IsActive
            };

        }


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}