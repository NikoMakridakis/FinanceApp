import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

//import AuthService from './services/AuthService';
import Welcome from './pages/Welcome/Welcome';
import Home from './pages/Home/Home';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';
import Budget from './pages/Budget/Budget';

function App() {


    //const [userIsInLocalStorage, setUserIsInLocalStorage] = useState(false);

    //async function checkUserIsInLocalStorage() {
    //    try {
    //        const user = AuthService.getCurrentUser();
    //        if (user !== null) {
    //            return setUserIsInLocalStorage(true);
    //        } else {
    //            return setUserIsInLocalStorage(false);
    //        }
    //    } catch (error) {
    //        console.log(error);
    //        }
    //}

    //useEffect(() => {
    //    checkUserIsInLocalStorage();
    //}, []);

    //<Route exact path='/login' render={(props) => (
    //    <Login {...props} checkUserIsInLocalStorage={checkUserIsInLocalStorage} />
    //)} />
    //<Route exact path='/welcome' render={() => (userIsInLocalStorage === true ? (<Welcome />) : (<Redirect to="/login" />))} />
    //<Route exact path='/budget' render={() => (userIsInLocalStorage === true ? (<Budget />) : (<Redirect to="/login" />))} />

    return (
        <Router>
            <Switch>
                <Route exact path={["/home", "/"]} component={Home} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/register' component={Register} />
                <Route exact path='/welcome' component={Welcome} />
                <Route exact path='/budget' component={Budget} />
            </Switch>
        </Router>
    )
}

export default App;