import React, { useState } from "react";
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";

import AuthService from './services/AuthService';
import Welcome from './pages/Welcome/Welcome';
import Home from './pages/Home/Home';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Budget from './pages/Budget/Budget';

function App() {

    const [authorized, setAuthorized] = useState(false);

    function setAuthorization() {
        console.log('fetching current user from App.setAuthorization');
        const response = AuthService.getCurrentUser();
        console.log('AuthService.getCurrentUser response:');
        console.log(response);
        const user = response.data;
        console.log('user data:');
        console.log(user);
        if (user !== null) {
            console.log('state set to true');
            setAuthorized(true);
        } else {
            console.log('state set to false');
            setAuthorized(false);
        }
    };

    return (
        <Router>
            <Switch>
                <Route exact path={["/home", "/"]} component={Home} />
                <Route exact path='/login' render={(props) => (
                    <Login {...props} setAuthorization={setAuthorization} />
                )}/>
                <Route exact path='/register' component={Register} />
                <Route exact path='/welcome' render={() => (authorized === true ? (<Welcome />) : (<Redirect to="/login" />))} />
                <Route exact path='/budget' render={() => (authorized === true ? (<Budget />) : (<Redirect to="/login" />))} />
            </Switch>
        </Router>
    );
};

export default App;