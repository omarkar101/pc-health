import React, { useState } from "react";
import "./Login.css";
import { Redirect } from "react-router";



function Login(prop: { setToken: (token) => void }) {
  const [CredentialsUsername, setUname] = useState("");
  const [CredentialsPassword, setPassword] = useState("");
  const [redirect, setRedirect] = useState("");
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
    prop.setToken(token)
    setRedirect(token);
  }
  if (redirect !== "false" && redirect !== "") { return <div><Redirect to="/table" /></div> }
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
        <input type="email" className="design_input" placeholder="Email" required
          onChange={(e) => setUname(e.target.value)}>
        </input>
        <input type="password" className="design_input" placeholder="Password" required onChange={(e) => setPassword(e.target.value)} />
        <button className="login_button" type="submit">
          Log in
        </button>
      </form>
    </div>)

}
export default Login