using ReactAuction.DTO.Models;

namespace ReactAuction.DTO.Responses
{
    public class LoginResponse
    {
        public UserResponse User { get; set; } = new UserResponse();
        public string Token { get; set; } = string.Empty;
    }
}