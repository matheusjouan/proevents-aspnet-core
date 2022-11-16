namespace ProEvents.Core.Models;
public abstract class ObjectResult
{
    protected CustomResult _customResult;

    protected ObjectResult()
    {
        _customResult = new CustomResult();
    }

    protected void AddError(string message) => _customResult.Errors.Add(message);

    protected void AddResult(object result) => _customResult.Result = result; 
}
