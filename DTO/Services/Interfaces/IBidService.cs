using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;

namespace ReactAuction.DTO.Services.Interfaces
{
    // Business logic for placing bids.
    public interface IBidService
    {
        Task<BidResponse?> PlaceBidAsync(BidCreateRequest request, int userId);

        Task<bool> UndoLastBidAsync(int auctionId, int userId);
    }
}