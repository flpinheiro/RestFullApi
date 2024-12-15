var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var sql = builder.AddSqlServer("sql")
                .WithDataVolume()
                .WithLifetime(ContainerLifetime.Persistent)
                .AddDatabase("database");

var migrations = builder.AddProject<Projects.RestFullApi_Domain_Infra_MigrationService>("migrations")
    .WithReference(sql)
    .WaitFor(sql);

var apiService = builder.AddProject<Projects.RestFullApi_WebApi>("apiservice")
    .WithReference(sql)
    .WaitFor(sql)
    .WaitForCompletion(migrations);

var commandApi = builder.AddProject<Projects.RestFull_CommandApi>("restfull-commandapi")
    .WithReference(sql)
    .WaitFor(sql)
    .WaitForCompletion(migrations);

var bffApiService = builder.AddProject<Projects.RestFull_BFFApi>("bff")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithReference(commandApi)
    .WaitFor(commandApi);

builder.AddProject<Projects.RestFullApi_Web>("webapi")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(bffApiService)
    .WaitFor(apiService);

builder.Build().Run();
