import axios from 'axios';

import AuthHeaderService from './AuthHeaderService';

const API_URL = 'https://localhost:44387'

function register(email, password) {
    return axios.post(API_URL + '/api/user/register', {
        email,
        password
    });
}

function login(email, password) {
    var authorizationHeader = AuthHeaderService()

    return axios
        .post(API_URL + '/api/user/login', {
            email,
            password,
        }, {
            headers: {
                authorizationHeader
            }
        }

    )
        .then(response => {
            if (response.data.accessToken) {
                localStorage.setItem('email', JSON.stringify(response.data));
            }

            return response.data;
        });
}

function logout() {
    localStorage.removeItem('email');
}

function getCurrentUser() {
    return JSON.parse(localStorage.getItem('email'));
}

export default {
    register,
    login,
    logout,
    getCurrentUser,
};
