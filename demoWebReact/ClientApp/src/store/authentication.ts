export enum TypeKeys {
    SET_AUTHENTICATION = 'SET_AUTHENTICATION',
    OTHER_ACTION = 'any_other_action_type'
}

export interface SetAuthentication {
    type: TypeKeys.SET_AUTHENTICATION;
    isAuthenticated: boolean;
    userName: string;
}

export interface OtherAction {
    type: TypeKeys.OTHER_ACTION;
}

export type ActionTypes = SetAuthentication | OtherAction;

export class AuthenticationState {
    isAuthenticated: boolean = false;
    userName: string = '';
}

export function authenticationReducer(s: AuthenticationState = {isAuthenticated:false, userName: 'unknown'}, action: ActionTypes) {
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
export const setAuthentication = (isAuthenticated: boolean, userName: string): SetAuthentication => ({
    type: TypeKeys.SET_AUTHENTICATION,
    isAuthenticated,
    userName
});


export const mapStatetoProps = state => {
    return { authentication: AuthenticationState };
}

export const mapDispatchToProps = dispatch => {
    dispatch(setAuthentication)
}
