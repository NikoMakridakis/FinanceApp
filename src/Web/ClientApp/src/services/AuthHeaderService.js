// To access protected resources, an HTTP request needs an Authorization header with the JWT token.

function AuthHeaderService() {
    const user = JSON.parse(localStorage.getItem('user'));

    if (user && user.accessToken) {
        return { Authorization: 'Bearer ' + user.accessToken };
    } else {
        return {};
    }
}

export default AuthHeaderService()