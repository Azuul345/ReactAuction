using Microsoft.AspNetCore.Mvc;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Response;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ReactAuction.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // => api/Auctions
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: /api/Auctions?search=foo
        [HttpGet]
        public async Task<ActionResult<List<AuctionListItemResponse>>> GetOpenAuctions([FromQuery] string? search)
        {
            var result = await _auctionService.GetOpenAuctionsAsync(search);
            return Ok(result);
        }

        // GET: /api/Auctions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AuctionDetailResponse>> GetAuctionById(int id)
        {
            var result = await _auctionService.GetAuctionByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: /api/Auctions
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AuctionDetailResponse>> CreateAuction(AuctionCreateRequest request)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            // TODO: replace this with the id from the logged-in user (JWT) later.
            var creatorUserId = int.Parse(userIdClaim.Value);

            var result = await _auctionService.CreateAuctionAsync(request, creatorUserId);

            if (result == null)
            {
                return BadRequest(new { message = "Could not create auction." });
            }

            return CreatedAtAction(nameof(GetAuctionById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<AuctionDetailResponse>> UpdateAuction(int id, AuctionCreateRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var result = await _auctionService.UpdateAuctionAsync(id, request, userId);

            if (result == null)
            {
                return BadRequest(new { message = "Cannot update auction (not found, not owner, or price change not allowed when bids exist)." });
            }

            return Ok(result);

        }


        [HttpPatch("{id}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeactivateAuction(int id)
        {
            var success = await _auctionService.DeactivateAuctionAsync(id);

            if (!success)
            {
                return NotFound(new { message = "Auction not found." });
            }

            return NoContent();
        }
    }


}
