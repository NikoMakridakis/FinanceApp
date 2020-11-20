import React, { useState } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import AuthService from './services/AuthService';
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

    async function checkAuthentication() {
        const response = await AuthService.checkUserIsAuthenticated();
        if (response === 200) {
            setIsAuthenticated(true);
        } if (response === 400 || 401 || 404) {
            setIsAuthenticated(false);
        }
    }

    checkAuthentication();
       

    //useEffect(() => {
    //    let isMounted = true
    //    const checkAuthentcation = async () => {
    //        const response = await AuthService.checkUserIsAuthenticated();
    //        if (isMounted === true) {
    //            if (response === 200) {
    //                setIsAuthenticated(true);
    //            } if (response === 400 || 401 || 404) {
    //                setIsAuthenticated(false);
    //            }
    //        }
    //    }
    //    checkAuthentcation().then(() => setDataLoaded(true))
    //    return () => {
    //        isMounted = false
    //    }
    //}, [dataLoaded])

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
                    <Register {...props} onChangeEmail={onChangeEmail} email={email} emailExists={emailExists}
                        setEmailExists={setEmailExists} setIsAuthenticated={setIsAuthenticated} />)} />
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
            {routes}
        </Router>
    )
}

export default App;