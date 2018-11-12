import * as React from 'react';
export class FetchMetrics extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: true,
            pageSize: 10,
            oldPageSize: 10,
            pageNumber: 1,
            applicationFilter: '',
            recordCount: 0,
            metricList: []
        };
        this.handleQuery = this.handleQuery.bind(this);
        this.onChangeApplicationFilter = this.onChangeApplicationFilter.bind(this);
        this.onChangePageNumber = this.onChangePageNumber.bind(this);
        this.onChangePageSize = this.onChangePageSize.bind(this);
        this.onLeavePageSize = this.onLeavePageSize.bind(this);
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
            React.createElement("hr", null),
            metricsTable);
    }
    getMetrics() {
        const metricQuery = JSON.stringify({
            pageSize: this.state.pageSize,
            pageNumber: this.state.pageNumber,
            applicationFilter: this.state.applicationFilter
        });
        fetch('api/Metrics/Get', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: metricQuery,
        })
            .then(this.handleErrors)
            .then(response => response.json())
            .then(data => {
            this.setState({
                metricList: data.metrics,
                loading: false,
                recordCount: data.metrics.length,
                applicationFilter: this.state.applicationFilter,
                pageSize: this.state.pageSize,
                pageNumber: this.state.pageNumber
            });
        });
    }
    handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }
    handleQuery(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        this.getMetrics();
    }
    onChangePageSize(event) {
        if (event.target.value < 1) {
            this.setState({ pageSize: 10 });
        }
        else {
            this.setState({ pageSize: event.target.value });
        }
    }
    onLeavePageSize(event) {
        console.log(event.target.value);
        if (this.state.pageSize === this.state.oldPageSize) {
            return;
        }
        this.setState({ oldPageSize: this.state.pageSize });
        this.getMetrics();
        this.forceUpdate();
    }
    onChangePageNumber(event) {
        if (event.target.value < 1) {
            this.setState({ pageNumber: 1 });
        }
        else {
            this.setState({ pageNumber: event.target.value });
        }
        this.getMetrics();
        this.forceUpdate();
    }
    onChangeApplicationFilter(event) {
        this.setState({ applicationFilter: event.target.value });
    }
    renderQueryForm() {
        return React.createElement("div", null,
            React.createElement("form", { onSubmit: this.handleQuery },
                React.createElement("div", { className: "form-group row" },
                    React.createElement("div", { className: "form-group col-md-4" },
                        React.createElement("label", { className: "control-label" }, "Application Filter"),
                        React.createElement("input", { className: "form-control", name: "applicationFilter", value: this.state.applicationFilter, onChange: this.onChangeApplicationFilter })),
                    React.createElement("div", { className: "form-group col-md-2" },
                        React.createElement("label", { className: "control-label" }, "Page Number"),
                        React.createElement("input", { className: "form-control", name: "pageNumber", type: "number", value: this.state.pageNumber, onChange: this.onChangePageNumber })),
                    React.createElement("div", { className: "form-group col-md-2" },
                        React.createElement("label", { className: "control-label" }, "Page Size"),
                        React.createElement("input", { className: "form-control", name: "pageSize", type: "number", value: this.state.pageSize, onChange: this.onChangePageSize, onBlur: this.onLeavePageSize })),
                    React.createElement("div", { className: "form-group col-md-2" },
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
                        React.createElement("th", null, "Metric\u00A0Id"),
                        React.createElement("th", null, "Start\u00A0Time"),
                        React.createElement("th", null, "Application"),
                        React.createElement("th", null, "Details"),
                        React.createElement("th", null, "Seconds"),
                        React.createElement("th", null, "Exception"),
                        React.createElement("th", null, "Method"),
                        React.createElement("th", null, "Request\u00A0Path"),
                        React.createElement("th", null, "Query"),
                        React.createElement("th", null, "Result\u00A0Code"),
                        React.createElement("th", null, "Result\u00A0Count"),
                        React.createElement("th", null, "Server\u00A0Name"),
                        React.createElement("th", null, "Trace\u00A0Id"),
                        React.createElement("th", null, "User\u00A0Name"))),
                React.createElement("tbody", null, this.state.metricList.map(metric => React.createElement("tr", null,
                    React.createElement("td", null, metric.metricId),
                    React.createElement("td", { className: "col-md-2" }, metric.startTime.toString().slice(0, 23)),
                    React.createElement("td", null, metric.application),
                    React.createElement("td", null, metric.details),
                    React.createElement("td", null, metric.elapsedTime),
                    React.createElement("td", null, metric.exceptionMessage),
                    React.createElement("td", null, metric.requestMethod),
                    React.createElement("td", null, metric.requestPath),
                    React.createElement("td", null, metric.query),
                    React.createElement("td", null, metric.resultCode),
                    React.createElement("td", null, metric.resultCount),
                    React.createElement("td", null, metric.serverName),
                    React.createElement("td", null, metric.traceId),
                    React.createElement("td", null, metric.userName))))));
    }
}
export class MetricPage {
}
export class MetricData {
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/FetchMetrics.js.map