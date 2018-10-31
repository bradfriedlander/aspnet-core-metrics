import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchDefinition } from './components/FetchDefinition';
import { AddDefinition } from './components/AddDefinition';
export const routes = React.createElement(Layout, null,
    React.createElement(Route, { exact: true, path: '/', component: Home }),
    React.createElement(Route, { path: '/fetchdefinition', component: FetchDefinition }),
    React.createElement(Route, { path: '/adddefinition', component: AddDefinition }),
    React.createElement(Route, { path: '/employee/edit/:empid', component: FetchDefinition }));
//# sourceMappingURL=routes.js.map