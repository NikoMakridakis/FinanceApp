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
        return { };
    }
}

function register(email, password) {
    return axios.post('/api/user/register', {
        email,
        password
    });
}

async function login(email, password) {
    try {
        console.log('login post request sent from AuthService.login');
        const response = await axios.post('/api/user/login',
            {
                email,
                password,
            })
        console.log('login response:')
        console.log(response.data);

        if (response.data.accessToken) {
            console.log('set user in local storage with email and access token');
            localStorage.setItem('user', JSON.stringify(response.data));
        }

        return response.data;
    } catch (error) {
        console.log(error);
    }
}

function logout() {
    localStorage.removeItem('user');
};

async function getCurrentUser() {
    try {
        console.log('fetching current user from local storage from AuthService.getCurrentUser');
        const response = await JSON.parse(localStorage.getItem('user'));
        console.log('getCurrentUser response:');
        console.log(response.data);
        return response.data;
    } catch (error) {
        console.log(error);
    }
};

export default {
    addAuthHeader,
    register,
    login,
    logout,
    getCurrentUser,
};
