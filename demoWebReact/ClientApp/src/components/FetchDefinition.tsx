import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';

interface FetchDefinitionDataState {
    definitionList: DefinitionData[];
    loading: boolean;
}

export class FetchDefinition extends React.Component<RouteComponentProps<{}>, FetchDefinitionDataState> {
    constructor(props) {
        super(props);
        this.state = { definitionList: [], loading: true };
        this.getAll();
        this.handleDelete = this.handleDelete.bind(this);
        this.handleUndelete = this.handleUndelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderEmployeeTable(this.state.definitionList);
        return <div>
            <h1>Definition Data</h1>
            <p>This component demonstrates fetching definition data from the server.</p>
            <p>
                <Link to="/adddefinition">Create New</Link>
            </p>
            {contents}
        </div>;
    }

    private getAll() {
        fetch('api/Definitions/GetAll')
            .then(response => response.json() as Promise<DefinitionData[]>)
            .then(data => {
                this.setState({ definitionList: data, loading: false });
            });
    }

    private handleDelete(id: number) {
        fetch('api/Definitions/Delete/' + id, {
            method: 'delete'
        })
            .then(data => {
                this.setState(
                    {
                        //definitionList: this.state.definitionList.filter((rec) => {
                        //    return (rec.definitionId != id);
                        //};
                        definitionList: this.state.definitionList
                    });
            });
        this.getAll();
    }

    private handleEdit(id: number) {
        this.props.history.push("/definition/edit/" + id);
    }

    private handleUndelete(id: number) {
        fetch('api/Definitions/Undelete/' + id, {
            method: 'put'
        })
            .then(data => {
                this.setState(
                    {
                        //definitionList: this.state.definitionList.filter((rec) => {
                        //    return (rec.definitionId != id);
                        //};
                        definitionList: this.state.definitionList
                    });
            });
        this.getAll();
    }

    private renderEmployeeTable(definitionList: DefinitionData[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>EmployeeId</th>
                    <th>Name</th>
                    <th>Is Deleted</th>
                </tr>
            </thead>
            <tbody>
                {definitionList.map(definition =>
                    <tr key={definition.definitionId}>
                        <td></td>
                        <td>{definition.definitionId}</td>
                        <td>{definition.name}</td>
                        <td><input type="checkbox" checked={definition.isDeleted} /></td>
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(definition.definitionId)}>Edit</a>  |
                            &nbsp;{this.renderDeleteLink(definition.definitionId, definition.isDeleted)}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    private renderDeleteLink(definitionId: number, isDeleted: boolean) {
        return isDeleted
            ? <a className="action" onClick={(id) => this.handleUndelete(definitionId)}>Undelete</a>
            : <a className="action" onClick={(id) => this.handleDelete(definitionId)}>Delete</a>
    }
}

export class DefinitionData {
    definitionId: number = 0;
    name: string = "";
    isDeleted: boolean = false;
}
