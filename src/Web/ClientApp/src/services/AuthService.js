import axios from '../axios/axios';

// To access protected resources, an HTTP request needs an Authorization header with the JWT token.
function addAuthHeader() {
    const user = JSON.parse(localStorage.getItem('user'));
    if (user && user.accessToken) {
        const accessToken = user.accessToken;
        axios.interceptors.request.use(
            function (config) {
                config.headers.Authorization = accessToken ? `Bearer ${accessToken}` : '';
                return config;
            }
        );
    } else {
        return { }
    }
}

async function register(email, password, confirmPassword) {
    try {
        const response = await axios.post('/api/user/register',
            {
                email,
                password,
                confirmPassword
            })
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            localStorage.setItem('user', JSON.stringify(response.data));
            console.log('AuthService.register response:');
            console.log(response);
            return responseStatusCode;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 401) {
            console.log('AuthService.register response catch error status code:');
            console.log(errorStatusCode);
            return errorStatusCode
        }
    }
}

async function loginForStaySignedIn(email, password) {
    try {
        const response = await axios.post('/api/user/login',
            {
                email,
                password,
            })
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            localStorage.setItem('user', JSON.stringify(response.data));
            console.log('AuthService.loginForStaySignedIn response:');
            console.log(response);
            return responseStatusCode;
        } 
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 401 || 404) {
            if (error.response.data.isLockedOut) {
                return error.response.data;
            }
            console.log('AuthService.login response catch error status code:');
            console.log(errorStatusCode);
            return errorStatusCode
        }
    }
}

async function loginForNotStaySignedIn(email, password) {
    try {
        const response = await axios.post('/api/user/login',
            {
                email,
                password,
            })
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            sessionStorage.setItem('user', JSON.stringify(response.data));
            console.log('AuthService.loginForStaySignedIn response:');
            console.log(response);
            return responseStatusCode;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 401 || 404) {
            if (error.response.data.isLockedOut) {
                return error.response.data;
            }
            console.log('AuthService.login response catch error status code:');
            console.log(errorStatusCode);
            return errorStatusCode
        }
    }
}

function logout() {
    localStorage.removeItem('user');
}

export default {
    addAuthHeader,
    register,
    loginForStaySignedIn,
    loginForNotStaySignedIn,
    logout,
}
