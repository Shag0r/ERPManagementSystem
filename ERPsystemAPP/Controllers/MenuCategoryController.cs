using ERPsystemAPP.BusinessLogicImplementation;
using ERPsystemAPP.BusinessLogicInterfaces;
using ERPsystemAPP.BusinessModels.Models;
using ERPsystemAPP.BusinessModels.Models.MenuCategoryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERPsystemAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoryController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IMenuCategoryRepository _menuCategoryeRepository;
        private ResponseDto _responseDto = new ResponseDto();
        public MenuCategoryController(IMenuCategoryRepository menuCategoryeRepository, IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
            _menuCategoryeRepository = menuCategoryeRepository;

        }


        [HttpPost("InsertMenuCategories")]
        public async Task<IActionResult> InsertMenuCategories([FromBody] MenuCategoryRequestDto model)
        {
            _responseDto = new ResponseDto();
            try
            {

               if(model == null)
                {
                    return BadRequest("Empty"+model);
                }
                #region "Hash Checking"
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                #endregion
                model.UserId = userId;

                int insertMenuCategories = await _menuCategoryeRepository.InsertMenuCategories(model);
                if (insertMenuCategories > 0)
                {
                    model.Id = insertMenuCategories;
                }

                _responseDto.StatusCode = (int)StatusCodes.Status200OK;
                _responseDto.Message = "Data Save Successfully";
                _responseDto.Data = model;


            }
            catch (Exception ex) {
                _responseDto = new ResponseDto();
                _responseDto.Message = ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message;

            }
            return Ok(_responseDto);
        }
    }
            
}
