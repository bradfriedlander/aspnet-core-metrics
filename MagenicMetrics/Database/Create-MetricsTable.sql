CREATE TABLE [dbo].[Metrics]
(
	[Application] [nvarchar](64) NOT NULL,
	[Details] [nvarchar](max) NULL,
	[ElpasedTime] [int] NOT NULL,
	[ExceptionMessage] [nvarchar](max) NULL,
	[MetricId] [int] IDENTITY(1,1) NOT NULL,
	[Query] [nvarchar](128) NOT NULL,
	[RequestPath] [nvarchar](128) NOT NULL,
	[ResultCode] [int] NOT NULL,
	[ResultCount] [int] NOT NULL,
	[ServerName] [nvarchar](64) NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[TraceId] [nvarchar](64) NOT NULL,
	[UserName] [nvarchar](64) NOT NULL,
	CONSTRAINT [PK_Metrics] PRIMARY KEY CLUSTERED 
	(
		[MetricId] ASC
	)
)
GO
