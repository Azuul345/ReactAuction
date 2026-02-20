using System.Text.Json.Serialization;

namespace ReactAuction.DTO.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int AuctionId { get; set; }

        [JsonIgnore]
        public Auction? Auction { get; set; }

        public int UserID { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}