import * as React from 'react';
import { Link } from 'react-router-dom';
export class FetchDefinition extends React.Component {
    constructor(props) {
        super(props);
        this.state = { definitionList: [], loading: true };
        this.getAll();
        this.handleDelete = this.handleDelete.bind(this);
        this.handleUndelete = this.handleUndelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
    }
    render() {
        let contents = this.state.loading
            ? React.createElement("p", null,
                React.createElement("em", null, "Loading..."))
            : this.renderEmployeeTable(this.state.definitionList);
        return React.createElement("div", null,
            React.createElement("h1", null, "Definition Data"),
            React.createElement("p", null, "This component demonstrates fetching definition data from the server."),
            React.createElement("p", null,
                React.createElement(Link, { to: "/adddefinition" }, "Create New")),
            contents);
    }
    getAll() {
        fetch('api/Definitions/GetAll')
            .then(this.handleErrors)
            .then(response => response.json())
            .then(data => {
            this.setState({ definitionList: data, loading: false });
        });
    }
    handleDelete(definition) {
        fetch('api/Definitions/Delete/', {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(definition)
        })
            .then(this.handleErrors)
            .then(response => response.json())
            .then(data => this.getAll());
    }
    handleEdit(id) {
        this.props.history.push("/definition/edit/" + id);
    }
    handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }
    handleUndelete(definition) {
        fetch('api/Definitions/Undelete/', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(definition)
        })
            .then(this.handleErrors)
            .then(response => response.json())
            .then(data => this.getAll());
    }
    renderEmployeeTable(definitionList) {
        return React.createElement("table", { className: 'table' },
            React.createElement("thead", null,
                React.createElement("tr", null,
                    React.createElement("th", null),
                    React.createElement("th", null, "EmployeeId"),
                    React.createElement("th", null, "Name"),
                    React.createElement("th", null, "Is Deleted"))),
            React.createElement("tbody", null, definitionList.map(definition => React.createElement("tr", { key: definition.definitionId },
                React.createElement("td", null),
                React.createElement("td", null, definition.definitionId),
                React.createElement("td", null, definition.name),
                React.createElement("td", null,
                    React.createElement("input", { type: "checkbox", className: "readonly", checked: definition.isDeleted })),
                React.createElement("td", null,
                    React.createElement("a", { className: "action", onClick: (id) => this.handleEdit(definition.definitionId) }, "Edit"),
                    "  | \u00A0",
                    this.renderDeleteLink(definition))))));
    }
    renderDeleteLink(definition) {
        return definition.isDeleted
            ? React.createElement("a", { className: "action", onClick: (id) => this.handleUndelete(definition) }, "Undelete")
            : React.createElement("a", { className: "action", onClick: (id) => this.handleDelete(definition) }, "Delete");
    }
}
export class DefinitionData {
    constructor() {
        this.definitionId = 0;
        this.name = "";
        this.isDeleted = false;
    }
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/FetchDefinition.js.map