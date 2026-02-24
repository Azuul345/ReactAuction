using ReactAuction.DTO.Models;

namespace ReactAuction.DTO.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> FindUserById(int id);
        Task AddAsync(User user);

        Task SaveChangesAsync();
        Task<List<User>> GetAllAsync();
    }
}