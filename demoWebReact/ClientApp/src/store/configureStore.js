import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import { authenticationReducer } from './authentication';

export default function configureStore(history, initialState) {
    const reducers = {
    };

    const middleware = [
        thunk,
        routerMiddleware(history)
    ];

    const enhancers = [];
    const isDevelopment = process.env.NODE_ENV === 'development';
    if (isDevelopment && typeof window !== 'undefined' && window.__REDUX_DEVTOOLS_EXTENSION__ ) {
        enhancers.push(window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__());
    }

    const rootReducer = combineReducers({
        ...reducers,
        routing: routerReducer,
        authenticating: authenticationReducer
    });

    return createStore(
        rootReducer,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
}
