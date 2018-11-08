import * as React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchDefinition } from './components/FetchDefinition';
import { AddDefinition } from './components/AddDefinition';
import { FetchMetrics } from './components/FetchMetrics';
import { Authenticate } from './components/Authenticate';
export default () => (React.createElement(Layout, null,
    React.createElement(Route, { exact: true, path: '/', component: Home }),
    React.createElement(Route, { path: '/fetchdefinition', component: FetchDefinition }),
    React.createElement(Route, { path: '/adddefinition', component: AddDefinition }),
    React.createElement(Route, { path: '/definition/edit/:definitionid', component: AddDefinition }),
    React.createElement(Route, { path: '/fetchmetrics', component: FetchMetrics }),
    React.createElement(Route, { path: '/authenticate', component: Authenticate })));
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/App.js.map