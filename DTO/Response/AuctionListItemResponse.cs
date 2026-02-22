namespace ReactAuction.DTO.Response
{
    // DTO used when listing or searching auctions (summary view).

    public class AuctionListItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsOpen { get; set; }

        public string CreatedByName { get; set; } = string.Empty;
    }
}