# MagenicMetrics

This namespace is the root for all classes in Magenic Metrics.

This namespace contains the following namespaces.

| Namespace | Usage |
|-----|------|
| MagenicMetrics |  This contains three major classes: [MagenicMetrics.Metric](#magenicmetricsmetric); [MagenicMetrics.MetricMiddleware](#magenicmetricsmetricmiddleware); and [MagenicMetrics.MetricService](#magenicmetricsmetricservice). It also contains the interfaces for those classes and some supporting classes.  |
| MagenicMetrics.Controllers | This contains the [MagenicMetrics.Controllers.MetricsBaseController](#magenicmetricscontrollersmetricsbasecontroller) class that can be used by application controllers. |
| MagenicMetrics.Filters |  This contains the [MagenicMetrics.Filters.MetricDetailsAttribute](#magenicmetricsfiltersmetricdetailsattribute) attribute that supports populating [MagenicMetrics.Metric](#magenicmetricsmetric).  |

## MagenicMetrics.Controllers.MetricsBaseController

This is a base class for all controllers that use [MagenicMetrics.MetricMiddleware](#magenicmetricsmetricmiddleware).

`Microsoft.AspNetCore.Mvc.Controller`

---

### MagenicMetrics.Controllers.MetricsBaseController.#ctor(MagenicMetrics.IMetric)

Initializes a new instance of the [MagenicMetrics.Controllers.MetricsBaseController](#magenicmetricscontrollersmetricsbasecontroller) class.

| Parameter | Description |
|-----|------|
| metric | The metric. |

---

### MagenicMetrics.Controllers.MetricsBaseController.MetricDetails

Gets the [MagenicMetrics.IMetric](#magenicmetricsimetric) Details.

**Value:** This is the [MagenicMetrics.IMetric](#magenicmetricsimetric) Details.

---

### MagenicMetrics.Controllers.MetricsBaseController._metric

This is the current [MagenicMetrics.IMetric](#magenicmetricsimetric) instance.

---

### MagenicMetrics.Controllers.MetricsBaseController.OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext)

Called after the action method is invoked.

| Parameter | Description |
|-----|------|
| context | The action executed context. |

---

### MagenicMetrics.Controllers.MetricsBaseController.SetMetricDetails(System.Object)

This method sets the [MagenicMetrics.IMetric](#magenicmetricsimetric) details.

| Parameter | Description |
|-----|------|
| details | This is the details to serialize into [MagenicMetrics.IMetric.Details](#magenicmetricsimetricdetails). |

This method can be invoked from any action that inherits from [MagenicMetrics.Controllers.MetricsBaseController](#magenicmetricscontrollersmetricsbasecontroller).

Since [MagenicMetrics.Filters.MetricDetailsAttribute](#magenicmetricsfiltersmetricdetailsattribute) uses this method, it must be `internal` instead of `protected`.

---

### MagenicMetrics.Controllers.MetricsBaseController.GetValidationErrors(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext)

This method gets the concatenated validation errors.

**Returns:** This is the concatenated validation errors with "|" uses as a separator.

---

### MagenicMetrics.Controllers.MetricsBaseController.SerializeDetails

This method sets the value of [MagenicMetrics.IMetric.Details](#magenicmetricsimetricdetails).

This method takes no action if [MagenicMetrics.Controllers.MetricsBaseController.MetricDetails](#magenicmetricscontrollersmetricsbasecontrollermetricdetails) is `null` or [MagenicMetrics.IMetric.Details](#magenicmetricsimetricdetails) already has a value.

If [MagenicMetrics.Controllers.MetricsBaseController.MetricDetails](#magenicmetricscontrollersmetricsbasecontrollermetricdetails) is a `System.String` or `System.Type.IsPrimitive`, [MagenicMetrics.IMetric.Details](#magenicmetricsimetricdetails) is set to a simple string representation of [MagenicMetrics.Controllers.MetricsBaseController.MetricDetails](#magenicmetricscontrollersmetricsbasecontrollermetricdetails).

Otherwise, [MagenicMetrics.IMetric.Details](#magenicmetricsimetricdetails) is set to a JSON serialization of [MagenicMetrics.Controllers.MetricsBaseController.MetricDetails](#magenicmetricscontrollersmetricsbasecontrollermetricdetails).

---

## MagenicMetrics.Filters.MetricDetailsAttribute

This class defines a filter for setting the object to be serialized into [MagenicMetrics.IMetric.Details](#magenicmetricsimetricdetails).

`System.Attribute``Microsoft.AspNetCore.Mvc.Filters.IActionFilter`

---

### MagenicMetrics.Filters.MetricDetailsAttribute.Source

Gets or sets the source identifier.

**Value:**  This is the source identifier. It is the name of the action parameter that gets serialized into [MagenicMetrics.IMetric.Details](#magenicmetricsimetricdetails). 

---

### MagenicMetrics.Filters.MetricDetailsAttribute.OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext)

Called after the action executes, before the action result.

| Parameter | Description |
|-----|------|
| context | The `Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext`. |

This method does nothing.

---

### MagenicMetrics.Filters.MetricDetailsAttribute.OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext)

Called before the action executes, after model binding is complete.

| Parameter | Description |
|-----|------|
| context | This is the `Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext` context. |

**Exception:** [System.ArgumentOutOfRangeException(System.ArgumentOutOfRangeException)]: Source

This method invokes [MagenicMetrics.Controllers.MetricsBaseController.SetMetricDetails(System.Object)](#magenicmetricscontrollersmetricsbasecontrollersetmetricdetails(systemobject)) to save the object identified by [MagenicMetrics.Filters.MetricDetailsAttribute.Source](#magenicmetricsfiltersmetricdetailsattributesource).

---

## MagenicMetrics.IMetric

This is the information persisted for every MVC action.

---

### MagenicMetrics.IMetric.Application

Gets or sets the name of current application.

**Value:** This is the name of the application.

---

### MagenicMetrics.IMetric.Details

Gets or sets the action provided details.

**Value:** This is the details. It should be a serialized JSOB object.

---

### MagenicMetrics.IMetric.ElapsedTime

Gets or sets the elapsed time in milliseconds.

**Value:** This is the elapsed time for the request.

---

### MagenicMetrics.IMetric.ExceptionMessage

Gets or sets the exception message.

**Value:** If an exception occurs, this is the exception message. Otherwise, it is a pipe delimited collection of validation messages.

---

### MagenicMetrics.IMetric.MetricId

Gets or sets the identity identifier for the metric instance.

**Value:** This is the identity identifier for the metric.

---

### MagenicMetrics.IMetric.Query

Gets or sets the query.

**Value:** The query.

---

### MagenicMetrics.IMetric.RequestMethod

Gets or sets the request method.

**Value:** The request method.

---

### MagenicMetrics.IMetric.RequestPath

Gets or sets the request path.

**Value:** The request path.

---

### MagenicMetrics.IMetric.ResultCode

Gets or sets the result code.

**Value:** The result code.

---

### MagenicMetrics.IMetric.ResultCount

Gets or sets the result count.

**Value:** This is the result count. It should be set to the number of items processed by the request.

---

### MagenicMetrics.IMetric.ServerName

Gets or sets the name of the server instance that responded to a request.

**Value:** The name of the server instance.

---

### MagenicMetrics.IMetric.StartTime

Gets or sets the UTC start time.

**Value:** This is the UTC start time.

---

### MagenicMetrics.IMetric.TraceId

Gets or sets the trace identifier.

**Value:** The trace identifier.

---

### MagenicMetrics.IMetric.UserName

Gets or sets the name of the user.

**Value:** The name of the user.

---

## MagenicMetrics.IMetricService

This service provides the persistence for [MagenicMetrics.IMetric](#magenicmetricsimetric) objects.

The application that uses this is responsible for providing the database that will serve as the persistence store and a table that contains columns for all of the properties defined in [MagenicMetrics.IMetric](#magenicmetricsimetric).

---

### MagenicMetrics.IMetricService.AddAndSave(MagenicMetrics.IMetric)

This method adds an instance of `metric` to the persistence store and commits the addition.

| Parameter | Description |
|-----|------|
| metric | The metric to be added to the persistence store. |

**Returns:** This is the number of entries written to the persistent store.

---

### MagenicMetrics.IMetricService.GetLatest(System.Int32,System.Int32,System.String)

This method gets the most recent [MagenicMetrics.IMetric](#magenicmetricsimetric) records based on the provided filters.

| Parameter | Description |
|-----|------|
| pageSize | This is the maximum number of records to retrieve for a given page. |
| pageNumber | This is the page number where `pageSize` is the page size. |
| applicationFilter | This is the filter for the application name. |

**Returns:** This is the matching collection of the records.

---

## MagenicMetrics.Metric

This is the information persisted for every MVC action.

[MagenicMetrics.IMetric](#magenicmetricsimetric)

---

### MagenicMetrics.Metric.#ctor

Initializes a new instance of the [MagenicMetrics.Metric](#magenicmetricsmetric) class.

The constructor initializes [MagenicMetrics.Metric.StartTime](#magenicmetricsmetricstarttime) to the current UTC date/time. It also sets the `System.Int32` properties to `-1` to distinguish a never set condition from the default value of `0`

---

### MagenicMetrics.Metric.Application

Gets or sets the name of current application.

**Value:** This is the name of the application.

---

### MagenicMetrics.Metric.Details

Gets or sets the action provided details.

**Value:** This is the details. It should be a serialized JSOB object.

---

### MagenicMetrics.Metric.ElapsedTime

Gets or sets the elapsed time in milliseconds.

**Value:** This is the elapsed time for the request.

---

### MagenicMetrics.Metric.ExceptionMessage

Gets or sets the exception message.

**Value:** If an exception occurs, this is the exception message. Otherwise, it is a pipe delimited collection of validation messages.

---

### MagenicMetrics.Metric.MetricId

Gets or sets the identity identifier for the metric instance.

**Value:** This is the identity identifier for the metric.

---

### MagenicMetrics.Metric.Query

Gets or sets the query.

**Value:** The query.

---

### MagenicMetrics.Metric.RequestMethod

Gets or sets the request method.

**Value:** The request method.

---

### MagenicMetrics.Metric.RequestPath

Gets or sets the request path.

**Value:** The request path.

---

### MagenicMetrics.Metric.ResultCode

Gets or sets the result code.

**Value:** The result code.

---

### MagenicMetrics.Metric.ResultCount

Gets or sets the result count.

**Value:** This is the result count. It should be set to the number of items processed by the request.

---

### MagenicMetrics.Metric.ServerName

Gets or sets the name of the server instance that responded to a request.

**Value:** The name of the server instance.

---

### MagenicMetrics.Metric.StartTime

Gets or sets the UTC start time.

**Value:** This is the UTC start time.

---

### MagenicMetrics.Metric.TraceId

Gets or sets the trace identifier.

**Value:** The trace identifier.

---

### MagenicMetrics.Metric.UserName

Gets or sets the name of the user.

**Value:** The name of the user.

---

## MagenicMetrics.MetricMiddleware

This class implements persisting DevOps metric information to a persistent store.

---

### MagenicMetrics.MetricMiddleware.#ctor(Microsoft.AspNetCore.Http.RequestDelegate,Microsoft.Extensions.Logging.ILogger{MagenicMetrics.MetricMiddleware})

Initializes a new instance of the [MagenicMetrics.MetricMiddleware](#magenicmetricsmetricmiddleware) class.

| Parameter | Description |
|-----|------|
| next | The next link in the middleware pipeline. |
| logger | This is the logger. |

---

### MagenicMetrics.MetricMiddleware._logger

This is the logger instance that can be used by this class.

---

### MagenicMetrics.MetricMiddleware._next

This is the next action to be performed in the pipeline. This delegate is called during the processing of the [MagenicMetrics.MetricMiddleware.Invoke(Microsoft.AspNetCore.Http.HttpContext,MagenicMetrics.IMetric,MagenicMetrics.IMetricService)](#magenicmetricsmetricmiddlewareinvoke(microsoftaspnetcorehttphttpcontext,magenicmetricsimetric,magenicmetricsimetricservice)) method.

---

### MagenicMetrics.MetricMiddleware.Invoke(Microsoft.AspNetCore.Http.HttpContext,MagenicMetrics.IMetric,MagenicMetrics.IMetricService)

Invokes the specified HTTP context.

| Parameter | Description |
|-----|------|
| context | This is the HTTP context for the request traversing the middleware pipeline. |
| metric | This is an instance of the metrics that are persisted. |
| metricService |  This is the metric service. It is here (instead of being in the constructor) because middleware components can only have singleton injected objects. This service must be scoped.  |

**Returns:** 

The metric service performs five actions.

1. Populate `metric` with initial values and start time.
1. Invoke the [MagenicMetrics.MetricMiddleware._next](#magenicmetricsmetricmiddleware_next) step of the pipeline.
1. Capture message from any exception that may occur and then re-throw the exception.
1. Always populate the elapsed time and other end of processing information.
1. Persist `metric` using `metricService`.

---

## MagenicMetrics.MetricMiddlewareExtensions

This class contains the standard extension methods for an ASP.NET Core middleware service.

---

### MagenicMetrics.MetricMiddlewareExtensions.AddMetrics(Microsoft.Extensions.DependencyInjection.IServiceCollection)

This methods adds all classes used by [MagenicMetrics.MetricMiddleware](#magenicmetricsmetricmiddleware) to the service collection.

| Parameter | Description |
|-----|------|
| services | This is the current services collection. |

**Returns:** This is the updated services collection.

---

### MagenicMetrics.MetricMiddlewareExtensions.UseMetrics(Microsoft.AspNetCore.Builder.IApplicationBuilder)

This method performs all of the actions needed to add [MagenicMetrics.MetricMiddleware](#magenicmetricsmetricmiddleware) to the HTTP request processing pipeline.

| Parameter | Description |
|-----|------|
| app | This is the current application pipeline. |

**Returns:** This is the updated HTTP request processing pipeline.

---

## MagenicMetrics.MetricService

This service provides the persistence for [MagenicMetrics.IMetric](#magenicmetricsimetric) objects.

The application that uses this is responsible for providing the database that will serve as the persistence store and a table that contains columns for all of the properties defined in [MagenicMetrics.IMetric](#magenicmetricsimetric).

`Microsoft.EntityFrameworkCore.DbContext`[MagenicMetrics.IMetricService](#magenicmetricsimetricservice)

---

### MagenicMetrics.MetricService.#ctor(Microsoft.Extensions.Options.IOptions{MagenicMetrics.MetricServiceOptions},Microsoft.Extensions.Logging.ILogger{MagenicMetrics.MetricService},Microsoft.EntityFrameworkCore.DbContextOptions{MagenicMetrics.MetricService})

Initializes a new instance of the [MagenicMetrics.MetricService](#magenicmetricsmetricservice) class.

| Parameter | Description |
|-----|------|
| options | These are the service configuration options. |
| logger | This is the logger. |
| contextOptions | This is the options for `Microsoft.EntityFrameworkCore.DbContext`. |

---

### MagenicMetrics.MetricService.Metrics

Gets or sets the `Microsoft.EntityFrameworkCore.DbSet`1` for [MagenicMetrics.Metric](#magenicmetricsmetric).

**Value:** This is the `Microsoft.EntityFrameworkCore.DbSet`1` for [MagenicMetrics.Metric](#magenicmetricsmetric).

---

### MagenicMetrics.MetricService._logger

This is the logger instance passed to the constructor.

---

### MagenicMetrics.MetricService._options

This is the configuration options passed to the constructor.

---

### MagenicMetrics.MetricService.AddAndSave(MagenicMetrics.IMetric)

This method adds an instance of `metric` to the persistence store and commits the addition.

| Parameter | Description |
|-----|------|
| metric | The metric to be added to the persistence store. |

**Returns:** This is the number of entries written to the persistent store.

---

### MagenicMetrics.MetricService.GetLatest(System.Int32,System.Int32,System.String)

This method gets the most recent [MagenicMetrics.IMetric](#magenicmetricsimetric) records based on the provided filters.

| Parameter | Description |
|-----|------|
| pageSize | This is the maximum number of records to retrieve for a given page. |
| pageNumber | This is the page number where `pageSize` is the page size. |
| applicationFilter | This is the filter for the application name. |

**Returns:** This is the matching collection of the records.

This method support inclusive and exclusive filtering for `applicationFilter`. If `applicationFilter` begins with "!", the filtering is exclusive; otherwise, it is inclusive. In either case, the balance of the filter value has an implicit wild card at the beginning and end.

---

### MagenicMetrics.MetricService.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)

Override this method to further configure the model that was discovered by convention from the entity types exposed in `Microsoft.EntityFrameworkCore.DbSet`1` properties on your derived context. The resulting model may be cached and re-used for subsequent instances of your derived context.

| Parameter | Description |
|-----|------|
| modelBuilder |  The builder being used to construct the model for this context. Databases (and other extensions) typically define extension methods on this object that allow you to configure aspects of the model that are specific to a given database.  |

If a model is explicitly set on the options for this context (via `Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)`) then this method will not be run.

The name of the table containing the [MagenicMetrics.Metric](#magenicmetricsmetric) records is define by the [MagenicMetrics.MetricServiceOptions.TableName](#magenicmetricsmetricserviceoptionstablename) in [MagenicMetrics.MetricService._options](#magenicmetricsmetricservice_options).

---

## MagenicMetrics.MetricServiceOptions

This class contains the options for configuring [MagenicMetrics.MetricService](#magenicmetricsmetricservice).

---

### MagenicMetrics.MetricServiceOptions.MetricServiceConnection

Gets or sets the connection string for the database containing the [MagenicMetrics.MetricServiceOptions.TableName](#magenicmetricsmetricserviceoptionstablename) table.

**Value:** The connection string.

---

### MagenicMetrics.MetricServiceOptions.TableName

Gets or sets the name of the table that contains the properties for [MagenicMetrics.IMetric](#magenicmetricsimetric).

**Value:** This is the name of the table.

---

