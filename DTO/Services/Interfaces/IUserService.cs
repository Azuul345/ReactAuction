using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;

namespace ReactAuction.DTO.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> RegisterAsync(UserRegisterRequest request);
        Task<LoginResponse?> LoginAsync(UserLoginRequest request);

        Task<List<UserResponse>> GetAllAsync();

        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

    }
}