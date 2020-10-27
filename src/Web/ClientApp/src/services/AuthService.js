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

function login(email, password) {

    return axios.post('/api/user/login', {
            email,
            password,
        })
        .then((response) => {
            if (response.data.accessToken) {
                localStorage.setItem('user', JSON.stringify(response.data));
            }
            console.log('login response:');
            console.log(response.data);
            return response.data;
        }, (error) => {
            console.log(error);
        });
};

function logout() {
    localStorage.removeItem('user');
};

function getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));
};

export default {
    addAuthHeader,
    register,
    login,
    logout,
    getCurrentUser,
};
