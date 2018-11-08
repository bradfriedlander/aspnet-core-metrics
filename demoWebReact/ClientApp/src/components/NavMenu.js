import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
export class NavMenu extends React.Component {
    constructor(props) {
        super(props);
        //var isAuthenticated = new Authenticate(this.props).isUserAuthenticated();
        //this.state = { hasUserAuthenticated: isAuthenticated};
        this.state = { hasUserAuthenticated: true };
    }
    //public componentDidMount() {
    //    var isAuthenticated = new Authenticate(this.props).isUserAuthenticated();
    //    if (this.state.hasUserAuthenticated !== isAuthenticated) {
    //        alert("NavMenu authentication changed");
    //        this.setState({ hasUserAuthenticated: isAuthenticated });
    //    }
    //}
    render() {
        var isAuthenticated = this.state.hasUserAuthenticated;
        let contentMetrics = this.renderMetricsLink(isAuthenticated);
        let contentDefinitions = this.renderDefinitionsLink(isAuthenticated);
        return React.createElement("div", { className: 'main-nav col-md-8' },
            React.createElement("div", { className: 'navbar navbar-inverse' },
                React.createElement("div", { className: 'navbar-header' },
                    React.createElement("button", { type: 'button', className: 'navbar-toggle', "data-toggle": 'collapse', "data-target": '.navbar-collapse' },
                        React.createElement("span", { className: 'sr-only' }, "Toggle navigation"),
                        React.createElement("span", { className: 'icon-bar' }),
                        React.createElement("span", { className: 'icon-bar' }),
                        React.createElement("span", { className: 'icon-bar' })),
                    React.createElement(Link, { className: 'navbar-brand', to: '/' }, "demoWebReact")),
                React.createElement("div", { className: 'clearfix' }),
                React.createElement("div", { className: 'navbar-collapse collapse' },
                    React.createElement("ul", { className: 'nav navbar-nav' },
                        React.createElement("li", null,
                            React.createElement(NavLink, { to: '/', exact: true, activeClassName: 'active' },
                                React.createElement("span", { className: 'glyphicon glyphicon-home' }),
                                " Home")),
                        contentDefinitions,
                        contentMetrics,
                        React.createElement("li", null,
                            React.createElement(NavLink, { to: '/authenticate', activeClassName: 'active' },
                                React.createElement("span", { className: 'glyphicon glyphicon-th-list' }),
                                " Authentication"))))));
    }
    renderMetricsLink(isAuthenticated) {
        return isAuthenticated
            ? React.createElement("li", null,
                React.createElement(NavLink, { to: '/fetchmetrics', activeClassName: 'active' },
                    React.createElement("span", { className: 'glyphicon glyphicon-th-list' }),
                    " Metrics"))
            : React.createElement("li", null,
                React.createElement("span", { className: 'glyphicon glyphicon-th-list' }),
                " Metrics");
    }
    renderDefinitionsLink(isAuthenticated) {
        return isAuthenticated
            ? React.createElement("li", null,
                React.createElement(NavLink, { to: '/fetchdefinition', activeClassName: 'active' },
                    React.createElement("span", { className: 'glyphicon glyphicon-th-list' }),
                    " Definitions"))
            : React.createElement("li", null,
                React.createElement("span", { className: 'glyphicon glyphicon-th-list' }),
                " Definitions");
    }
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/NavMenu.js.map