﻿import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import PrivateRoute from './routing/PrivateRoute';
import Home from './pages/Home/Home';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Budget from './pages/Budget/Budget';

function App() {

    return (
        <Router>
            <Switch>
                <Route exact path={["/home", "/"]} component={Home} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/register' component={Register} />
                <PrivateRoute exact path='/budget' component={Budget} />
            </Switch>
        </Router>
    );
};

export default App;