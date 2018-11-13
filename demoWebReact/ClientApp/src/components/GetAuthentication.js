import { AuthenticationState } from '../store/authentication';
export async function isUserAuthenticated() {
    var authenticationState = new AuthenticationState();
    return await fetch('api/Authentication/GetAuthentication', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
    })
        .then(handleErrors)
        .then(response => response.json())
        .then(data => {
        //console.log(JSON.stringify(data), 'GetAuthentication::isUserAuthenticated');
        authenticationState.isAuthenticated = data.isAuthenticated;
        authenticationState.userName = data.userName;
        return authenticationState;
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
//# sourceMappingURL=C:/MagenicDev/aspnet-core-metrics/demoWebReact/ClientApp/components/GetAuthentication.js.map