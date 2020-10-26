import React, { useState, useEffect } from "react";
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";

import AuthService from './services/AuthService';
import Welcome from './pages/Welcome/Welcome';
import Home from './pages/Home/Home';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Budget from './pages/Budget/Budget';

function App() {

    const [authorized, setAuthorized] = useState(false);

    useEffect(() => {
        checkAuthorization();
    }, []);

    function checkAuthorization() {
        let user = AuthService.getCurrentUser();
        console.log('AuthService checkAuthorization from App.js');
        console.log(user);
        if (user !== null) {
            setAuthorized(true);
        } else {
            setAuthorized(false);
        }
    };

    return (
        <Router>
            <Switch>
                <Route exact path={["/home", "/"]} component={Home} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/register' component={Register} />
                <Route exact path='/welcome' render={() => (authorized === true ? (<Welcome />) : (<Redirect to="/login" />))} />
                <Route exact path='/budget' render={() => (authorized === true ? (<Budget />) : (<Redirect to="/login" />))} />
            </Switch>
        </Router>
    );
};

export default App;