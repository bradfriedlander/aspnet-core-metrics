import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchDefinition } from './components/FetchDefinition';
import { AddDefinition } from './components/AddDefinition';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/fetchdefinition' component={FetchDefinition} />
    <Route path='/adddefinition' component={AddDefinition} />
    <Route path='/definition/edit/:definitionid' component={AddDefinition} />
</Layout>;
