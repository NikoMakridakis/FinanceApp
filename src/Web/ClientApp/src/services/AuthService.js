import axios from '../axios/axios';
import AuthHeader from './AuthHeader';

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
            return response.data;
        });
};

function logout() {
    localStorage.removeItem('user');
};

function getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));
};

function isAuthenticated() {
    axios.post('/api/user/isAuthenticated', {
        headers: AuthHeader()
    })
    .then(function (response) {
        //handle success
        console.log(response);
    })
    .catch(function (response) {
        //handle error
        console.log(response);
    });
};

export default {
    register,
    login,
    logout,
    getCurrentUser,
    isAuthenticated,
};
