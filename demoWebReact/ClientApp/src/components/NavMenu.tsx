import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { AuthenticationState } from '../store/authentication';
import { isUserAuthenticated } from './GetAuthentication';

export class NavMenu extends React.Component<{}, AuthenticationState> {
    constructor(props) {
        super(props);
        this.state = { isAuthenticated: true, userName: '' };
        isUserAuthenticated().then(userAuthentication => {
            this.setState({ isAuthenticated: userAuthentication.isAuthenticated, userName: userAuthentication.userName });
        });
    }

    public render() {
        var hasUserAuthenticated = this.state.isAuthenticated;
        let contentMetrics = this.renderMetricsLink(hasUserAuthenticated);
        let contentDefinitions = this.renderDefinitionsLink(hasUserAuthenticated);
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

    private renderMetricsLink(isAuthenticated: boolean) {
        return isAuthenticated
            ? <li>
                <NavLink to={'/fetchmetrics'} activeClassName='active'>
                    <span className='glyphicon glyphicon-th-list'></span> Metrics
                            </NavLink>
            </li>
            : null;
    }

    private renderDefinitionsLink(isAuthenticated: boolean) {
        return isAuthenticated
            ? <li>
                <NavLink to={'/fetchdefinition'} activeClassName='active'>
                    <span className='glyphicon glyphicon-th-list'></span> Definitions
                            </NavLink>
            </li>
             : null;
   }
}
