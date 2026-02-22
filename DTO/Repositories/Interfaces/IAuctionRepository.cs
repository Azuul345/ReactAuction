using ReactAuction.DTO.Models;

namespace ReactAuction.DTO.Repositories.Interfaces
{
    public interface IAuctionRepository
    {
        Task<List<Auction>> GetOpenAuctionsAsync(string? titleSearch);

        Task<Auction?> GetByIdWithBidsAsync(int id);

        Task AddAsync(Auction auction);
    }

}