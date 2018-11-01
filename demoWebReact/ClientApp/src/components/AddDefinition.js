import * as React from 'react';
import { DefinitionData } from './FetchDefinition';
export class AddDefinition extends React.Component {
    constructor(props) {
        super(props);
        this.state = { title: "", loading: true, definitionData: new DefinitionData };
        var definitionId = props.match.params["definitionid"];
        if (definitionId > 0) {
            // This will set state for Edit employee
            fetch('api/Definitions/Details/' + definitionId)
                .then(response => response.json())
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
    render() {
        let contents = this.state.loading
            ? React.createElement("p", null,
                React.createElement("em", null, "Loading..."))
            : this.renderCreateForm();
        return React.createElement("div", null,
            React.createElement("h1", null, "Definition"),
            React.createElement("hr", null),
            contents);
    }
    handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }
    handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);
        const defintionData = JSON.stringify({ definitionId: data.get("definitionId"), name: data.get("name"), isDeleted: false });
        if (this.state.title === "Edit") {
            // PUT request for Edit employee.
            fetch('api/Definitions/Update', {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: defintionData,
            })
                .then(this.handleErrors)
                .then((response) => response.json())
                .then((data) => {
                this.props.history.push("/fetchdefinition");
            });
        }
        else {
            // POST request to create definition.
            fetch('api/Definitions/Create', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: defintionData,
            })
                .then(this.handleErrors)
                .then((response) => response.json())
                .then((data) => {
                this.props.history.push("/fetchdefinition");
            });
        }
    }
    // This will handle Cancel button click event.
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/fetchdefinition");
    }
    // Returns the HTML Form to the render() method.
    renderCreateForm() {
        return (React.createElement("form", { onSubmit: this.handleSave },
            React.createElement("div", { className: "form-group row" },
                React.createElement("input", { type: "hidden", name: "definitionId", value: this.state.definitionData.definitionId })),
            React.createElement("div", { className: "form-group row" },
                React.createElement("label", { className: " control-label col-md-12", htmlFor: "Name" }, "Name"),
                React.createElement("div", { className: "col-md-4" },
                    React.createElement("input", { className: "form-control", type: "text", name: "name", defaultValue: this.state.definitionData.name, required: true }))),
            React.createElement("div", { className: "form-group" },
                React.createElement("button", { type: "submit", className: "btn btn-default" }, "Save"),
                React.createElement("button", { className: "btn", onClick: this.handleCancel }, "Cancel"))));
    }
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/AddDefinition.js.map