import * as React from 'react';
import { NavMenu } from './NavMenu';
export class Layout extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return React.createElement("div", { className: 'container-fluid' },
            React.createElement("div", { className: 'row' },
                React.createElement("div", { className: 'col-sm-3' },
                    React.createElement(NavMenu, null)),
                React.createElement("div", { className: 'col-sm-9' }, this.props.children)));
    }
}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/Layout.js.map