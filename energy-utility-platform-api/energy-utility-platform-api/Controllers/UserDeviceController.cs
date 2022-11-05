using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.Utils.CustomExceptions;
using energy_utility_platform_api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace energy_utility_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDeviceController : ControllerBase
    {
        private readonly IUserDeviceService _userDeviceService;

        private readonly IMapper _mapper;

        public UserDeviceController(IUserDeviceService userDeviceService, IMapper mapper)
        {
            this._userDeviceService = userDeviceService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] UserDeviceForCreateDto newUserDeviceDto)
        {
     
           try
           {
                var result = await _userDeviceService.Add(newUserDeviceDto);

               return Ok(_mapper.Map<UserDeviceViewModel>(result));
           }
           catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (ConflictException e)
           {
              return Conflict(e.Message);
           }
 

        }
    }
}
