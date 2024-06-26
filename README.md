# eShopMicroservices


Developing backend for an e-commerce modules over Product, Basket, Discount and Ordering microservices with Relational databases (SQLite, Sql Server) with communicating over RabbitMQ Event Driven Communication and using Yarp API Gateway. 
You can find Microservices Architecture and Step by Step Implementation on .NET which step by step developing this course with extensive explanations and details.


## Catalog microservice which includes-
- ASP.NET Core Minimal APIs and latest features of .NET 8 and C# 12
- Vertical Slice Architecture implementation with Feature folders
- Cross-cutting concerns Logging, global Exception Handling and Health Checks


## Discount microservice which includes-
- ASP.NET gRPC Server application
- Build a Highly Performant inter-service gRPC Communication with Basket Microservice
- Exposing gRPC Services with creating Protobuf messages
- SQLite database connection and containerization
- Microservices Communication
- Sync inter-service gRPC Communication
- Async Microservices Communication with RabbitMQ Message-Broker Service
- Using RabbitMQ Publish/Subscribe Topic Exchange Model
- Using MassTransit for abstraction over RabbitMQ Message-Broker system
- Publishing BasketCheckout event queue from Basket microservices and Subscribing this event from Ordering microservices
- Create RabbitMQ EventBus.Messages library and add references Microservices

## Ordering Microservice-
- Use Domain Events & Integration Events
- Entity Framework Core Code-First Approach, Migrations, DDD Entity Configurations
- Consuming RabbitMQ BasketCheckout event queue with using MassTransit-RabbitMQ Configuration
- SqlServer database connection and containerization
- Using Entity Framework Core ORM and auto migrate to SqlServer when application startup

## Yarp API Gateway Microservice-
- Implement API Gateways with Yarp Reverse Proxy applying Gateway Routing Pattern
- Yarp Reverse Proxy Configuration; Route, Cluster, Path, Transform, Destinations
- Rate Limiting with FixedWindowLimiter on Yarp Reverse Proxy Configuration
- Sample microservices/containers to reroute through the API Gateways

