using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Services;
using energy_utility_platform_api.Utils.CustomExceptions;
using energy_utility_platform_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace energy_utility_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyDeviceController : ControllerBase
    {
        private readonly IEnergyDeviceService _energyDeviceService;

        private readonly IMapper _mapper;

        public EnergyDeviceController(IEnergyDeviceService energyDeviceService, IMapper mapper)
        {
            this._energyDeviceService = energyDeviceService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] EnergyDeviceForCreateDto newEnergyDeviceDto)
        {          
            try
            {
                var result = await _energyDeviceService.Add(newEnergyDeviceDto);

                return Ok(_mapper.Map<EnergyDeviceViewModel>(result));
            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }           
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _energyDeviceService.GetById(id);

            if (result.Id == Guid.Empty)
            {
                return NotFound("Energy device not registered");
            }

            return Ok(_mapper.Map<EnergyDeviceViewModel>(result));
        }

        [HttpGet("byName/{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var result = await _energyDeviceService.GetByModelName(name);

            if (result.Id == Guid.Empty)
            {
                return NotFound("Energy device not found");
            }

            return Ok(_mapper.Map<EnergyDeviceViewModel>(result));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] EnergyDeviceForUpdateDto energyDeviceUpdateDto)
        {
                
            try
            {
                var result = await _energyDeviceService.Update(energyDeviceUpdateDto);

                return Ok(_mapper.Map<EnergyDeviceViewModel>(result));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }          

        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _energyDeviceService.Delete(id);
            if (result.Id == Guid.Empty)
            {
                return NotFound("Energy device not found");
            }

            return Ok(_mapper.Map<EnergyDeviceViewModel>(result));
        }
    }
}
