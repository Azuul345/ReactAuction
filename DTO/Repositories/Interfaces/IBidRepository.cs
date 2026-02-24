using ReactAuction.DTO.Models;

namespace ReactAuction.DTO.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task<List<Bid>> GetBidsForAuctionAsync(int auctionId);

        Task<Bid?> GetHighestBidForAuctionAsync(int auctionId);

        Task AddAsync(Bid bid);


    }
}