﻿export const SET_AUTHENTICATION = 'SET_AUTHENTICATION';

export class AuthenticationData {
    type: string;
    isAuthenticated: boolean = false;
    userName: string = '';
}

export const authentication = (state = [], action: AuthenticationData) => {
    switch (action.type) {
        case SET_AUTHENTICATION:
            console.log(SET_AUTHENTICATION + ': ' + action);
            return { isAuthenticated: action.isAuthenticated, userName: action.userName };
        default:
            return state;
    }
}

export const setAuthentication = (action: AuthenticationData) => ({ type: SET_AUTHENTICATION, isAuthenticated: action.isAuthenticated, userName: action.userName });

export const mapStatetoProps = state => {
    return { authetication: authentication };
}