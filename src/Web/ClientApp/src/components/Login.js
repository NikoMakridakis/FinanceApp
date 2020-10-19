import React, { useState } from "react";

import AuthService from "../services/AuthService";

function Login(props) {

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    function onChangeEmail(e) {
        const email = e.target.value;
        setEmail(email);
    };

    function onChangePassword(e) {
        const password = e.target.value;
        setPassword(password);
    };

    function handleLogin() {
        AuthService.login(email, password).then(
            () => {
                props.history.push("/profile");
                window.location.reload();
            }
        );
    }

    return (
        <div>
            <div>
                <form onSubmit={handleLogin}>
                    <div>
                        <label htmlFor="email">Email</label>
                        <input
                            type="text"
                            name="email"
                            value={email}
                            onChange={onChangeEmail}
                        />
                    </div>
                    <div>
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            name="password"
                            value={password}
                            onChange={onChangePassword}
                        />
                    </div>
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