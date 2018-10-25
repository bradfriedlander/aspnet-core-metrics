# Magenic Metrics
This is the documentation for the Magenic Metrics project.

## Overview
Magenic Metrics is a middleware component for use in ASP.NET Core MVC/Web API applications.
It's purpose is to provide a standard mechanism for [capturing](#MetricMiddleware) and [logging](#MetricService) 
(to a [database](#Database)) consistent information about each request (*i.e.*, controller action usage) to the application.
The information is contained in the [Metric](#Metric) class.

This component provides mechanisms for each action to add action-specific information into the [Metric](#Metric) record.
To facilitate this, the controller class needs to inherit from the [MetricBaseController](#MetricBaseController).
The component exposes the [MetricDetails attribute](#MetricDetails) which populates part of the record.

## [Metric](documentation/Metric.md)
This class is the data that is persisted for each MVC controller interaction.

## [MetricService](documentation/MetricService.md)
This class provides the ability to persist a Metric to a backing store.

## [MetricMiddleware](documentation/MetricMiddleware.md)
This class is a middleware pipeline component that manages the Metric information for each HTTP request.

## [MetricBaseController](documentation/MetricBaseController.md)
This is a base class for all controllers that use [MetricMiddleware](#MetricMiddleware).

## [MetricDetails](documentation/MetricDetails.md)
This is an attribute that supports populating the "Details" property of the [Metric](#Metric) class.
The actual population is done in the [MetricBaseController](#MetricBaseController).

## [Database](documentation/Database.md)
This is a folder that contains scripts to create the database artifacts used by Magenic Metrics.
This is an alternative to using the `dotnet ef` commands.

# [Code](documentation/MagenicMetrics.md)
This document provides details on the classes, methods, and properties in the MagenicMetrics assembly.

# [demoWebApp](documentation/demoWebApp.md)
This is a simple MVC project that demonstrates how Magenic Metrics works for controllers that serve a user interface.

# [demoWebApi](documentation/demoWebApi.md)
This is a simple MVC Web API projects that demonstrates how Magenic metrics works for API controllers.

