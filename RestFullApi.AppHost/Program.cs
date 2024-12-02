var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.RestFullApi_WebApi>("apiservice");

var bffApiService = builder.AddProject<Projects.RestFull_BFFApi>("bff")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.RestFullApi_Web>("webapi")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(bffApiService)
    .WaitFor(apiService);

builder.Build().Run();
