# Metric Details
[MetricDetails](./MagenicMetrics.md#magenicmetricsfiltersmetricdetailsattribute) is an attribute that can be applied to any controller action entry point.
The attribute has a single parameter, named `Source`, which is the name of the action parameter that gets serialized into the
[Metric](./Metric.md).[Details](./MagenicMetrics.md#magenicmetricsmetricdetails) property.

An example usage of the attribute is shown below.
```csharp
[MetricDetails(Source = "definition")]
public IActionResult Post(DefinitionInput definition)
```

In this example, the object named `definition` is stored in [Metric.Details](./MagenicMetrics.md#magenicmetricsmetricdetails) as a serialized JSON object.
