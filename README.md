# Event Driven Services

This repo is for the Hypertheory course _Event Driven Services_.

## Repositories

### Docs

This is an ASP.NET MVC Application that serves as the overall documentation for the Hypertheory Domain.

In the `docs` folder, run it with:

```shell
dotnet run
```

It will be on localhost:5005.

### Admin Deployments

> Note: Don't do any of this until instructed, please.

In the spirit of [12 Factor App - Admin Processes](https://12factor.net/admin-processes).

These services provide the "ambient" environment for the Hypertheory Domain. Only a local development environment is provided for this training course, but could serve as a blueprint for creating other (test, staging, prod) environments.

Briefly, they are:

#### MongoDb

A sharded instance of MongoDb for data.

In the `AdminDeployments/dev-environment/mongodb` folder, run:

```shell
docker compose build
```

This will build the images needed for docker compose.

Then run:

```shell
docker compose up -d
```

To start the services.

##### The Services

- `mongodb-sharded` on Port 27017
- `mongodb-shard0` Part of the sharded MongoDB Cluster
- `mongodb-cfg` The configuration server for the MongoDB cluster
- `mongo-setup` A script to setup the MongoDB cluster with some seeded data.
  - note, this will shutdown after it is done doing it's thing. That is _normal_ In production, this would be akin to an `init container` in a Pod.
- `mongo-express` - A development environment _only_ UI for MongoDB, running at port 8090.

The development environment credentials for Mongodb are:

- user: root
- password: TokyoJoe138!

#### Kafka

We are using the Confluent docker images for this training. We are using them under the community license - as part of that, you can only have one broker.

To start the services, in the `AdminDeployments/dev-environment/kafka` folder, run:

```shell
docker compose up -d
```

##### The Services

- `zookeeper` On port 2181
  - Provides configuration for the cluster.
- `broker` on port 9092
  - The Kafka broker
- `connect` on port 8083
  - The Kafka Connect service - for CDC, etc.
- `schema-registry` on port 8081
  - The Kafka Schema Registry service - for storing schemas for the Kafka topics and validating publishing
- `control-center` on port 9021
  - The Confluent web UI for managing a cluster.
- `shell`
  - A shell to interact with the Kafka cluster for setting up CDC, etc.

### WebPresenceBoundedContext

#### `WebPresence`

Our "starter" application.

An ASP.NET MVC application that allows users to create accounts and enroll in training courses.

#### `WebPresenceACL`

> Note: To be created in class.

The anti-corruption layer for the web presence bounded context.

#### `WebPresenceMessages`

A shared library of messages used internally in the web presence bounded context.

### TrainingBoundedContext

> Note: To be created in class.

### CustomersBoundedContext

> Note: To be created in class.

## Other Technolgies Used During Training course

### Project Tye

A Microsoft tool for working with microservices.

[Project Tye Github](https://github.com/dotnet/tye)

### Dapr

A CNCF project that provides a microservice runtime. "Dapr" stands for "Distributed Application Programming Runtime".

[Dapr.io](https://dapr.io)

### Nuget Packages

A variety of Nuget packages will be used (`MongoDb.Driver`, `Confluent.Kafka`, etc.)

One in particular, created for this course: `Hypertheory.KafkaUtils` is a library of utilities for working with Kafka. While based on the guidance of the Confluent Kafka documentation, it should be considered a "work in progress" and not a production ready library.

The nuget package for the Kafka Utils is at https://github.com/HypertheoryTraining/Hypertheory.KafkaUtils

### AsyncAPI

Will be lightly introduced in the class as a tool for designing and documenting asynchronous APIs. It is inspired by OpenAPI.

[https://www.asyncapi.com/](https://www.asyncapi.com/)

We will use the Studio, [Github](https://github.com/asyncapi/studio)

You can run it in Docker with:

```shell
docker run -it -d -p 9333:80 asyncapi/studio
```

And then access it at `localhost:9333`.
