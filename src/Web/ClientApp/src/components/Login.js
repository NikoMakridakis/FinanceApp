import React from "react";

import AuthService from "../services/AuthService";

function Login(props) {

    const email = 'demouser@microsoft.com';
    const password = 'Pass@word1';

    const handleLogin = () => {
        AuthService.login(email, password).then(
            () => {
                props.history.push('/profile');
                window.location.reload();
            }
        );
    };

    return (
        <div>
            <div>
                <form onSubmit={handleLogin}>
                    <div>
                        <button>
                            <span>Login</span>
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Login;