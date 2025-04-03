using Api.Services.Interfaces;
using DbComtext;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    [Route("api/stores")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetStores([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var stores = await _storeService.GetStores(page, pageSize);
            return Ok(stores);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] Store store)
        {
            var createdStore = await _storeService.CreateStore(store);
            return CreatedAtAction(nameof(GetStores), new { id = createdStore.Id }, createdStore);
        }

        [HttpPut("{storeId}")]
        public async Task<IActionResult> UpdateStore(int storeId, [FromBody] Store store)
        {
            var updated = await _storeService.UpdateStore(storeId, store);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{storeId}")]
        public async Task<IActionResult> DeleteStore(int storeId)
        {
            var deleted = await _storeService.DeleteStore(storeId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
