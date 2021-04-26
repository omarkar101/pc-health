import React, { useState } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import Table from "./Table";
import Services from "./Services";
import Login from "./Login";
import Register from "./Register";
import ProtectedRoute from "./ProtectedRoute";
import ChangePass from "./ChangePass";
import ForgetPassword from "./ForgetPassword";
import ForgetPassword_ID from "./ForgetPassword_ID";
import NewPassword from "./NewPassword";
import Stats from "./Stats";
import ResetPass from "./ResetPass"
import AboutUs from "./AboutUs";
import Home from "./Home";
import './style.css';

function App() {
  const [token, setToken] = useState();
  return (
    <>
      <Router>
        <main>
          <Switch>
            <Route exact path="/AboutUs" component={AboutUs} />
            <Route exact path="/" component={Home} />
            <Route path="/Login" exact component={Login}>
              <Login setToken={setToken} />
            </Route>
            <ProtectedRoute exact path="/table" render={props => (<Table {...props} />)}>
              <Table />
            </ProtectedRoute>
            <ProtectedRoute path="/Stats/:id/:name" component={Stats} />
            <ProtectedRoute path="/ResetPass" component={ResetPass} />
            <ProtectedRoute path="/table/:id/:name" component={Services} />
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