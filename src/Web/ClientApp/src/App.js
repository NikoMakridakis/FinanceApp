import React, { useState } from "react";
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";

import Landing from './pages/Landing/Landing';
import Login from './pages/Login/Login';
import Reset from './pages/Reset/Reset';
import Register from './pages/Register/Register';
import Welcome from './pages/Welcome/Welcome';
import Budget from './pages/Budget/Budget';

function App() {

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

    const isAuthenticated = JSON.parse(localStorage.getItem('isAuthenticated'));
    
    if (isAuthenticated === true) {
        routes =
            <Switch>
                <Route exact path='/welcome' render={(props) => (<Welcome {...props} />)} />
                <Route exact path={['/budget', '/']} component={Budget} />
                <Route exact path='/*'>
                    <Redirect to='/budget' /> : <Budget />}
                </Route>
            </Switch>
    }

    else {
        routes =
            <Switch>
                <Route exact path='/login' render={(props) => (
                    <Login {...props} onChangeEmail={onChangeEmail} email={email} isLoginError={isLoginError} setIsLoginError={setIsLoginError}
                        isLockedOut={isLockedOut} setIsLockedOut={setIsLockedOut} />)} />
                <Route exact path='/user/reset' render={(props) => (
                    <Reset {...props} onChangeEmail={onChangeEmail} email={email} />)} />
                <Route exact path='/register' render={(props) => (
                    <Register {...props} onChangeEmail={onChangeEmail} email={email} emailExists={emailExists}
                        setEmailExists={setEmailExists} />)} />
                <Route exact path={['/landing', '/']} component={Landing} />
                <Route exact path='/*'>
                    <Redirect to='/landing' /> : <Landing />}
                    </Route>
            </Switch>
    }

    return (
        <Router>
            {routes}
        </Router>
    )
}

export default App;