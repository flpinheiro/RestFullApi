using Microsoft.AspNetCore.Mvc;

namespace RestFull.Domain.Core.Queries;

public class BasicPaginatedQuery
{
    [FromQuery(Name = "page")]
    public int Page { get; set; }
    [FromQuery(Name = "offset")]
    public int Take { get; set; }
}

public class PaginatedQuery : BasicPaginatedQuery
{
    [FromQuery(Name = "query")]
    public string? Query { get; set; }
}