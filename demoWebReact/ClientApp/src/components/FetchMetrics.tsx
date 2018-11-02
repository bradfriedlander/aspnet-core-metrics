import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface FetchMetricsDataState {
    metricList: MetricData[];
    loading: boolean;
    pageSize: number;
    pageNumber: number;
    applicationFilter: string;
    recordCount: number;
}

export class FetchMetrics extends React.Component<RouteComponentProps<{}>, FetchMetricsDataState> {
    constructor(props) {
        super(props);
        this.state = { loading: true, pageSize: 10, pageNumber: 1, applicationFilter: '', recordCount: 0, metricList: [] };
        this.handleQuery = this.handleQuery.bind(this);
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
            <br />
            <hr />
            <br />
            {metricsTable}
        </div>;
    }
    private getMetrics() {
        //this.state = { loading: true, pageSize: 10, pageNumber: 1, applicationFilter: '', metricList: new MetricData[0] };
    }

    private handleQuery(event) {
        event.preventDefault();
        const data = new FormData(event.target);
    }

    private renderQueryForm() {
        return <div>
            <form onSubmit={this.handleQuery} >
                <div className="form-group row">
                    <div className="form-group col-md-4">
                        <label asp-for="ApplicationFilter"></label>
                        <input className="form-control" asp-for="ApplicationFilter" />
                        <span asp-validation-for="ApplicationFilter"></span>
                    </div>
                    <div className="form-group col-md-2">
                        <label asp-for="PageNumber"></label>
                        <input className="form-control" asp-for="PageNumber" />
                        <span asp-validation-for="PageNumber"></span>
                    </div>
                    <div className="form-group col-md-2">
                        <label asp-for="PageSize"></label>
                        <input className="form-control" asp-for="PageSize" />
                        <span asp-validation-for="PageSize"></span>
                    </div>
                    <div className="form-group col-md-2">
                        <label>&nbsp;</label><br />
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
                            Metric Id
                </th>
                        <th>
                            StartTime
                </th>
                        <th>
                            Application
                </th>
                        <th>
                            Details
                </th>
                        <th>
                            Elapsed Time
                </th>
                        <th>
                            Exception Message
                </th>
                        <th>
                            Request Method
                </th>
                        <th>
                            Request Path
                </th>
                        <th>
                            Query
                </th>
                        <th>
                            Result Code
                </th>
                        <th>
                            Result Count
                </th>
                        <th>
                            Server Name
                </th>
                        <th>
                            TraceId)
                </th>
                        <th>
                            UserName)
                </th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.metricList.map(metric =>
                        <tr>
                            <td>
                                {metric.MetricId}
                            </td>
                            <td>
                                {metric.StartTime.toISOString()}
                            </td>
                            <td>
                                {metric.Application}
                            </td>
                            <td>
                                {metric.Details}
                            </td>
                            <td>
                                {metric.ElapsedTime}
                            </td>
                            <td>
                                {metric.ExceptionMessage}
                            </td>
                            <td>
                                {metric.RequestMethod}
                            </td>
                            <td>
                                {metric.RequestPath}
                            </td>
                            <td>
                                {metric.Query}
                            </td>
                            <td>
                                {metric.ResultCode}
                            </td>
                            <td>
                                {metric.ResultCount}
                            </td>
                            <td>
                                {metric.ServerName}
                            </td>
                            <td>
                                {metric.TraceId}
                            </td>
                            <td>
                                {metric.UserName}
                            </td>
                        </tr>
                    )
                    }
                </tbody>
            </table>
        </div>;
    }
}

export class MetricData {
    Application: string;
    Details: string;
    ElapsedTime: number;
    ExceptionMessage: string;
    MetricId: number;
    Query: string;
    RequestMethod: string;
    RequestPath: string;
    ResultCode: number;
    ResultCount: number;
    ServerName: string;
    StartTime: Date;
    TraceId: string;
    UserName: string;
}
