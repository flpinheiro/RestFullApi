using Microsoft.AspNetCore.Mvc;

namespace RestFull.Domain.Core.Dtos;

public class BasicPaginatedQueryDto
{
    [FromQuery(Name = "page")]
    public int Page { get; set; }
    [FromQuery(Name = "offset")]
    public int Take { get; set; }
}

public class PaginatedQueryDto : BasicPaginatedQueryDto
{
    [FromQuery(Name = "query")]
    public string? Query { get; set; }
}