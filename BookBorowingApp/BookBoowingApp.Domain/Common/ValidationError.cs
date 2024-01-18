namespace BookBoowingApp.Domain.Common;

public class ValidationError(string code, string description)
{
    public string Code { get; set; } = code;
    public string Description { get; set; } = description;
}
