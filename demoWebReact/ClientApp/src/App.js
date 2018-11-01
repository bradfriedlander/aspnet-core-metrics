import * as React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
//import Counter from './components/Counter';
//import FetchData from './components/FetchData';
//import Definitions from './components/Definitions';
import { FetchDefinition } from './components/FetchDefinition';
import { AddDefinition } from './components/AddDefinition';
//export default () => (
//    <Layout>
//        <Route exact path='/' component={Home} />
//        <Route path='/counter' component={Counter} />
//        <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
//        <Route path='/definitions/:startDateIndex?' component={Definitions} />
//        <Route path='/fetchdefinition' component={FetchDefinition} />
//        <Route path='/adddefinition' component={AddDefinition} />
//        <Route path='/definition/edit/:definitionid' component={AddDefinition} />
//    </Layout>
//);
export default () => (React.createElement(Layout, null,
    React.createElement(Route, { exact: true, path: '/', component: Home }),
    React.createElement(Route, { path: '/fetchdefinition', component: FetchDefinition }),
    React.createElement(Route, { path: '/adddefinition', component: AddDefinition }),
    React.createElement(Route, { path: '/definition/edit/:definitionid', component: AddDefinition })));
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/App.js.map