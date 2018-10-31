import * as React from 'react';
import { Link } from 'react-router-dom';

export class FetchDefinition extends React.Component {
    constructor(props) {
        super(props);
        this.state = { definitionList: [], loading: true };
        fetch('api/Definitions/GetAll')
            .then(response => response.json())
            .then(data => {
            this.setState({ definitionList: data, loading: false });
        });
        // This binding is necessary to make "this" work in the callback
        this.handleDelete = this.handleDelete.bind(this);
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
    // Handle Delete request for an employee
    handleDelete(id) {
        fetch('api/Definition/Delete/' + id, {
            method: 'delete'
        })
            .then(data => {
            this.setState({
                //definitionList: this.state.definitionList.filter((rec) => {
                //    return (rec.definitionId != id);
                //};
                definitionList: this.state.definitionList
            });
        });
    }
    handleEdit(id) {
        this.props.history.push("/employee/edit/" + id);
    }
    // Returns the HTML table to the render() method.
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
                    React.createElement("input", { type: "checkbox", checked: definition.isDeleted })),
                React.createElement("td", null,
                    React.createElement("a", { className: "action", onClick: (id) => this.handleEdit(definition.definitionId) }, "Edit"),
                    "  |",
                    React.createElement("a", { className: "action", onClick: (id) => this.handleDelete(definition.definitionId) }, "Delete"))))));
    }
}
export class DefinitionData {
    constructor() {
        this.definitionId = 0;
        this.name = "";
        this.isDeleted = false;
    }
}
//# sourceMappingURL=FetchDefinition.js.map