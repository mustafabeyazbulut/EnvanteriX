using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using EnvanteriX.Application.Features.Queries.AssetMovementQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetMovementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetMovementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Tüm hareketleri getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAssetMovementsQuery());
            return Ok(result);
        }

        // Id'ye göre hareket getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAssetMovementByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // Belirli bir varlık (asset) hareketlerini getir
        [HttpGet("asset/{assetId}")]
        public async Task<IActionResult> GetByAssetId(int assetId)
        {
            var result = await _mediator.Send(new GetAssetMovementsByAssetIdQuery(assetId));
            return Ok(result);
        }

        // Belirli bir lokasyon hareketlerini getir (gelen/giden)
        [HttpGet("location/{locationId}")]
        public async Task<IActionResult> GetByLocationId(int locationId)
        {
            var result = await _mediator.Send(new GetAssetMovementsByLocationIdQuery(locationId));
            return Ok(result);
        }

        // Belirli bir kullanıcı hareketlerini getir (gönderen/alıcı)
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _mediator.Send(new GetAssetMovementsByUserIdQuery(userId));
            return Ok(result);
        }

        // Yeni hareket oluştur
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssetMovementCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // Mevcut hareketi güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAssetMovementCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // Hareketi sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteAssetMovementCommand (id));

            return NoContent();
        }
    }
}
