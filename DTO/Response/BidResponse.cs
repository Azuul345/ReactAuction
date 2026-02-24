namespace ReactAuction.DTO.Responses
{
    public class BidResponse
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string BidderName { get; set; } = string.Empty;
    }
}