import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { DefinitionData } from './FetchDefinition';

interface AddDefinitionDataState {
    title: string;
    loading: boolean;
    definitionData: DefinitionData;
}

export class AddDefinition extends React.Component<RouteComponentProps<{}>, AddDefinitionDataState> {
    constructor(props) {
        super(props);
        this.state = { title: "", loading: true, definitionData: new DefinitionData };
        var definitionId = props.match.params["definitionid"];
        if (definitionId > 0) {
            // This will set state for Edit employee
            fetch('api/Definitions/Details/' + definitionId)
                .then(response => response.json() as Promise<DefinitionData>)
                .then(data => {
                    this.setState({ title: "Edit", loading: false, definitionData: data });
                });
        }
        else {
            // This will set state for Add employee
            this.state = { title: "Create", loading: false, definitionData: new DefinitionData };
        }
        // This binding is necessary to make "this" work in the callback
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm();
        return <div>
            <h1>Definition</h1>
            <hr />
            {contents}
        </div>;
    }

    // This will handle the submit form event.
    private handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        const defintionData = JSON.stringify({ definitionId: data.get("definitionId"), name: data.get("name"), isDeleted: false });
        // PUT request for Edit employee.
        if (this.state.title === "Edit") {
            fetch('api/Definitions/Update', {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: defintionData,
            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchdefinition");
                })
        }
        // POST request to create definition.
        else {
            fetch('api/Definitions/Create', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: defintionData,
            }).then((response) => response.json())
                .then((responseJson) => {
                    this.props.history.push("/fetchdefinition");
                })
        }
    }

    // This will handle Cancel button click event.
    private handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchdefinition");
    }

    // Returns the HTML Form to the render() method.
    private renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="definitionId" value={this.state.definitionData.definitionId} />
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">Name</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="name" defaultValue={this.state.definitionData.name} required />
                    </div>
                </div >
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div >
            </form >
        )
    }
}
