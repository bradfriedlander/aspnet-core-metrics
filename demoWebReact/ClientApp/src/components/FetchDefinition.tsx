import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
interface FetchDefinitionDataState {
    definitionList: DefinitionData[];
    loading: boolean;
}
export class FetchEmployee extends React.Component<RouteComponentProps<{}>, FetchDefinitionDataState> {
    constructor() {
        super();
        this.state = { definitionList: [], loading: true };
        fetch('api/Employee/Index')
            .then(response => response.json() as Promise<DefinitionData[]>)
            .then(data => {
                this.setState({ definitionList: data, loading: false });
            });
        // This binding is necessary to make "this" work in the callback  
        this.handleDelete = this.handleDelete.bind(this);
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
    // Handle Delete request for an employee  
    private handleDelete(id: number) {
        if (!confirm("Do you want to delete employee with Id: " + id))
            return;
        else {
            fetch('api/Employee/Delete/' + id, {
                method: 'delete'
            }).then(data => {
                this.setState(
                    {
                        definitionList: this.state.definitionList.filter((rec) => {
                            return (rec.definitionId != id);
                        })
                    });
            });
        }
    }
    private handleEdit(id: number) {
        this.props.history.push("/employee/edit/" + id);
    }
    // Returns the HTML table to the render() method.  
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
                            <a className="action" onClick={(id) => this.handleDelete(definition.definitionId)}>Delete</a>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}
export class DefinitionData {
    definitionId: number = 0;
    name: string = "";
    isDeleted: boolean = false;
}