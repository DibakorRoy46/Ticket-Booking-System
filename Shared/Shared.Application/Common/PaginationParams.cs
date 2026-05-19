namespace SharedKernal.Application.Common;

public sealed record PaginationParams
{
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    public int Page { get; init; }
    public int PageSize { get; init; }

    public PaginationParams(int page = 1, int pageSize = DefaultPageSize)
    {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? DefaultPageSize
                 : pageSize > MaxPageSize ? MaxPageSize
                 : pageSize;
    }

    public int Skip => (Page - 1) * PageSize;
    public int Take => PageSize;
}
