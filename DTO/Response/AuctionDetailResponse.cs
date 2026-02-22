namespace ReactAuction.DTO.Response
{
    // DTO used when viewing a single auction in detail.

    public class AuctionDetailResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsOpen { get; set; }
        public bool IsActive { get; set; }

        public int CreatedByUserId { get; set; }
        public string CreatedByName { get; set; } = string.Empty;

        public List<AuctionBidResponse> Bids { get; set; } = new();

    }
}