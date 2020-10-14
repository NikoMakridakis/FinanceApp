import axios from 'axios';

// The axios instance predefines the baseURL so that the endpoint is shortened.
// For example, axios.get(http://localhost:44387/api/budget) is shortened to axios.get(/api/budget)

const instance = axios.create({
    baseURL: 'http://localhost:44387',
    headers: {
        headerType: 'example header type'
    }
});

export default instance;