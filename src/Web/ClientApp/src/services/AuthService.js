﻿import axios from 'axios';

const API_URL = 'https://localhost:44387'

function register(email, password) {
    return axios.post(API_URL + '/api/user/register', {
        email,
        password
    });
}

function login(email, password) {

    return axios
        .post(API_URL + '/api/user/login', {
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
}

function getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));
}

export default {
    register,
    login,
    logout,
    getCurrentUser,
};
