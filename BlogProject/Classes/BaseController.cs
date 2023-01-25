using BlogProject.Models.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Classes;

public class BaseController : ControllerBase
{
    protected IActionResult ApiResult(bool isSuccess, string message = default, string description = default)
    {
        var response = new BaseResponse()
        {
            IsSuccess = isSuccess,
            Message = message,
            Description = description
        };
        return isSuccess ? Ok(response) : BadRequest(response);
    }

    protected IActionResult ApiResult<TData>(bool isSuccess, string message = "", string description = "",
        TData data = default)
    {
        var response = new BaseResponse<TData>()
        {
            IsSuccess = isSuccess,
            Message = message,
            Description = description,
            Data = data
        };
        return isSuccess ? Ok(response) : BadRequest(response);
    }
}