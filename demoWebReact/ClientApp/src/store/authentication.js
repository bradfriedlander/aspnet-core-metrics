export var TypeKeys;
(function (TypeKeys) {
    TypeKeys["SET_AUTHENTICATION"] = "SET_AUTHENTICATION";
    TypeKeys["OTHER_ACTION"] = "any_other_action_type";
})(TypeKeys || (TypeKeys = {}));
export class AuthenticationState {
    constructor() {
        this.isAuthenticated = false;
        this.userName = '';
    }
}
export function authenticationReducer(s = { isAuthenticated: false, userName: 'unknown' }, action) {
    switch (action.type) {
        case TypeKeys.SET_AUTHENTICATION:
            console.log(action.type);
            console.log(JSON.stringify(action));
            return { isAuthenticated: action.isAuthenticated, userName: action.userName };
        default:
            return s;
    }
}
// action creator
export const setAuthentication = (isAuthenticated, userName) => ({
    type: TypeKeys.SET_AUTHENTICATION,
    isAuthenticated,
    userName
});
export const mapStatetoProps = state => {
    return { authentication: AuthenticationState };
};
export const mapDispatchToProps = dispatch => {
    dispatch(setAuthentication);
};
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/store/authentication.js.map