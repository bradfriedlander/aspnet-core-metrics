import * as React from 'react';
export class FetchMetrics extends React.Component {
    constructor(props) {
        super(props);
        this.state = { loading: true, pageSize: 10, pageNumber: 1, applicationFilter: '', recordCount: 0, metricList: [] };
        this.handleQuery = this.handleQuery.bind(this);
        this.getMetrics();
    }
    render() {
        let queryForm = this.renderQueryForm();
        let metricsTable = this.state.recordCount > 0
            ? this.renderMetricsTable(this.state.metricList)
            : this.renderEmptyMetricsTable();
        return React.createElement("div", null,
            React.createElement("hr", null),
            queryForm,
            React.createElement("br", null),
            React.createElement("hr", null),
            React.createElement("br", null),
            metricsTable);
    }
    getMetrics() {
        //this.state = { loading: true, pageSize: 10, pageNumber: 1, applicationFilter: '', metricList: new MetricData[0] };
    }
    handleQuery(event) {
        event.preventDefault();
        const data = new FormData(event.target);
    }
    renderQueryForm() {
        return React.createElement("div", null,
            React.createElement("form", { onSubmit: this.handleQuery },
                React.createElement("div", { className: "form-group row" },
                    React.createElement("div", { className: "form-group col-md-4" },
                        React.createElement("label", { "asp-for": "ApplicationFilter" }),
                        React.createElement("input", { className: "form-control", "asp-for": "ApplicationFilter" }),
                        React.createElement("span", { "asp-validation-for": "ApplicationFilter" })),
                    React.createElement("div", { className: "form-group col-md-2" },
                        React.createElement("label", { "asp-for": "PageNumber" }),
                        React.createElement("input", { className: "form-control", "asp-for": "PageNumber" }),
                        React.createElement("span", { "asp-validation-for": "PageNumber" })),
                    React.createElement("div", { className: "form-group col-md-2" },
                        React.createElement("label", { "asp-for": "PageSize" }),
                        React.createElement("input", { className: "form-control", "asp-for": "PageSize" }),
                        React.createElement("span", { "asp-validation-for": "PageSize" })),
                    React.createElement("div", { className: "form-group col-md-2" },
                        React.createElement("label", null, "\u00A0"),
                        React.createElement("br", null),
                        React.createElement("button", { type: "submit", className: "btn btn-default" }, "Apply Filter")))));
    }
    renderEmptyMetricsTable() {
        return React.createElement("div", null,
            React.createElement("h2", null,
                "Page # ",
                this.state.pageNumber,
                " - ",
                this.state.recordCount,
                " Records"));
    }
    renderMetricsTable(metricsList) {
        return React.createElement("div", null,
            React.createElement("h2", null,
                "Page # ",
                this.state.pageNumber,
                " - ",
                this.state.recordCount,
                " Records"),
            React.createElement("table", null,
                React.createElement("thead", null,
                    React.createElement("tr", null,
                        React.createElement("th", null, "Metric Id"),
                        React.createElement("th", null, "StartTime"),
                        React.createElement("th", null, "Application"),
                        React.createElement("th", null, "Details"),
                        React.createElement("th", null, "Elapsed Time"),
                        React.createElement("th", null, "Exception Message"),
                        React.createElement("th", null, "Request Method"),
                        React.createElement("th", null, "Request Path"),
                        React.createElement("th", null, "Query"),
                        React.createElement("th", null, "Result Code"),
                        React.createElement("th", null, "Result Count"),
                        React.createElement("th", null, "Server Name"),
                        React.createElement("th", null, "TraceId)"),
                        React.createElement("th", null, "UserName)"))),
                React.createElement("tbody", null, this.state.metricList.map(metric => React.createElement("tr", null,
                    React.createElement("td", null, metric.MetricId),
                    React.createElement("td", null, metric.StartTime.toISOString()),
                    React.createElement("td", null, metric.Application),
                    React.createElement("td", null, metric.Details),
                    React.createElement("td", null, metric.ElapsedTime),
                    React.createElement("td", null, metric.ExceptionMessage),
                    React.createElement("td", null, metric.RequestMethod),
                    React.createElement("td", null, metric.RequestPath),
                    React.createElement("td", null, metric.Query),
                    React.createElement("td", null, metric.ResultCode),
                    React.createElement("td", null, metric.ResultCount),
                    React.createElement("td", null, metric.ServerName),
                    React.createElement("td", null, metric.TraceId),
                    React.createElement("td", null, metric.UserName))))));
    }
}
export class MetricData {
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/FetchMetrics.js.map