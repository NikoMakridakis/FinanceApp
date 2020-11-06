import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import Home from './pages/Home/Home';
import Login from './pages/Login/Login';
import Reset from './pages/Reset/Reset';
import ResetPassword from './pages/Reset/ResetPassword';
import Register from './pages/Register/Register';
import Welcome from './pages/Welcome/Welcome';
import Budget from './pages/Budget/Budget';

function App() {

    return (
        <Router>
            <Switch>
                <Route exact path={["/home", "/"]} component={Home} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/reset' component={Reset} />
                <Route exact path='/resetPassword' component={ResetPassword} />
                <Route exact path='/register' component={Register} />
                <Route exact path='/welcome' component={Welcome} />
                <Route exact path='/budget' component={Budget} />
            </Switch>
        </Router>
    )
}

export default App;