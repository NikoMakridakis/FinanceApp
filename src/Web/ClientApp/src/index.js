import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Home from './components/Home';
import Login from './components/Login';
import BudgetGroupList from './components/BudgetGroupList';
import NotFound from './components/NotFound';

ReactDOM.render(
        <BrowserRouter>
            <Switch>
                <Route path="/" exact component={Home} />
                <Route path="/login" component={Login} />
                <Route path="/budget" component={BudgetGroupList} />
                <Route path="*" component={NotFound} />
            </Switch>
        </BrowserRouter>,
    document.getElementById('root')
);
