# Metric

See [Metric](./MagenicMetrics.md#MagenicMetrics.Metric)

This class mirrors the data that is persisted to the [Database](./Database.md).
Most of the properties are managed by the [MetricMiddleware](./MetricMiddleware.md).

The class can be injected into any controller or controller action.
This gives each action the ability to add additional information that may provide additional DevOps support. The properties that can be set are:
- [ResultCount](./MagenicMetrics.md#magenicmetricsmetricresultcount): This can be used to indicate the number of objects managed by the action.
- [Details](./MagenicMetrics.md#magenicmetricsmetricdetails): This is used to provide any details that may be helpful understanding the usage of the action.
The [MetricDetails](./MetricDetails.md) attribute can be used to populate this property.
- [ExceptionMessage](./MagenicMetrics.md#magenicmetricsmetricexceptionmessage): This is used to capture information about any exception. The default behavior is to store the Message value of the exception.
