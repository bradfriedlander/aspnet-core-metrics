# Magenic Metrics
This is the read me file for this project.

## Overview
Magenic Metric is a middleware component for use in ASP.NET Core MVC/Web API applications.
It's purpose is to provide a standard mechanism for [capturing](#metric-middleware) and [logging](#metric-service) 
(to a [database](#database)) consistent information about each request (*i.e.*, controller action usage) to the application.
The information is contained in the [Metric](#metric) class.

This component provides mechanisms for each action to add action-specific information into the [Metric](#metric) record.
To facilitate this, the controller class needs to inherit from the [MetricBaseController](#metric-base-controller).
The component exposes the [MetricDetails attribute](#metric-details) which populates part of the record.

## [Metric](Documentation/Metric.md) {#metric}
This class is the data that is persisted for each MVC controller interaction.

## [MetricService](Documentation/MetricService.md) {#metric-service}
This class provides the ability to persist a Metric to a backing store.

## [MetricMiddleware](Documentation/MetricMiddleware.md) {#metric-middleware}
This class is a middleware pipeline component that manages the Metric information for each HTTP request.

## [MetricBaseController](Documentation/MetricBaseController.md) {#metric-base-controller}
This is a base class for all controllers that use [MetricMiddleware](#metric-middleware).

## [MetricDetails](Documentation/MetricDetails.md) {#metric-details}
This is an attribute that supports populating the "Details" property of the [Metric](#metric) class.
The actual population is done in the [MetricBaseController](#metric-base-controller).

## [Database](Documentation/Database.md) {#database}
This is a folder that contains scripts to create the database artifacts used by Magenic Metrics. This is an alternative to using the `dotnet ef` commands.

# [Code](Documentation/MagenicMetrics.md)
This document provides details on the classes, methods, and properties in the MagenicMetrics assembly.

# demoWebApp
This is a simple MVC project that demonstrates how Magenic Metrics works for controllers that serve a user interface.

# demoWebApi
This is a simple MVC Web API projects that demonstrates how Magenic metrics works for API controllers.

