namespace ReactAuction.DTO.Requests
{
    // DTO used when a client wants to create a new auction.

    public class AuctionCreateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}