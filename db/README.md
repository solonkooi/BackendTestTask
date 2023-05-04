# Database Flyway migrations

## How-To Migrate

1. Install an instance of Postgres database on your local environment.
   It is expected that you Postgres instance listens to `localhost:5432` 

1. Create admin user __postgres__ with password __postgres__
1. Create required databases. For ex. `backendtesttask`
1. Install uuid extension on your database:
   ```postgresql
   CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
   ```
1. Run required `migrate-*` script. Database must be migrated successfully. 

If you have some migration conflicts, you can try to edit `public.schema_version` table,
or just re-create the entire database,

## More Information

1. [Command Line Instructions](https://flywaydb.org/documentation/commandline/)
   
   Common use case: 
   ```shell script
   flyway -url=jdbc:postgresql://localhost:5432/backendtesttask -user=postgres -password=postgres -locations=filesystem:db/backendtesttask migrate 
   ```
1. [How-To : Add new SQL migration](https://flywaydb.org/documentation/migrations#sql-based-migrations)
