import React, { useState } from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
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
import { Beforeunload } from 'react-beforeunload';

function App() {
  const [token, setToken] = useState();

  return (
    <>
      <Router>
        <main>
          <Route path="/" exact component={Login}>
            <Login setToken={setToken} />
          </Route>

          <ProtectedRoute path="/Stats/:id" component={Stats} />
          <ProtectedRoute path="/ResetPass" component={ResetPass} />
          <ProtectedRoute path="/table/:id" component={Services} />
          <ProtectedRoute path="/ChangePass" component={ChangePass}/>
          <Route path="/register" component={Register} />
          <Route  path="/ForgetPassword" component={ForgetPassword}/>
          <Route  path="/forgotpassword/Code" component={ForgetPassword_ID}/>
          <Route path="/NewPassword" component={NewPassword}/>
        </main>
      </Router>
    </>
  );
}

export default App;
