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
            .then(response => response.json())
            .then(data => {
            this.setState({ definitionList: data, loading: false });
        });
    }
    handleDelete(id) {
        fetch('api/Definitions/Delete/' + id, {
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
        this.getAll();
    }
    handleEdit(id) {
        this.props.history.push("/definition/edit/" + id);
    }
    handleUndelete(id) {
        fetch('api/Definitions/Undelete/' + id, {
            method: 'put'
        })
            .then(data => {
            this.setState({
                //definitionList: this.state.definitionList.filter((rec) => {
                //    return (rec.definitionId != id);
                //};
                definitionList: this.state.definitionList
            });
        });
        this.getAll();
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
                    React.createElement("input", { type: "checkbox", checked: definition.isDeleted })),
                React.createElement("td", null,
                    React.createElement("a", { className: "action", onClick: (id) => this.handleEdit(definition.definitionId) }, "Edit"),
                    "  | \u00A0",
                    this.renderDeleteLink(definition.definitionId, definition.isDeleted))))));
    }
    renderDeleteLink(definitionId, isDeleted) {
        return isDeleted
            ? React.createElement("a", { className: "action", onClick: (id) => this.handleUndelete(definitionId) }, "Undelete")
            : React.createElement("a", { className: "action", onClick: (id) => this.handleDelete(definitionId) }, "Delete");
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