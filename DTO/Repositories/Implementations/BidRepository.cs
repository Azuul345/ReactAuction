using Microsoft.EntityFrameworkCore;
using ReactAuction.Data;
using ReactAuction.DTO.Models;
using ReactAuction.DTO.Repositories.Interfaces;

namespace ReactAuction.DTO.Repositories.Implementations
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _context;

        public BidRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Bid bid)
        {
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bid>> GetBidsForAuctionAsync(int auctionId)
        {
            return await _context.Bids.Include(b => b.User).Where(b => b.AuctionId == auctionId).OrderByDescending(b => b.Amount)
            .ThenBy(b => b.CreatedAt).ToListAsync();
        }

        public async Task<Bid?> GetHighestBidForAuctionAsync(int auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionId == auctionId).OrderByDescending(b => b.Amount).FirstOrDefaultAsync();
        }
    }
}