using System.Text.Json.Serialization;

namespace Shared.Result;

public class Result
{
    protected internal Result(bool isSuccess, Error? error = default)
    {
        if (isSuccess && error != default || !isSuccess && error == default)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    public Error? Error { get; } = default;

    public static Result Success() => new(true);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true);

    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static implicit operator Result(Error error) => new(false, error);
}

public class Result<TData> : Result
{
    private readonly TData? _data;

    protected internal Result(TData? value, bool isSuccess, Error? error = default)
        : base(isSuccess, error)
    {
        _data = value;
    }

    public TData? Data => _data;

    public static implicit operator Result<TData>(Error error) => new(default, false, error);

    public static implicit operator Result<TData>(TData? value) => value is not null 
        ? Success(value) 
        : Failure<TData>(Error.NullValue);
}
