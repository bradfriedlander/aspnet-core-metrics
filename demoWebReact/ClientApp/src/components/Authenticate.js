import * as React from 'react';
export class Authenticate extends React.Component {
    constructor(props) {
        super(props);
        this.state = { isAuthenticated: false, userName: '' };
        this.handleErrors = this.handleErrors.bind(this);
        this.getAuthentication = this.getAuthentication.bind(this);
        this.isUserAuthenticated = this.isUserAuthenticated.bind(this);
        this.getAuthentication();
    }
    isUserAuthenticated() {
        this.getAuthentication();
        return this.state.isAuthenticated;
    }
    render() {
        let content = this.state.isAuthenticated
            ? React.createElement("div", null,
                React.createElement("p", null,
                    "Hello, ",
                    this.state.userName),
                React.createElement("p", null,
                    React.createElement("a", { className: "action", href: "/Identity/Account/Logout" }, "Logout")))
            : React.createElement("div", null,
                React.createElement("p", null,
                    React.createElement("a", { className: "action", href: "/Identity/Account/Login" }, "Login")),
                React.createElement("p", null,
                    React.createElement("a", { className: "action", href: "/Identity/Account/Register" }, "Register")));
        return React.createElement("div", null,
            React.createElement("h1", null, "Authentication"),
            content);
    }
    handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }
    getAuthentication() {
        fetch('api/Authentication/GetAuthentication', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        })
            .then(this.handleErrors)
            .then(response => response.json())
            .then(data => {
            this.setState({ isAuthenticated: data.isAuthenticated, userName: data.userName });
        });
    }
}
export class AuthenticationData {
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/Authenticate.js.map