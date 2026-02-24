namespace ReactAuction.DTO.Requests
{
    public class BidCreateRequest
    {
        public int AuctionId { get; set; }
        public decimal Amount { get; set; }
    }
}