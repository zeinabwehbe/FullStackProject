using AutoMapper;
using Catel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;
using serverside.Models.DTOs.User;
using serverside.Repository;

namespace serverside.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // this is to initialize them
        //private readonly projectDbContext dbContext;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController( IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            //this.dbContext = projectDbContext;
            this.mapper = mapper;

        }

        // GET ALL USERS
        // https://localhost:7069/api/Users
        //GET: /api/walks?filerOn=MyProperty&filterQuery=Track
        [HttpGet]
        //public async Task<IActionResult> GetAllUsers([FromQuery] string filterOn, [FromQuery] int? filterQuery )
        public async Task<IActionResult> GetAllUsers()

        {
            // Get data from the database - Domain Model
            //var usersDomain = await userRepository.GetAllUsersAsync(filterOn, filterQuery);
            var usersDomain = await userRepository.GetAllUsersAsync();

            // Map Domain Models to DTOs
            var usersDto = mapper.Map<List<UserDto>>(usersDomain);

            // Return DTOs
            return Ok(usersDto);
        }

        // GET USER BY ID
        // https://localhost:7069/api/Users/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var userDomain = await userRepository.GetUserByIdAsync(id);
            if (userDomain == null)
            {
                return NotFound();
            }
            // Map Domain Models to DTOs
            var userDto = mapper.Map<UserDto>(userDomain);

            return Ok(userDto);
        }

        // POST to create new user
        // POST: https://localhost:7069/api/Users
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto addUserRequestDto)
        {
            // Map DTO to domain model
            var userDomainModel = mapper.Map<Users>(addUserRequestDto);

            // Use domain model to create user
            userDomainModel = await userRepository.CreateAsync(userDomainModel);

            // Map Domain Models to DTOs
            var userDto = mapper.Map<UserDto>(userDomainModel);

            return CreatedAtAction(nameof(GetUserById), new { id = userDomainModel.Id }, userDto);

        }

        // UPDATE USER
        // PUT: https://localhost:7069/api/Users/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map  DTO To domain model
                var userDomainModel = mapper.Map<Users>(updateUserRequestDto);

                //check if user exist
                userDomainModel = await userRepository.UpdateAsync(id, userDomainModel);

                if (userDomainModel == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                // Map Domain Models to DTOs
                var userDto = mapper.Map<UserDto>(userDomainModel);

                return Ok(new
                {
                    Message = "User updated successfully.",
                    UpdatedUser = userDto
                });
            }
            else
            {
                return BadRequest(ModelState);

            }
        }

        // DELETE USER
        // DELETE: https://localhost:7069/api/Users/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userDomainModel = await userRepository.DeleteAsync(id);

            if (userDomainModel == null)
            {
                return NotFound($"User with ID {id} not found.");
            }


            // Map Domain Models to DTOs
            var userDto = mapper.Map<UserDto>(userDomainModel);

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginToProfile([FromBody] LoginUserRequestDto loginUserRequestDto)
        {
            var userDomain = await userRepository.GetUserByEmailAsync(loginUserRequestDto.Email);
            if (userDomain == null || (loginUserRequestDto.Password != userDomain.Password))
            {
                return Unauthorized("Invalid email or password.");
            }
            var userDto = mapper.Map<UserDto>(userDomain);
            return Ok(userDto);
        }

    }
}
