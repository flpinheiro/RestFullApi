using Microsoft.AspNetCore.Mvc;

namespace RestFull.Domain.Core.Queries;

public abstract class BasicPaginatedQuery
{
    [FromQuery(Name = "page")]
    public int Page { get; set; } = 1;
    [FromQuery(Name = "offset")]
    public int Take { get; set; } = 10;
}

public abstract class PaginatedQuery : BasicPaginatedQuery
{
    [FromQuery(Name = "query")]
    public string? Query { get; set; }
}