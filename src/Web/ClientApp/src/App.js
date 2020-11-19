import React, { useState } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import Landing from './pages/Landing/Landing';
import Login from './pages/Login/Login';
import Reset from './pages/Reset/Reset';
import Register from './pages/Register/Register';
import Welcome from './pages/Welcome/Welcome';
import Budget from './pages/Budget/Budget';

function App() {

    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [email, setEmail] = useState('');
    const [isLoginError, setIsLoginError] = useState(false);
    const [isLockedOut, setIsLockedOut] = useState(false);
    const [emailExists, setEmailExists] = useState(false);

    function onChangeEmail(data) {
        if (data.target.value === '') {
            setIsLockedOut(false);
        }
        setIsLoginError(false);
        setEmailExists(false);
        setEmail(data.target.value);
    }

    let routes;
    if (isAuthenticated === false) {
        routes =
            <Switch>
                <Route exact path='/login' render={(props) => (
                    <Login {...props} onChangeEmail={onChangeEmail} email={email} isLoginError={isLoginError} setIsLoginError={setIsLoginError}
                        isLockedOut={isLockedOut} setIsLockedOut={setIsLockedOut} setIsAuthenticated={setIsAuthenticated}/>)} />
                <Route exact path='/user/reset' render={(props) => (
                    <Reset {...props} onChangeEmail={onChangeEmail} email={email} />)} />
                <Route exact path='/register' render={(props) => (
                    <Register {...props} onChangeEmail={onChangeEmail} email={email} emailExists={emailExists} setEmailExists={setEmailExists} />)} />
                <Route exact path={["/landing", "/", "/*"]} component={Landing} />
            </Switch>
    } else if (isAuthenticated === true) {
        routes =
            <Switch>
                <Route exact path='/welcome' render={(props) => (<Welcome {...props} setIsAuthenticated={setIsAuthenticated}/>)} />
                <Route exact path={["/budget", "/", "/*"]} component={Budget} />
            </Switch>
    }

    return (
        <Router>
            <Switch>
                <Route exact path='/login' render={(props) => (
                    <Login {...props} onChangeEmail={onChangeEmail} email={email} isLoginError={isLoginError} setIsLoginError={setIsLoginError}
                        isLockedOut={isLockedOut} setIsLockedOut={setIsLockedOut} />)} />
                <Route exact path='/user/reset' render={(props) => (
                    <Reset {...props} onChangeEmail={onChangeEmail} email={email} />)} />
                <Route exact path='/register' render={(props) => (
                    <Register {...props} onChangeEmail={onChangeEmail} email={email} emailExists={emailExists} setEmailExists={setEmailExists} />)} />
                <Route exact path='/welcome' component={Welcome} />
                <Route exact path='/budget' component={Budget} />
                <Route exact path={["/landing", "/", "/*"]} component={Landing} />
            </Switch>
        </Router>
    )
}

export default App;