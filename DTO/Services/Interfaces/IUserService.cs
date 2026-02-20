using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;

namespace ReactAuction.DTO.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> RegisterAsync(UserRegisterRequest request);
        Task<UserResponse?> LoginAsync(UserLoginRequest request);
    }
}