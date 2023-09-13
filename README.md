# Customer registration
#### Migration:
- To create a new migration run the command below in the terminal in the path "src/CustomerRegistration.Infrastructure".

1. dotnet ef --startup-project ../CustomerRegistration.API/  migrations add initial -c CustomerRegistrationCommandContext --msbuildprojectextensionspath local/obj -v
2. dotnet ef --startup-project ../CustomerRegistration.API/  database update

#### Docker
- Run docker-compose up in the project root directory to start all services defined in the docker-compose.yml file.

#### Swagger
- https://localhost:7014/swagger/index.html

