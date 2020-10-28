import axios from 'axios';

axios.interceptors.response.use((res) => {
    return Promise.resolve(res);
}, (error) => {
        if (error !== null && error.status === 401) {
            console.log('axios interceptor redirecting to login page');
            window.location.href = '/login';
        }
        
        return Promise.reject(error);
})

export default axios.create({
    baseURL: 'https://localhost:44387',
    headers: {
        'Content-Type': 'application/json'
    }
});