using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.Services;
using energy_utility_platform_api.Utils.CustomExceptions;
using energy_utility_platform_api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace energy_utility_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyConsumptionController : ControllerBase
    {
        private readonly IEnergyConsumptionService _energyConsumptionService;

        private readonly IMapper _mapper;

        public EnergyConsumptionController(IEnergyConsumptionService energyConsumptionService, IMapper mapper)
        {
            this._energyConsumptionService = energyConsumptionService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] EnergyConsumptionDtoForCreate energyConsumptionDtoForCreate)
        {

            try
            {
                var result = await _energyConsumptionService.Add(energyConsumptionDtoForCreate);

                return Ok(_mapper.Map<EnergyConsumptionViewModel>(result));
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Authorize(Policy = Policies.Client)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _energyConsumptionService.GetAll(id);

            if (result.Count() == 0)
            {
                return NotFound("User device does not have consumptions registered");
            }

            return Ok(result.Select(x => _mapper.Map<EnergyConsumptionViewModel>(x)));
        }

        [HttpGet("byDay")]
        [Authorize(Policy = Policies.Client)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDay([FromQuery] Guid id, [FromQuery] DateTime date)
        {
           
            var result = await _energyConsumptionService.GetEnergyConsumptionByDay(id, date);

            if (result.Count() == 0)
            {
                return NotFound("No energy consumptions registred for that day");
            }

            return Ok(result.Select(x => _mapper.Map<DailyEnergyConsumptionViewModel>(x)));
        }
    }
}
