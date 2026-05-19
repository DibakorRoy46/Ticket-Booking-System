namespace SharedKernal.Application.Common;

public sealed class PagedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;

    private PagedList(
        IReadOnlyList<T> items,
        int page,
        int pageSize,
        int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static PagedList<T> Create(
        IEnumerable<T> items,
        int page,
        int pageSize,
        int totalCount)
        => new(items.ToList().AsReadOnly(), page, pageSize, totalCount);

    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> source,
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        // totalCount query runs before the data query for accurate count
        var totalCount = source.Count();
        var items = source
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .ToList();

        return new(items.AsReadOnly(), pagination.Page, pagination.PageSize, totalCount);
    }

    public PagedList<TOut> Map<TOut>(Func<T, TOut> mapper)
        => new(
            Items.Select(mapper).ToList().AsReadOnly(),
            Page,
            PageSize,
            TotalCount);
}