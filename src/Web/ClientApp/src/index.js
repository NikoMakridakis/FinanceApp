import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Home from './components/Home';
import Login from './components/Login';

ReactDOM.render(
    <BrowserRouter>
        <Switch>
            <Route exact path="/home" component={ Home } />
            <Route path="/login" component={ Login } />
        </Switch>
    </BrowserRouter>,
    document.getElementById('root')
);
