import axios from "axios";

const API_URL = "https://localhost:44387";

class AuthService {
    login(email, password) {
        return axios
            .post(API_URL + "/api/user/login", {
                email,
                password
            })
            .then(response => {
                if (response.data.accessToken) {
                    localStorage.setItem("email", JSON.stringify(response.data));
                }

                return response.data;
            });
    }

    logout() {
        localStorage.removeItem("email");
    }

    register(email, password) {
        return axios.post(API_URL + "/api/user/register", {
            email,
            password
        });
    }

    getCurrentUser() {
        return JSON.parse(localStorage.getItem('email'));;
    }
}

export default new AuthService();