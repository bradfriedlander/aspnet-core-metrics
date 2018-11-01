import './css/site.css';
import 'bootstrap';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import { BrowserRouter } from 'react-router-dom';
import * as RoutesModule from './routes';
let routes = RoutesModule.routes;
function renderApp() {
    const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
    ReactDOM.render(React.createElement(AppContainer, null,
        React.createElement(BrowserRouter, { children: routes, basename: baseUrl })), document.getElementById('react-app'));
}
renderApp();
// Allow Hot Module Replacement
//if (module.hot) {
//    module.hot.accept('./routes', () => {
//        routes = require<typeof RoutesModule>('./routes').routes;
//        renderApp();
//    });
//}
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/boot.js.map