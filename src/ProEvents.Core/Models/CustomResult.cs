namespace ProEvents.Core.Models;
public class CustomResult
{
    public object Result { get; set; }
    public List<string> Errors { get; set; }

    public CustomResult()
    {
        Errors = new List<string>();
    }

    public virtual bool IsValid => Errors.Count == 0;
}
