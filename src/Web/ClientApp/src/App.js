import React, { useState } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import Home from './pages/Home/Home';
import Login from './pages/Login/Login';
import Reset from './pages/Reset/Reset';
import Register from './pages/Register/Register';
import Welcome from './pages/Welcome/Welcome';
import Budget from './pages/Budget/Budget';

function App() {

    const [email, setEmail] = useState('');

    function onChangeEmail(data) {
        setEmail(data.target.value);
    }

    return (
        <Router>
            <Switch>
                <Route exact path={["/home", "/"]} component={Home} />
                <Route exact path='/login'
                    render={(props) => (
                        <Login {...props}
                            onChangeEmail={onChangeEmail}
                            email={email}
                        />)}
                />
                <Route exact path='/user/Reset'
                    render={(props) => (
                        <Reset {...props}
                            onChangeEmail={onChangeEmail}
                            email={email}
                    />)}
                />
                <Route exact path='/register'
                    render={(props) => (
                        <Register {...props}
                            onChangeEmail={onChangeEmail}
                            email={email}
                        />)}
                />
                <Route exact path='/welcome' component={Welcome} />
                <Route exact path='/budget' component={Budget} />
            </Switch>
        </Router>
    )
}

export default App;