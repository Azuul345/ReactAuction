namespace ReactAuction.DTO.Response
{
    // DTO representing a single bid inside an auction detail view.

    public class AuctionBidResponse
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BidderName { get; set; } = string.Empty;
    }
}