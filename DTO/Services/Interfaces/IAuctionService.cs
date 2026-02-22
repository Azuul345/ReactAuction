using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Response;
using ReactAuction.DTO.Responses;

namespace ReactAuction.DTO.Services.Interfaces
{
    public interface IAuctionService
    {
        Task<List<AuctionListItemResponse>> GetOpenAuctionsAsync(string? titleSearch);

        Task<AuctionDetailResponse?> GetAuctionByIdAsync(int id);

        Task<AuctionDetailResponse?> CreateAuctionAsync(AuctionCreateRequest request, int creatorUserId);
    }
}