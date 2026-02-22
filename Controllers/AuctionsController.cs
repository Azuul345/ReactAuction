using Microsoft.AspNetCore.Mvc;
using ReactAuction.DTO.Requests;
using ReactAuction.DTO.Response;
using ReactAuction.DTO.Responses;
using ReactAuction.DTO.Services.Interfaces;

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
        // For now, we will hard-code creatorUserId (e.g. 1) until we add JWT.
        [HttpPost]
        public async Task<ActionResult<AuctionDetailResponse>> CreateAuction(AuctionCreateRequest request)
        {
            // TODO: replace this with the id from the logged-in user (JWT) later.
            var creatorUserId = 1;

            var result = await _auctionService.CreateAuctionAsync(request, creatorUserId);

            if (result == null)
            {
                return BadRequest(new { message = "Could not create auction." });
            }

            return CreatedAtAction(nameof(GetAuctionById), new { id = result.Id }, result);
        }
    }
}