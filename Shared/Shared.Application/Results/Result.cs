namespace SharedKernal.Application.Results;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException(
                "A successful result cannot carry an error.");
        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException(
                "A failed result must carry an error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value)
        => new(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error)
        => new(default!, false, error);

    public static Result<TValue> Create<TValue>(TValue? value)
        => value is not null
            ? Success(value)
            : Failure<TValue>(Error.NullValue);
}

public sealed class Result<TValue> : Result
{
    private readonly TValue _value;

    internal Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
        => _value = value;

    public TValue Value => IsSuccess
        ? _value
        : throw new InvalidOperationException(
            $"Cannot access Value of a failed result. Error: {Error}");

    public static implicit operator Result<TValue>(TValue value)
        => value is not null
            ? Success(value)
            : Failure<TValue>(Error.NullValue);

    public Result<TOut> Map<TOut>(Func<TValue, TOut> mapper)
        => IsSuccess
            ? Result.Success(mapper(Value))
            : Result.Failure<TOut>(Error);

    public async Task<Result<TOut>> MapAsync<TOut>(Func<TValue, Task<TOut>> mapper)
        => IsSuccess
            ? Result.Success(await mapper(Value))
            : Result.Failure<TOut>(Error);

    public Result<TValue> Tap(Action<TValue> action)
    {
        if (IsSuccess) action(Value);
        return this;
    }
}