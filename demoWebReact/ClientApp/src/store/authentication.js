export const SET_AUTHENTICATION = 'SET_AUTHENTICATION';
export class AuthenticationData {
    constructor() {
        this.isAuthenticated = false;
        this.userName = '';
    }
}
export const authentication = (state = [], action) => {
    switch (action.type) {
        case SET_AUTHENTICATION:
            console.log(SET_AUTHENTICATION + ': ' + action);
            return { isAuthenticated: action.isAuthenticated, userName: action.userName };
        default:
            return state;
    }
};
export const setAuthentication = (action) => ({ type: SET_AUTHENTICATION, isAuthenticated: action.isAuthenticated, userName: action.userName });
export const mapStatetoProps = state => {
    return { authetication: authentication };
};
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/store/authentication.js.map