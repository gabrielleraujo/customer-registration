# Customer registration

#### Run:
- To install the database image and build the SQL Server container.
1. $ docker-compose up
2. dotnet ef --startup-project ../CustomerRegistration.API/  database update

### New Migration:
- To create a new migration run the command below in the terminal in the path "src/CustomerRegistration.Infrastructure".
1. dotnet ef --startup-project ../CustomerRegistration.API/  migrations add initial -c CustomerRegistrationContext --msbuildprojectextensionspath local/obj -v
2. dotnet ef --startup-project ../CustomerRegistration.API/  database update

#### Swagger
- https://localhost:7014/swagger/index.html

