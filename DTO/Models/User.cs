using System.Text.Json.Serialization;

namespace ReactAuction.DTO.Models
{
    // This class represents a user account in the system.
    // It will map to a "Users" table in the database.
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public ICollection<Auction>? Auctions { get; set; }
        [JsonIgnore]
        public ICollection<Bid>? Bids { get; set; }



    }
}