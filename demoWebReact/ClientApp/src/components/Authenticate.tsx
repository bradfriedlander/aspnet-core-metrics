import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import { RequiredKeys } from 'prop-types';
import { setAuthentication } from '../store/authentication';

interface AuthenticationState {
    isAuthenticated: boolean;
    userName: string;
}

export class Authenticate extends React.Component<RouteComponentProps<{}>, AuthenticationState> {
    constructor(props) {
        super(props);
        this.state = { isAuthenticated: false, userName: '' };
        this.handleErrors = this.handleErrors.bind(this);
        this.getAuthentication = this.getAuthentication.bind(this);
        this.isUserAuthenticated = this.isUserAuthenticated.bind(this);
        this.getAuthentication();
    }

    public isUserAuthenticated() {
        this.getAuthentication();
        return this.state.isAuthenticated;
    }

    public render() {
        let content = this.state.isAuthenticated
            ? <div>
                <p>Hello, {this.state.userName}</p>
                <p>
                    <a className="action" href="/Identity/Account/Logout">Logout</a>
                </p>
            </div>
            : <div>
                <p>
                    <a className="action" href="/Identity/Account/Login">Login</a>
                </p>
                <p>
                    <a className="action" href="/Identity/Account/Register">Register</a>
                </p>
            </div>
            ;
        return <div>
            <h1>Authentication</h1>
            {content}
        </div>
    }

    private handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }

    private getAuthentication() {
        fetch('api/Authentication/GetAuthentication', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        })
            .then(this.handleErrors)
            .then(response => response.json() as Promise<AuthenticationData>)
            .then(data => {
                this.setState({ isAuthenticated: data.isAuthenticated, userName: data.userName });
                //this.context.store.dispatch(setAuthentication({type:'', isAuthenticated: data.isAuthenticated, userName: data.userName }));
            });
    }

    static contextTypes = {
        //store: PropKeys.object.isRequired
    }
}

export class AuthenticationData {
    isAuthenticated: boolean;
    userName: string;
}
