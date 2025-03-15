using CandidateApplicationManager.Core;
using CandidateApplicationManager.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CandidateApplicationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("fetchById")]
        public async Task<ActionResult<GeneralUser>> GetUsersById(string userId)
        {
            GeneralUser generalUser = await _userRepository.GetUserByIdAsync(userId);
            if (generalUser == null)
            {
                return NotFound();
            }
            return Ok(generalUser);
        }

        [HttpPost("add")]
        public async Task<ActionResult<GeneralUser>> CreateUser(GeneralUser generalUser)
        {
            generalUser.Id = Guid.NewGuid();
            generalUser.UserId = generalUser.Id.ToString();

            GeneralUser createGeneralUser = await _userRepository.CreateUserAsync(generalUser);
            return CreatedAtAction(nameof(GetUsersById), new { userId = generalUser.UserId?.ToString() }, createGeneralUser);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<GeneralUser>> Updateuser(string userId, GeneralUser generalUser)
        {
            GeneralUser existGeneralUser = await _userRepository.GetUserByIdAsync(userId);

            if (existGeneralUser == null)
            {
                return NotFound();
            }

            //Preserve the original ID
            generalUser.Id = existGeneralUser.Id;
            generalUser.UserId = existGeneralUser.UserId;

            GeneralUser? updatedUser = await _userRepository.UpdateUserAsync(generalUser);
            return Ok(updatedUser);
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<GeneralUser>>> GetAllApplication()
        {
            IEnumerable<GeneralUser> generalUsers = await _userRepository.GetAllUsersAsync();
            return Ok(generalUsers);
        }
    }
}
