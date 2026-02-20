using System.Text.Json.Serialization;

namespace ReactAuction.DTO.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Foreign key: which user created this auction.
        public int CreatedByUserId { get; set; }
        // Navigation property: the user who created the auction.
        [JsonIgnore]
        public User? CreatedBtUser { get; set; }

        // True if this auction is active/visible.
        // Admin can set this to false to hide it from searches.
        public bool IsActive { get; set; } = true;

        // Navigation property: all bids placed on this auction.
        [JsonIgnore]
        public ICollection<Bid>? Bids { get; set; }

        // Read-only helper property to check if the auction is currently open.
        // This is computed from EndTime instead of stored in the database.
        public bool IsOpen => IsActive && DateTime.UtcNow < EndTime;




    }
}
