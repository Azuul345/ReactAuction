using ReactAuction.DTO.Models;
using ReactAuction.DTO.Repositories.Interfaces;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;

namespace ReactAuction.DTO.Services.Implementations
{
    public class BidService : IBidService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IUserRepository _userRepository;

        public BidService(
            IAuctionRepository auctionRepository,
            IBidRepository bidRepository,
            IUserRepository userRepository)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
            _userRepository = userRepository;
        }

        public async Task<BidResponse?> PlaceBidAsync(BidCreateRequest request, int userId)
        {
            var auction = await _auctionRepository.GetByIdWithBidsAsync(request.AuctionId);

            if (auction == null || !auction.IsActive || !auction.IsOpen)
            {
                return null;
            }

            if (auction.CreatedByUserId == userId)
            {
                return null;
            }

            var highestBid = await _bidRepository.GetHighestBidForAuctionAsync(request.AuctionId);

            decimal minimumAllowed = highestBid?.Amount ?? auction.StartPrice;

            if (request.Amount <= minimumAllowed)
            {
                return null;
            }

            var bid = new Bid
            {
                AuctionId = request.AuctionId,
                UserID = userId,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow
            };

            await _bidRepository.AddAsync(bid);

            return new BidResponse
            {
                Id = bid.Id,
                AuctionId = bid.AuctionId,
                Amount = bid.Amount,
                CreatedAt = bid.CreatedAt,
                UserId = bid.UserID,
                BidderName = string.Empty
            };

        }

        public async Task<bool> UndoLastBidAsync(int auctionId, int userId)
        {
            var auction = await _auctionRepository.GetByIdWithBidsAsync(auctionId);

            if (auction == null || !auction.IsOpen)
            {
                return false;
            }

            var lastBid = auction.Bids.OrderByDescending(b => b.CreatedAt).FirstOrDefault();

            if (lastBid == null)
            {
                return false;
            }

            _bidRepository.Remove(lastBid);
            await _bidRepository.SaveChangesAsync();
            return true;
        }
    }
}