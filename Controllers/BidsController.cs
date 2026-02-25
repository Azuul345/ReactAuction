using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;
using System.Security.Claims;


namespace ReactAuction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidsController(IBidService bidService)
        {
            _bidService = bidService;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BidResponse>> PlaceBid(BidCreateRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            var result = await _bidService.PlaceBidAsync(request, userId);

            if (result == null)
            {
                return BadRequest(new { message = "Bid is not allowed" });
            }
            return Ok(result);
        }

        [HttpDelete("undo-last-bid/{auctionId}")]
        [Authorize]
        public async Task<IActionResult> UndoLastBid(int auctionId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var success = await _bidService.UndoLastBidAsync(auctionId, userId);

            if (!success)
            {
                return BadRequest(new { message = "Cannot undo last bid (no bids, not your bid, or auction is closed)." });
            }
            return NoContent();
        }
    }
}