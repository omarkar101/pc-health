import React, { useState } from "react";
import { Redirect } from "react-router";
import { Link } from 'react-router-dom'
import { AiOutlineMail } from 'react-icons/ai'
import { ImKey } from 'react-icons/im'
import "./Login.css";


function Login() {
  const [CredentialsUsername, setUname] = useState("");
  const [CredentialsPassword, setPassword] = useState("");
  const [redirect, setRedirect] = useState("");

  const submit = async (e) => {
    e.preventDefault();

    await fetch(
      "https://pc-health.azurewebsites.net/Admin/Login",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          CredentialsUsername,
          CredentialsPassword,
        }),
      }).then((response) => response.text()).then(result => {

        localStorage.setItem("token", result);
        localStorage.setItem("interval", 3);
        setRedirect(result);
      })
    setPassword("")
    setUname("")
  }

  if (redirect !== "false" && redirect !== "") {
    return (<Redirect to="/table" />
    );
  }
  return (
    <div className="div_design">

      <form className="form_container" onSubmit={submit}>
        <img className="logologin" src="/images/logo3.png" alt="" />
        <h2 className="h1_d">Log in to P-See</h2>

        {redirect === 'false' ?
          <p className="failed_login">
            The username/password you entered is incorrect!
          </p> : <p style={{ marginTop: "5.5%" }}>
            &nbsp;
          </p>
        }


        <div className="input-icon">
          <AiOutlineMail className="icon" />
          <input
            type="email"
            className="design_input"
            placeholder="Email"
            value={CredentialsUsername}
            required
            onChange={(e) => setUname(e.target.value)}>
          </input>
        </div>

        <div className="input-icon">
          <ImKey className="icon" />
          <input
            type="password"
            className="design_input"
            placeholder="Password"
            value={CredentialsPassword}
            required
            onChange={(e) => setPassword(e.target.value)} />
        </div>

        <button className="login_button" type="submit">
          Log in
          </button>

        <hr />
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
                Sign up
            </Link>
            </div>
            <br />
          </div>
        </nav>
      </form>
    </div>

  );
}
export default Login