﻿import * as React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchDefinition } from './components/FetchDefinition';
import { AddDefinition } from './components/AddDefinition';
import { FetchMetrics } from './components/FetchMetrics';
import { Authenticate } from './components/Authenticate';

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/fetchdefinition' component={FetchDefinition} />
        <Route path='/adddefinition' component={AddDefinition} />
        <Route path='/definition/edit/:definitionid' component={AddDefinition} />
        <Route path='/fetchmetrics' component={FetchMetrics} />
        <Route path='/authenticate' component={Authenticate} />
    </Layout>
);
