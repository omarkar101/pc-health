import React, { useState,useEffect } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Table from "./Table";
import Services from "./Services";
import Settings from "./Settings";
import Login from "./Login";
import Nav from "./Nav";
import Register from "./Register";
import './style.css'
import ProtectedRoute from "./ProtectedRoute";
import ChangePass from "./ChangePass";
import ForgetPassword from "./ForgetPassword";
import ForgetPassword_ID from "./ForgetPassword_ID";
import NewPassword from "./NewPassword";
import Stats from "./Stats";
import ResetPass from "./ResetPass"
import { CgWindows } from "react-icons/cg";
import Error from './Error'
import Home from './Home'
import AboutUs from './AboutUs'
function App() {
  const [token, setToken] = useState();
  return (
    <>
      <Router>
        <main>
          <Switch>
            <Route exact path = '/aboutus' component={AboutUs}/>
            <Route path = "/home" exact component={Home}/>
            <Route path="/" exact component={Login}>
              <Login setToken={setToken} />
            </Route>
            <ProtectedRoute
              exact
              path="/table"
              render={(props) => <Table {...props} />}
            >
              <Table />
            </ProtectedRoute>
            <Route
              exact
              path="/error"
              render={(props) => <Error {...props} />}
            >
              <Error />
            </Route>
            <ProtectedRoute path="/Stats/:id" component={Stats} />
            <ProtectedRoute path="/ResetPass" component={ResetPass} />
            <ProtectedRoute path="/table/:id" component={Services} />
            <ProtectedRoute path="/ChangePass" component={ChangePass} />
            <Route path="/register" component={Register} />
            <Route path="/ForgetPassword" component={ForgetPassword} />
            <Route path="/forgotpassword/Code" component={ForgetPassword_ID} />
            <Route path="/NewPassword" component={NewPassword} />
          </Switch>
        </main>
      </Router>
    </>
  );
}

export default App;