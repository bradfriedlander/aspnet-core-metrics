import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { AuthenticationState } from '../store/authentication';
import { isUserAuthenticated } from './GetAuthentication';

export class Authenticate extends React.Component<RouteComponentProps<{}>, AuthenticationState> {
    constructor(props) {
        super(props);
        this.state = { isAuthenticated: false, userName: '' };
        isUserAuthenticated().then(userAuthentication => {
            console.log(JSON.stringify(userAuthentication), 'Authenticate::constructor');
            this.setState({ isAuthenticated: userAuthentication.isAuthenticated, userName: userAuthentication.userName });
        });
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
}
