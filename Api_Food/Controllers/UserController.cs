using Api_Food.Models;
using Api_Food.Services;
using AutoMapper;
using Food_Users_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Food.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = userRepository.GetByEmail(email);

            if (user == null)
                return BadRequest("No Data Error");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddFood(FoodDto foodDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var food = mapper.Map<Food>(foodDto);
           
            var result = await userRepository.AddFood(email, food);

            if (result == null)
                return BadRequest("Error: Food Not Added");

            return Ok(result);
        }

        [HttpGet("test")]
        public async Task<ActionResult> GetFood()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await userRepository.GetFood(email);

            if (result == null)
                return BadRequest("Error");

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteFood(FoodDto foodDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var food = mapper.Map<Food>(foodDto);
            var result = await userRepository.DeleteFood(email, food);

            if (result == null)
                return BadRequest("Error");

            return Ok(result);
        }

    }
}
