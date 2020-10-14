import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Switch } from "react-router-dom";
import Home from './components/Home';
import Login from './components/Login';
import NotFound from './components/NotFound';
import repositoryReducer from './store/reducers/repositoryReducer';
import { Provider } from 'react-redux';
import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';

// The thunk middleware allows us to send async requests with Redux actions.

const store = createStore(repositoryReducer, applyMiddleware(thunk));

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>
            <Switch>
                <Route path="/" exact component={Home} />
                <Route path="/login" component={Login} />
                <Route path="*" component={NotFound} />
            </Switch>
        </BrowserRouter>
    </Provider>,
    document.getElementById('root')
);
