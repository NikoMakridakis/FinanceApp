import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import PrivateRoute from './routing/PrivateRoute';
import Home from './components/Home';
import Login from './components/Login';
import Register from './components/Register';
import Profile from './components/Profile';

function App() {

    return (
        <Router>
            <Switch>
                <Route exact path={["/home", "/"]} component={Home} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/register' component={Register} />
                <PrivateRoute exact path='/profile' component={Profile} />
            </Switch>
        </Router>
    );
};

export default App;