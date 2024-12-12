# entity Framwork 

## Add-Migration

```cli
	Add-Migration -Name MigrationName -Project RestFull.Domain.Infra -StartupProject RestFull.CommandApi -OutputDir Migrations
```

## Update-Database

```cli
	Update-Database -Migration FirstMigration -Project RestFull.Domain.Infra -StartupProject RestFull.CommandApi Connection database
```
