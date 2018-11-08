import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

interface NavState {
    hasUserAuthenticated: boolean;
    userName: string;
}

export class NavMenu extends React.Component<{}, NavState> {
    constructor(props) {
        super(props);
        this.state = { hasUserAuthenticated: true, userName: '' };
        this.getAuthentication();
    }

    public render() {
        var isAuthenticated = this.state.hasUserAuthenticated;
        let contentMetrics = this.renderMetricsLink(isAuthenticated);
        let contentDefinitions = this.renderDefinitionsLink(isAuthenticated);
        return <div className='main-nav col-md-8'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>demoWebReact</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink to={'/'} exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        {contentDefinitions}
                        {contentMetrics}
                        <li>
                            <NavLink to={'/authenticate'} activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Authentication
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
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
                this.setState({ hasUserAuthenticated: data.isAuthenticated, userName: data.userName });
            });
    }

    private handleErrors(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }

    private renderMetricsLink(isAuthenticated: boolean) {
        return isAuthenticated
            ? <li>
                <NavLink to={'/fetchmetrics'} activeClassName='active'>
                    <span className='glyphicon glyphicon-th-list'></span> Metrics
                            </NavLink>
            </li>
            : <li>
                <span className='glyphicon glyphicon-th-list'></span> Metrics
                </li>;
    }

    private renderDefinitionsLink(isAuthenticated: boolean) {
        return isAuthenticated
            ? <li>
                <NavLink to={'/fetchdefinition'} activeClassName='active'>
                    <span className='glyphicon glyphicon-th-list'></span> Definitions
                            </NavLink>
            </li>
            : <li>
                <span className='glyphicon glyphicon-th-list'></span> Definitions
                </li>;
    }
}

export class AuthenticationData {
    isAuthenticated: boolean;
    userName: string;
}
