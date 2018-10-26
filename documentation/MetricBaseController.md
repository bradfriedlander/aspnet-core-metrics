# Metric Base Controller
Any controller that modifies [Metric](./Metric.md) content directly or indirectly needs to inherit this base controller.
Using the [MetricDetails](./MetricDetails.md) attribute implicitly requires this base controller be inherited.

The base controller provides the following capabilities and services.
- Save the [Metric](./Metric.md) instance passed to the inheriting controller.
- When an action is finished executing and the model state is invalid, the validation errors are places in the
[ExceptionMessage](./MagenicMetrics.md#magenicmetricsmetricexceptionmessage).
- Provides the [SetMetricDetails](./MagenicMetrics.md#magenicmetricscontrollersmetricsbasecontrollersetmetricdetailssystemobject) 
to set a value for [Details](./MagenicMetrics.md#magenicmetricsimetricdetails).
