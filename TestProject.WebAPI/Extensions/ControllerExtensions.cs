namespace TestProject.WebAPI.Extensions
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using TestProject.Business.Models;

    public static class ControllerExtensions
    {
        public static ActionResult FromResult<source,destination>(this ControllerBase controller, Result<source> result, IMapper mapper)
        {
            return result.ResultType switch
            {
                ResultType.Ok => controller.Ok(mapper.Map<destination>(result.Data)),
                ResultType.NotFound => controller.NotFound(result.Errors),
                ResultType.Invalid => controller.BadRequest(result.Errors),
                ResultType.Conflict => controller.Conflict(result.Errors),
                _ => throw new Exception("An unexpected error has occurred."),
            };
        }

        public static ActionResult FromResult<t>(this ControllerBase controller, Result<t> result)
        {
            return result.ResultType switch
            {
                ResultType.Ok => controller.Ok(result.Data),
                ResultType.NotFound => controller.NotFound(result.Errors),
                ResultType.Invalid => controller.BadRequest(result.Errors),
                ResultType.Conflict => controller.Conflict(result.Errors),
                _ => throw new Exception("An unexpected error has occurred."),
            };
        }
    }
}
