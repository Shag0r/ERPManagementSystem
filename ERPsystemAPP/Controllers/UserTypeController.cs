using ERPsystemAPP.BusinessLogicInterfaces;
using ERPsystemAPP.BusinessModels.Models.UserType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERPsystemAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeController(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
            
        }


        [HttpPost("UserTypeInsert")]
        public async Task<IActionResult> UserTypeInsert([FromBody] UserRequestDto model)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (model == null)
                    return BadRequest(new { Message = "Category data cannot be null" });

                model.UserId = userId;
                int result = await _userTypeRepository.UserTypeInsert(model);
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while creating category",
                    Details = ex.Message
                });
            }
        }
    }
}
