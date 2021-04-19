import React, { useState } from "react";
import "./Login.css";
import { Redirect, Route} from "react-router";
import { BrowserRouter as Router } from "react-router-dom";
import {Link} from 'react-router-dom'
import Nav from './Nav'

function Login(prop: { setToken: (token) => void }) {
  const [CredentialsUsername, setUname] = useState("");
  const [CredentialsPassword, setPassword] = useState("");
  const [redirect, setRedirect] = useState("");
  const [token,setToken]=useState("")

  // function validateForm() {
  //     return CredentialsUsername.length > 0 && CredentialsPassword.length > 0;
  // }
  const submit = async (e) => {
    e.preventDefault();
    const response = await fetch(
      "http://pc-health.somee.com/Admin/Login",
      {
        method: "POST",
        // authorization: "Bearer Token",
        headers: { "Content-Type": "application/json" },
        // credentials: "include",
        body: JSON.stringify({
          CredentialsUsername,
          CredentialsPassword,
        }),
      }
    );
    const token = await response.text();
    localStorage.setItem("token", token)
    localStorage.setItem("interval",3)
    setRedirect(token);
  }
  if (redirect !== "false" && redirect !== "") {
    return (<Redirect to="/table"/>
    ); }
  if (redirect === 'false') {
    return (
      <div className="div_design">
        <form className="form_container" onSubmit={submit}>
          <h1 className="h1_d">Please Login</h1>
          <input
            type="email"
            className="design_input"
            placeholder="Email"
            required
            onChange={(e) => setUname(e.target.value)}
          />
          <input
            type="password"
            className="design_input"
            placeholder="Password"
            required
            onChange={(e) => setPassword(e.target.value)}
          />
          <button className="login_button" type="submit">
            Log in
          </button>
          <p className="failed_login">The username/password you entered is incorrect!</p>
        </form>
      </div>);
  }
  return (
    <div className="div_design">
      <form className="form_container" onSubmit={submit}>
        <h1 className="h1_d">Please Login</h1>
        <input
          type="email"
          className="design_input"
          placeholder="Email"
          required
          onChange={(e) => setUname(e.target.value)}
        ></input>
        <input
          type="password"
          className="design_input"
          placeholder="Password"
          required
          onChange={(e) => setPassword(e.target.value)}
        />
        <button className="login_button" type="submit">
          Log in
        </button>
      </form>
      <nav>
        <div className="navigations_login">
          <div className="plinks">
            Forgot Password?
            <Link to="/ForgetPassword" className="links">
              Click here
            </Link>
          </div>
          <br />
          <div className="plinks">
            Don't have an account?
            <Link to="/register" className="links">
              Register
            </Link>
          </div>
          <br />
          {/* </ul> */}
        </div>
      </nav>
    </div>
  );

}
export default Login