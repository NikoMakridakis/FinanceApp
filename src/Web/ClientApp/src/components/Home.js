import React from "react";
import { useHistory } from "react-router-dom";

function Home() {

    const history = useHistory();

    const navigateToHome = () => history.push('/home');
    const navigateToLogin = () => history.push('/login');
    const navigateToRegister = () => history.push('/register');
    const navigateToProfile = () => history.push('/profile');
    
    return (
        <div>
            <button onClick={navigateToHome}>Home</button>
            <button onClick={navigateToLogin}>Login</button>
            <button onClick={navigateToRegister}>Register</button>
            <button onClick={navigateToProfile}>Profile</button>
        </div>
    );
};

export default Home;