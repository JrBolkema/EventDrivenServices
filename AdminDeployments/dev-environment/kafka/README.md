# Kafka

## Broker

At Port `9092`

## Connect

At Port `8083`

Check:

```
http://localhost:8083/connector-plugins
```

> Note: The `Dockerfile-MongoConnect` installs the `mongodb/kafka-connect-mongodb` package.

You can connect a terminal the the `mongo-shell` container.

The container mounts a local volume from the `.\connect-files` into the root of the container.

In the container terminal:

```bash
cd /connect-files
```

You can POST new connectors using the file(s) like this:

```bash
curl -X POST -H "Content-Type: application/json" --data @source-connector-offerings.json  http://connect:8083/connectors
curl -X POST -H "Content-Type: application/json" --data @source-connector-courses.json  http://connect:8083/connectors
```

Sink connectors

```bash
curl -X POST -H "Content-Type: application/json" --data @sink-connector-webpresence-courses.json  http://connect:8083/connectors
curl -X POST -H "Content-Type: application/json" --data @sink-connector-webpresence-offerings.json  http://connect:8083/connectors
```

### Additionally

You can interact with the _Connect_ API from Postman or whatever:

Get a list of connectors:

```

GET http://localhost:8083/connectors

```

Get a specific connector (and status)

```

GET http://localhost:8083/connectors/course
GET http://localhost:8083/connectors/course/status

```

Delete a specific connector:

```

DELETE http://localhost:8083/connectors/course

```

## Schema Registry

At Port `8081`

Check:

```
http://localhost:8081/schemas
```

## Control Center

The Web UI

At Port `9021`

Open Browser to: [Control Center](http://localhost:9021/clusters)
