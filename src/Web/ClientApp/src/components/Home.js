import React from "react";
import { Link } from "react-router-dom";

function Home() {
    return (
        <div>
            <h1>
                Home
            </h1>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/login">Login</Link>
                </li>
                <li>
                    <Link to="/register">Register</Link>
                </li>
                <li>
                    <Link to="/profile">Profile</Link>
                </li>
            </ul>
        </div>
    );
};

export default Home;