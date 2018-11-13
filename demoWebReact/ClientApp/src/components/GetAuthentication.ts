import { AuthenticationState } from '../store/authentication';

export async function isUserAuthenticated(): Promise<AuthenticationState> {
    var xxx = new AuthenticationState();
    return await fetch('api/Authentication/GetAuthentication', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
    })
        .then(handleErrors)
        .then(response => response.json() as Promise<AuthenticationState>)
        .then(data => {
            console.log(JSON.stringify(data), 'GetAuthentication::isUserAuthenticated');
            //this.setState({ isAuthenticated: data.isAuthenticated, userName: data.userName });
            xxx.isAuthenticated = data.isAuthenticated;
            xxx.userName = data.userName;
            return xxx;
        })
        .catch(error => {
            console.log(error, 'GetAuthentication::isUserAuthenticated');
            throw Error(error);
        });
}

function handleErrors(response) {
    if (!response.ok) {
        console.log(response.statusText, 'GetAuthentication::handleErrors');
        throw Error(response.statusText);
    }
    return response;
}
