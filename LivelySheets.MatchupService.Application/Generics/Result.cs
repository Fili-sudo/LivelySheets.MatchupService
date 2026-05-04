namespace LivelySheets.MatchupService.Application.Generics;

public class Result<T>
{
    public T? Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public List<string>? Errors { get; private set; }
    public Exception? Exception { get; private set; }
    public bool RethrowException { get; set; }

    public Result<T> Undergo()
        => IsSuccess || !RethrowException || Exception is null ? this
        : throw Exception;

    public static Result<T> Success() => new() { IsSuccess = true };

    public static Result<T> Failure(List<string> errors, Exception? exception = null)
        => new() { IsSuccess = false, Errors = errors, Exception = exception };

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

    public static implicit operator Result<T>(Exception exception)
        => new()
        {
            IsSuccess = false,
            Exception = exception,
            Errors = [exception.Message],
        };
}
