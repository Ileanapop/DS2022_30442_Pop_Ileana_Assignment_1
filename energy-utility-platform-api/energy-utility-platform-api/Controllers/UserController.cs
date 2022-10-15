using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Utils.CustomExceptions;
using energy_utility_platform_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace energy_utility_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] UserForCreateDto newUserDto)
        {

            var definedUserType = Enum.IsDefined(typeof(UserTypeEnum), newUserDto.Type);

            if (definedUserType)
            {
                try
                {
                    var result = await _userService.AddUser(newUserDto);

                    return Ok(_mapper.Map<UserViewModel>(result));
                }
                catch (ConflictException e)
                {
                    return Conflict(e.Message);
                }
            }
            else
            {
                return BadRequest("Invalid User Type");
            }
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
           var result = await _userService.GetUser(id);

            if (result.Id == Guid.Empty)
            {
                return NotFound("User not found");
            }

            return Ok(_mapper.Map<UserViewModel>(result));
        }

        [HttpGet("byName/{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var result = await _userService.GetUserByName(name);

            if (result.Id == Guid.Empty)
            {
                return NotFound("User not found");
            }

            return Ok(_mapper.Map<UserViewModel>(result));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UserForUpdateDto userUpdateDto)
        {

            var definedUserType = Enum.IsDefined(typeof(UserTypeEnum), userUpdateDto.Type);

            if (definedUserType)
            {
                try
                {
                    var result = await _userService.Update(userUpdateDto);

                    return Ok(_mapper.Map<UserViewModel>(result));
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
            else
            {
                return BadRequest("Invalid User Type");
            }

        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _userService.Delete(id);
            if(result.Id == Guid.Empty)
            {
                return NotFound("User Not Found");
            }

            return Ok(_mapper.Map<UserViewModel>(result));
        }

    }
}
