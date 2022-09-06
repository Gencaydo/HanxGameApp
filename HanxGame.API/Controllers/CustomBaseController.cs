using HanxGame.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HanxGame.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> responseDto)
        {
            if (responseDto.StatusCode == 204) 
                return new ObjectResult(null) 
                { 
                    StatusCode = responseDto.StatusCode 
                };

            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode,
            };
        }
    }
}
