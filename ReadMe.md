# Magenic Metrics
This is the read me file for this project.
## [Metric](Documentation/Metric.md)
This class is the data that is persisted for each MVC controller interaction.
## [MetricService](Documentation/MetricService.md)
This class provides the ability to persist a Metric to a backing store.
## [MetricMiddleware](Documentation/MetricMiddleware.md)
This class is a middleware pipeline component that manages the Metric information for each HTTP request.
## [MetricBaseController](Documentation/MetricBaseController.md)
This is a base class for all controllers that use MetricMiddleware.
## Database
This is a folder that contains scripts to create the database artifacts used by Magenic Metrics. This is an alternative to using the `dotnet ef` commands.
# demoWebApp
This is a simple MVC project that demonstrates how Magenic Metrics works for controllers that serve a user interface.
# demoWebApi
This is a simple MVC Web API projects that demonstrates how Magenic metrics works for API controllers.
