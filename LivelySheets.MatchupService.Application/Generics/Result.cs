namespace LivelySheets.MatchupService.Application.Generics;

public class Result<T>
{
    public T? Value { get; set; }
    public bool IsSuccess { get; set; }
    public List<string>? Errors { get; set; }

    public static Result<T> Success() => new() { IsSuccess = true };
    public static Result<T> Failure(List<string> errors) => new() { IsSuccess = false, Errors = errors };

    public static implicit operator Result<T>(T value)
        => new()
        {
            Value = value,
            IsSuccess = true,
        };

    public static implicit operator Result<T>(List<string> errors)
        => new()
        {
            IsSuccess = false,
            Errors = errors,
        };
}
