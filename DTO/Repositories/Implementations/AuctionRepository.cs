using Microsoft.EntityFrameworkCore;
using ReactAuction.Data;
using ReactAuction.DTO.Models;
using ReactAuction.DTO.Repositories.Interfaces;

namespace ReactAuction.DTO.Repositories.Implementations
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly AppDbContext _context;

        public AuctionRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Auction auction)
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
        }

        public async Task<Auction?> GetByIdWithBidsAsync(int id)
        {
            return await _context.Auctions.Include(a => a.CreatedBtUser).Include(a => a.Bids)
            .ThenInclude(b => b.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Auction>> GetOpenAuctionsAsync(string? titleSearch)
        {
            var query = _context.Auctions.Include(a => a.CreatedBtUser)
            .Where(a => a.IsActive && a.EndTime > DateTime.UtcNow);

            if (!string.IsNullOrWhiteSpace(titleSearch))
            {
                query = query.Where(a => a.Title.Contains(titleSearch));
            }

            return await query.OrderBy(a => a.EndTime).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}