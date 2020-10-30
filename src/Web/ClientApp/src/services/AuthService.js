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

function register(email, password) {
    return axios.post('/api/user/register', {
        email,
        password
    })
}

async function login(email, password) {
    try {
        const response = await axios.post('/api/user/login',
            {
                email,
                password,
            })

        if (response.data.accessToken) {
            localStorage.setItem('user', JSON.stringify(response.data));
            console.log(`login response: ${JSON.stringify(response.data)}`);
            return response.data;
        }

    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 401 || 404) {
            console.log(`login response catch error status code: ${JSON.stringify(errorStatusCode)}`);
            return errorStatusCode
        }
    }
}

function logout() {
    localStorage.removeItem('user');
}

async function getCurrentUser() {
    try {
        console.log('fetching current user from local storage from AuthService.getCurrentUser');
        const response = await JSON.parse(localStorage.getItem('user'));
        console.log(`getCurrentUser response: ${response.data}`);
        return response.data;
    } catch (error) {
        console.log(error);
    }
}

export default {
    addAuthHeader,
    register,
    login,
    logout,
    getCurrentUser,
}
