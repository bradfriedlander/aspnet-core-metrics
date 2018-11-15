import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import { uniqueId } from 'react-html-id';

interface FetchMetricsDataState {
    metricList: MetricData[];
    loading: boolean;
    pageSize: number;
    oldApplicationFilter: string;
    oldPageSize: number;
    pageNumber: number;
    applicationFilter: string;
    recordCount: number;
}

export class FetchMetrics extends React.Component<RouteComponentProps<{}>, FetchMetricsDataState> {
    constructor(props) {
        super(props);
        //uniqueId.enableUniqueIds(this);
        this.state = {
            loading: true,
            pageSize: 10,
            oldPageSize: 10,
            pageNumber: 1,
            applicationFilter: '',
            oldApplicationFilter: '',
            recordCount: 0,
            metricList: []
        };
        this.handleQuery = this.handleQuery.bind(this);
        this.onChangeApplicationFilter = this.onChangeApplicationFilter.bind(this);
        this.onChangePageNumber = this.onChangePageNumber.bind(this);
        this.onChangePageSize = this.onChangePageSize.bind(this);
        this.onLeaveApplicationFilter = this.onLeaveApplicationFilter.bind(this);
        this.onLeavePageSize = this.onLeavePageSize.bind(this);
        this.getMetrics();
    }

    public render() {
        let queryForm = this.renderQueryForm();
        let metricsTable = this.state.recordCount > 0
            ? this.renderMetricsTable(this.state.metricList)
            : this.renderEmptyMetricsTable();
        return <div>
            <hr />
            {queryForm}
            <hr />
            {metricsTable}
        </div>;
    }
    private getMetrics() {
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
            .then(response => response.json() as Promise<MetricPage>)
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

    private handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }

    private handleQuery(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        this.getMetrics();
    }

    private onChangeApplicationFilter(event) {
        this.setState({ applicationFilter: event.target.value });
    }

    private onChangePageNumber(event) {
        if (event.target.value < 1) {
            this.setState({ pageNumber: 1 });
        } else {
            this.setState({ pageNumber: event.target.value });
        }
        this.getMetrics();
        this.forceUpdate();
    }

    private onChangePageSize(event) {
        if (event.target.value < 1) {
            this.setState({ pageSize: 10 });
        } else {
            this.setState({ pageSize: event.target.value });
        }
    }

    private onLeaveApplicationFilter(event) {
        console.log(event.target.value);
        if (this.state.applicationFilter === this.state.oldApplicationFilter) {
            return;
        }
        this.setState({ oldApplicationFilter: this.state.applicationFilter });
        this.getMetrics();
        this.forceUpdate();
    }

    private onLeavePageSize(event) {
        console.log(event.target.value);
        if (this.state.pageSize === this.state.oldPageSize) {
            return;
        }
        this.setState({ oldPageSize: this.state.pageSize });
        this.getMetrics();
        this.forceUpdate();
    }

    private renderQueryForm() {
        return <div>
            <form onSubmit={this.handleQuery} >
                <div className="form-group row">
                    <div className="form-group col-md-4">
                        <label htmlFor="id-1" className="control-label">Application Filter</label>
                        <input id="id-1" className="form-control" name="applicationFilter" value={this.state.applicationFilter}
                            onChange={this.onChangeApplicationFilter}
                            onBlur={this.onLeaveApplicationFilter} />
                    </div>
                    <div className="form-group col-md-2">
                        <label className="control-label">Page Number</label>
                        <input className="form-control" name="pageNumber" type="number" value={this.state.pageNumber}
                            onChange={this.onChangePageNumber} />
                    </div>
                    <div className="form-group col-md-2">
                        <label className="control-label">Page Size</label>
                        <input className="form-control" name="pageSize" type="number" value={this.state.pageSize}
                            onChange={this.onChangePageSize}
                            onBlur={this.onLeavePageSize} />
                    </div>
                    <div className="form-group col-md-2">
                        <button type="submit" className="btn btn-default">Apply Filter</button>
                    </div>
                </div>
            </form>
        </div>;
    }

    private renderEmptyMetricsTable() {
        return <div>
            <h2>Page # {this.state.pageNumber} - {this.state.recordCount} Records</h2>
        </div>;
    }

    private renderMetricsTable(metricsList: MetricData[]) {
        return <div>
            <h2>Page # {this.state.pageNumber} - {this.state.recordCount} Records</h2>
            <table>
                <thead>
                    <tr>
                        <th>
                            Metric&nbsp;Id
                </th>
                        <th>
                            Start&nbsp;Time
                </th>
                        <th>
                            Application
                </th>
                        <th>
                            Details
                </th>
                        <th>
                            Seconds
                </th>
                        <th>
                            Exception
                </th>
                        <th>
                            Method
                </th>
                        <th>
                            Request&nbsp;Path
                </th>
                        <th>
                            Query
                </th>
                        <th>
                            Result&nbsp;Code
                </th>
                        <th>
                            Result&nbsp;Count
                </th>
                        <th>
                            Server&nbsp;Name
                </th>
                        <th>
                            Trace&nbsp;Id
                </th>
                        <th>
                            User&nbsp;Name
                </th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.metricList.map(metric =>
                        <tr>
                            <td>
                                {metric.metricId}
                            </td>
                            <td className="col-md-2">
                                {metric.startTime.toString().slice(0, 23)}
                            </td>
                            <td>
                                {metric.application}
                            </td>
                            <td>
                                {metric.details}
                            </td>
                            <td>
                                {metric.elapsedTime}
                            </td>
                            <td>
                                {metric.exceptionMessage}
                            </td>
                            <td>
                                {metric.requestMethod}
                            </td>
                            <td>
                                {metric.requestPath}
                            </td>
                            <td>
                                {metric.query}
                            </td>
                            <td>
                                {metric.resultCode}
                            </td>
                            <td>
                                {metric.resultCount}
                            </td>
                            <td>
                                {metric.serverName}
                            </td>
                            <td>
                                {metric.traceId}
                            </td>
                            <td>
                                {metric.userName}
                            </td>
                        </tr>
                    )
                    }
                </tbody>
            </table>
        </div>;
    }
}

export class MetricPage {
    applicationFilter: string;
    pageSize: number;
    pageNumber: number;
    metrics: MetricData[];
}

export class MetricData {
    application: string;
    details: string;
    elapsedTime: number;
    exceptionMessage: string;
    metricId: number;
    query: string;
    requestMethod: string;
    requestPath: string;
    resultCode: number;
    resultCount: number;
    serverName: string;
    startTime: Date;
    traceId: string;
    userName: string;
}
