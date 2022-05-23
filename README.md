# Checkout

##How to run 

The application requires a database to store payment requests
- Setup database with docker-compose.yml 

- build the solution and create the dabaase
```shell
dotnet restore 
dotnet build
dotnet ef database update
```

- run the tests
  - unit tests
  - E2E tests 

##Assumptions

- for simplicity , there is no authentication/authorization in place. For a real world scenario, a JWT based solution is required to process all payment requests

## Area of improvment
- add retry and circuit breaker policies around the bank Simulator
- add additional scenarios in the simulator : eg to test the circuit breaker with transient failures :
  - add routes to enable/disable the circuit breaker and add additonal logic the API to display a message that the payment gateway currently process any payments
- add health checks (a must with the cloud architecture described below)
- add logging in the simulator service and add serilog sinks to push log streams to an OpenSearch service
- enrich the DDD model , using value object for example to store the Credit car data and associated validation rules
- I could have used a simple API key based authentication to secure access to the resources

## Cloud technologies
- I would recommend using a managed kubernetes solution like AWS Fargate, best suited for stateless REST api microservice
  - coupled with an application Load Balancing, possibility to scale up and down the number of tasks in the cluster based on metrics, to handle payment request peaks gracefully 
- set up CI/CD with AWS Code pipeline
- use RDS as a managed SQL service, which offers automatic backups, maintenance, upgrades, ...
- use OpenSearch to query logs and build dashboards
- use AWS Key management store to store secrets for connection strings
- use AWS Cognito to handle Authentication, offering a lots of options (social login out of the box)