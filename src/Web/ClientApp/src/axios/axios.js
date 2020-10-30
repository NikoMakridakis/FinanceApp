import axios from 'axios';

axios.create({
    baseURL: 'https://localhost:44387',
    headers: {
        'Content-Type': 'application/json'
    }
})

export default axios;