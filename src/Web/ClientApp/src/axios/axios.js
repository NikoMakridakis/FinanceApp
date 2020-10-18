import axios from 'axios';

const instance = axios.create({
    baseURL: 'http://localhost:44387',
});

export default instance;