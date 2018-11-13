import * as React from 'react';
import { isUserAuthenticated } from './GetAuthentication';
export class Authenticate extends React.Component {
    constructor(props) {
        super(props);
        this.state = { isAuthenticated: false, userName: '' };
        isUserAuthenticated().then(userAuthentication => {
            console.log(JSON.stringify(userAuthentication), 'Authenticate::constructor');
            this.setState({ isAuthenticated: userAuthentication.isAuthenticated, userName: userAuthentication.userName });
        });
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
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/Authenticate.js.map