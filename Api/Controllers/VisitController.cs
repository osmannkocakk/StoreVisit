using Api.Services.Interfaces;
using Domain.DTOs;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/visits")]
    [ApiController]
    [Authorize]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VisitController(IVisitService visitService, IHttpContextAccessor httpContextAccessor)
        {
            _visitService = visitService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);

        [HttpPost]
        [Authorize(Roles = "Standard")]
        public async Task<IActionResult> CreateVisit([FromBody] VisitRequestDto request)
        {
            var visit = await _visitService.CreateVisit(GetUserId(), request.StoreId);
            return Created("", visit);
        }

        [HttpGet]
        public async Task<IActionResult> GetVisits([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userId = GetUserId();
            var visits = await _visitService.GetUserVisits(userId, page, pageSize);
            return Ok(visits);
        }

        [HttpGet("{visitId}")]
        public async Task<IActionResult> GetVisitDetails(int visitId)
        {
            var visit = await _visitService.GetVisitById(visitId, GetUserId());
            if (visit == null) return NotFound();
            return Ok(visit);
        }

        [HttpPost("{visitId}/photos")]
        public async Task<IActionResult> UploadPhoto(int visitId, [FromBody] Photo photo)
        {
            var userId = GetUserId();
            var added = await _visitService.AddPhotoToVisit(visitId, photo.ProductId, photo.Base64Image);
            if (!added) return BadRequest(new { Message = "Failed to add photo" });

            return Ok(new { Message = "Photo uploaded successfully" });
        }

        [HttpPut("{visitId}/complete")]
        public async Task<IActionResult> CompleteVisit(int visitId)
        {
            var userId = GetUserId();
            var completed = await _visitService.CompleteVisit(visitId, userId);
            if (!completed) return BadRequest(new { Message = "Failed to complete visit" });

            return Ok(new { Message = "Visit marked as completed" });
        }
    }

}
