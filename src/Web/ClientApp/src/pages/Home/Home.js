import React from "react";
import { useHistory } from "react-router-dom";
import Button from '@material-ui/core/Button';

function Home() {

    const history = useHistory();

    function navigateToHome() {
        history.push('/home');
    };

    function navigateToLogin() {
        history.push('/login');
    }

    function navigateToRegister() {
        history.push('/register');
    }

    function navigateToBudget() {
        history.push('/budget');
    }

    return (
        <div>
            <h1>
                Home
            </h1>
            <ul>
                <li>
                    <Button variant="contained" color="primary" onClick={navigateToHome}>
                        Home
                    </Button>
                </li>
                <li>
                    <Button variant="contained" color="primary" onClick={navigateToLogin}>
                        Login
                    </Button>
                </li>
                <li>
                    <Button variant="contained" color="primary" onClick={navigateToRegister}>
                        Register
                    </Button>
                </li>
                <li>
                    <Button variant="contained" color="primary" onClick={navigateToBudget}>
                        Budget
                    </Button>
                </li>
            </ul>
        </div>
    );
};

export default Home;