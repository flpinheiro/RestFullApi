var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var sqlServer = builder.AddSqlServer("sqlServer")
                .WithDataVolume()
                .WithLifetime(ContainerLifetime.Persistent)
                .AddDatabase("database");

var migrationervice = builder.AddProject<Projects.RestFullApi_Domain_Infra_MigrationService>("migrationervice")
                        .WithReference(sqlServer)
                        .WaitFor(sqlServer);

var queryApiService = builder.AddProject<Projects.RestFullApi_WebApi>("queryApiService")
                        .WithReference(sqlServer)
                        .WaitFor(sqlServer)
                        .WaitForCompletion(migrationervice);

var commandApiService = builder.AddProject<Projects.RestFull_CommandApi>("commandApiService")
                        .WithReference(sqlServer)
                        .WaitFor(sqlServer)
                        .WaitForCompletion(migrationervice);

var bffApiService = builder.AddProject<Projects.RestFull_BFFApi>("bffApiService")
                    .WithExternalHttpEndpoints()
                    .WithReference(redis)
                    .WaitFor(redis)
                    .WithReference(queryApiService)
                    .WaitFor(queryApiService)
                    .WithReference(commandApiService)
                    .WaitFor(commandApiService);

builder.AddProject<Projects.RestFullApi_Web>("webapp")
        .WithExternalHttpEndpoints()
        .WithReference(redis)
        .WaitFor(redis)
        .WithReference(bffApiService)
        .WaitFor(bffApiService);

builder.Build().Run();
