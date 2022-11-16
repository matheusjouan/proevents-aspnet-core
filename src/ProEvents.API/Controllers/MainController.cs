using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProEvents.Core.Models;

namespace ProEvents.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperationValid())
        {
            NotFound();
            return Ok(result);
        }

        // ValidationProblemDetails => implementa um padeão RFC de como deve responder detalhes de erro
        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() },
        }));
    }

    protected ActionResult CustomResponse(CustomResult customResult)
    {
        if (customResult.IsValid)
            return CustomResponse(customResult.Result);

        foreach (var erro in customResult.Errors)
        {
            AddError(erro);
        }

        return CustomResponse();
    }

    // Sobrescrevendo NotFound()
    public override NotFoundObjectResult NotFound([ActionResultObjectValue] object? value)
    {
        var obj = (CustomResult)value;

        foreach (var erro in obj.Errors)
            AddError(erro);

        return new NotFoundObjectResult(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            { "Messages", Errors.ToArray() },
        }));
    }

    protected bool OperationValid() => !Errors.Any();

    protected void AddError(string error) => Errors.Add(error);

    protected void ClearError() => Errors.Clear();
}
