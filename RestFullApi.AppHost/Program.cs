var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var elasticsearch = builder.AddElasticsearch("elasticsearch")
                    .WithDataVolume()
                    .WithLifetime(ContainerLifetime.Persistent);

var keycloak = builder.AddKeycloak("keycloak", 8080)
                .WithDataVolume()
                .WithLifetime(ContainerLifetime.Persistent);

var password = builder.AddParameter("password", "Chageme@123", secret: true);

var sqlServer = builder.AddSqlServer("sqlServer", password, port: 14330)
                .WithDataVolume()
                .WithLifetime(ContainerLifetime.Persistent)
                .AddDatabase("database");

var migrationervice = builder.AddProject<Projects.RestFullApi_Domain_Infra_MigrationService>("migrationervice")
                        .WithReference(sqlServer)
                        .WaitFor(sqlServer);

var queryApiService = builder.AddProject<Projects.RestFullApi_WebApi>("queryApiService")
                        .WithReference(sqlServer)
                        .WaitFor(sqlServer)
                        .WithReference(elasticsearch)
                        .WaitFor(elasticsearch)
                        .WaitForCompletion(migrationervice);

var commandApiService = builder.AddProject<Projects.RestFull_CommandApi>("commandApiService")
                        .WithReference(sqlServer)
                        .WaitFor(sqlServer)
                        .WithReference(elasticsearch)
                        .WaitFor(elasticsearch)
                        .WaitForCompletion(migrationervice);

var bffApiService = builder.AddProject<Projects.RestFull_BFFApi>("bffApiService")
                    .WithExternalHttpEndpoints()
                    .WithReference(redis)
                    .WaitFor(redis)
                    .WithReference(queryApiService)
                    .WaitFor(queryApiService)
                    .WithReference(commandApiService)
                    .WaitFor(commandApiService)
                    .WithReference(keycloak)
                    .WaitFor(keycloak);

builder.AddProject<Projects.RestFullApi_Web>("webapp")
        .WithExternalHttpEndpoints()
        .WithReference(redis)
        .WaitFor(redis)
        .WithReference(bffApiService)
        .WaitFor(bffApiService)
        .WithReference(keycloak)
        .WaitFor(keycloak);

builder.Build().Run();
