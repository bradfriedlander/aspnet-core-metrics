import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { DefinitionModel } from '../models/DefinitionModel';

interface FetchDefinitionDataState {
    definitionList: DefinitionModel[];
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
            .then(this.handleErrors)
            .then(response => response.json() as Promise<DefinitionModel[]>)
            .then(data => {
                this.setState({ definitionList: data, loading: false });
            });
    }

    private handleDelete(definition: DefinitionModel) {
        fetch('api/Definitions/Delete/', {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(definition)
        })
            .then(this.handleErrors)
            .then(response => response.json() as Promise<DefinitionModel>)
            .then(data => this.getAll());
    }

    private handleEdit(id: number) {
        this.props.history.push("/definition/edit/" + id);
    }

    private handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }

    private handleUndelete(definition: DefinitionModel) {
        fetch('api/Definitions/Undelete/', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(definition)
        })
            .then(this.handleErrors)
            .then(response => response.json() as Promise<DefinitionModel>)
            .then(data => this.getAll());
    }

    private renderEmployeeTable(definitionList: DefinitionModel[]) {
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
                        <td><input type="checkbox" className="readonly" checked={definition.isDeleted} /></td>
                        <td>
                            <a className="action" onClick={(id) => this.handleEdit(definition.definitionId)}>Edit</a>  |
                            &nbsp;{this.renderDeleteLink(definition)}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }

    private renderDeleteLink(definition: DefinitionModel) {
        return definition.isDeleted
            ? <a className="action" onClick={(id) => this.handleUndelete(definition)}>Undelete</a>
            : <a className="action" onClick={(id) => this.handleDelete(definition)}>Delete</a>
    }
}

export class DefinitionData {
    definitionId: number = 0;
    name: string = "";
    isDeleted: boolean = false;
}
