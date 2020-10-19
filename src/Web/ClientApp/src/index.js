import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Home from './components/Home';
import Login from './components/Login';
import BudgetGroupList from './components/BudgetGroupList';
import PrivateRoute from './components/PrivateRoute';

import AuthService from './services/AuthService';

ReactDOM.render(
        <BrowserRouter>
            <Switch>
                <Route path='/' exact component={Home} />
                <Route path='/Account/login' component={Login} />
                <PrivateRoute path='/budget' component={BudgetGroupList} isAuthenticate={() => AuthService.Login()}/>
            </Switch>
        </BrowserRouter>,
    document.getElementById('root') 
);
