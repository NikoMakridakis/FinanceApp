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

async function register(email, fullName, password) {
    try {
        const response = await axios.post('/user/register',
            {
                email,
                fullName,
                password
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
        if (errorStatusCode === 400 || 401 || 404) {
            console.log('AuthService.register response catch error status code:');
            console.log(errorStatusCode);
            return errorStatusCode
        }
    }
}

async function loginForStaySignedIn(email, password) {
    try {
        const response = await axios.post('/user/login',
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
        if (errorStatusCode === 400 || 401 || 404) {
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
        const response = await axios.post('/user/login',
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
        if (errorStatusCode === 400 || 401 || 404) {
            if (error.response.data.isLockedOut) {
                return error.response.data;
            }
            console.log('AuthService.login response catch error status code:');
            console.log(errorStatusCode);
            return errorStatusCode
        }
    }
}

async function googleLogin() {
    try {
        const response = await axios.get('/user/googleLogin');
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            sessionStorage.setItem('user', JSON.stringify(response.data));
            console.log('AuthService.loginForStaySignedIn response:');
            console.log(response);
            return responseStatusCode;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 400 || 401 || 404) {
            if (error.response.data.isLockedOut) {
                return error.response.data;
            }
            console.log('AuthService.login response catch error status code:');
            console.log(errorStatusCode);
            return errorStatusCode
        }
    }
}

async function forgotPassword(email) {
    try {
        const response = await axios.post('/user/forgotPassword', { email })
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            console.log('verification email sent!');
            return responseStatusCode;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 400 || 401 || 404) {
            console.log('Error, no email sent');
            return errorStatusCode
        }
    }
}

async function verifyResetToken(email, resetToken) {
    try {
        const response = await axios.post('/user/verifyResetToken', { email, resetToken })
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            console.log('Success, token is valid');
            return responseStatusCode;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 400 || 401 || 404) {
            console.log('Error, token not valid');
            return errorStatusCode
        }
    }
}

async function resetPassword(email, password, confirmPassword, resetToken) {
    try {
        const response = await axios.post('/user/resetPassword', { email, password, confirmPassword, resetToken })
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            console.log('Success, password reset!');
            return responseStatusCode;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 400 || 401 || 404) {
            console.log('Error, password not reset!');
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
    googleLogin,
    forgotPassword,
    verifyResetToken,
    resetPassword,
    logout,
}
