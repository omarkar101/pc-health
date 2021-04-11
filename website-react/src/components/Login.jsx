import React, { useState } from "react";
import "./Login.css";
import { Redirect } from "react-router";



function Login(prop: { setToken:(token)=>void}) {
    const [CredentialsUsername, setUname] = useState("");
    const [CredentialsPassword, setPassword] = useState("");
    const [redirect, setRedirect] = useState("");
    // function validateForm() {
    //     return CredentialsUsername.length > 0 && CredentialsPassword.length > 0;
    // }
    const submit = async (e) => {
        e.preventDefault();
        const response = await fetch(
          "http://omarkar1011-001-site1.dtempurl.com/Admin/Login",
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
      <div>
        <form onSubmit={submit}>
          <h1 className="h3 mb-3 fw-normal">Please Login</h1>
          <input
            type="text"
            className="form-control"
            placeholder="Username"
            required
            onChange={(e) => setUname(e.target.value)}
          ></input>
          <input
            type="password"
            className="form-control"
            placeholder="Password"
            required
            onChange={(e) => setPassword(e.target.value)}
          />
          <button className="w-100 btn btn-lg btn-primary" type="submit">
            Log in
      </button>
        </form>
        <p>The username/password you entered are incorrect</p></div>);}
    return (
        <form onSubmit={submit}>
            <h1 className="h3 mb-3 fw-normal">Please Login</h1>
            <input type="text" className="form-control" placeholder="Username" required
            onChange={(e)=>setUname(e.target.value)}></input>
            <input type="password" className="form-control" placeholder="Password" required onChange={(e)=>setPassword(e.target.value)}/>
            <button className="w-100 btn btn-lg btn-primary" type = "submit">Log in</button>
        </form>)
}
export default Login