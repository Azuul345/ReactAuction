using ReactAuction.DTO.Models;
using ReactAuction.DTO.Repositories.Interfaces;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Response;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;

namespace ReactAuction.DTO.Services.Implementations
{
    public class AuctionService : IAuctionService
    {

        private readonly IAuctionRepository _auctionRepository;
        private readonly IUserRepository _userRepository;

        public AuctionService(IAuctionRepository auctionRepository, IUserRepository userRepository)
        {
            _auctionRepository = auctionRepository;
            _userRepository = userRepository;
        }


        public async Task<AuctionDetailResponse?> CreateAuctionAsync(AuctionCreateRequest request, int creatorUserId)
        {
            var creator = await _userRepository.GetByEmailAsync(email: string.Empty);

            var auction = new Auction
            {
                Title = request.Title,
                Description = request.Description,
                StartPrice = request.StartPrice,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CreatedByUserId = creatorUserId,
                IsActive = true
            };

            await _auctionRepository.AddAsync(auction);

            return new AuctionDetailResponse
            {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                StartPrice = auction.StartPrice,
                StartTime = auction.StartTime,
                EndTime = auction.EndTime,
                IsOpen = auction.IsOpen,
                IsActive = auction.IsActive,
                CreatedByUserId = auction.CreatedByUserId,
                CreatedByName = string.Empty,
                Bids = new List<AuctionBidResponse>()
            };




        }

        public async Task<AuctionDetailResponse?> GetAuctionByIdAsync(int id)
        {
            var auction = await _auctionRepository.GetByIdWithBidsAsync(id);

            if (auction == null)
            {
                return null;
            }

            var bids = auction.Bids?.OrderByDescending(b => b.Amount).ThenBy(b => b.CreatedAt).Select(b => new AuctionBidResponse
            {
                Id = b.Id,
                Amount = b.Amount,
                CreatedAt = b.CreatedAt,
                BidderName = b.User?.Name ?? string.Empty
            }).ToList() ?? new List<AuctionBidResponse>();

            return new AuctionDetailResponse
            {
                Id = auction.Id,
                Title = auction.Title,
                Description = auction.Description,
                StartPrice = auction.StartPrice,
                StartTime = auction.StartTime,
                EndTime = auction.EndTime,
                IsOpen = auction.IsOpen,
                IsActive = auction.IsActive,
                CreatedByUserId = auction.CreatedByUserId,
                CreatedByName = auction.CreatedBtUser?.Name ?? string.Empty,
                Bids = bids
            };


        }

        public async Task<List<AuctionListItemResponse>> GetOpenAuctionsAsync(string? titleSearch)
        {
            var auctions = await _auctionRepository.GetOpenAuctionsAsync(titleSearch);

            return auctions.Select(a => new AuctionListItemResponse
            {
                Id = a.Id,
                Title = a.Title,
                StartPrice = a.StartPrice,
                EndTime = a.EndTime,
                IsOpen = a.IsOpen,
                CreatedByName = a.CreatedBtUser?.Name ?? string.Empty
            }).ToList();
        }
    }

}
