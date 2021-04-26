import React, { useState } from 'react'
import { Redirect } from 'react-router'
import { Link } from "react-router-dom";
import { RiLockPasswordFill } from 'react-icons/ri'
import { ImKey } from 'react-icons/im'
import "./Login.css";

function ResetPass() {
  const [result, setResult] = useState()
  const [CredentialsUsername, setEmail] = useState("")
  const [CredentialsPassword, setPassword] = useState("")

  async function submit(e) {
    e.preventDefault();
    const response = await fetch(
      "https://pc-health.azurewebsites.net/Admin/ResetPcCredentialPassword",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify({
          CredentialsUsername,
          CredentialsPassword,
        }),
      }
    );
    const ans = await response.json()
    setResult(ans)
    setPassword("")
    setEmail("")
  }

  if (result === true) { return <Redirect to="/table" />; }
  return (
    <div className="div_design">
      <form className="form_container" onSubmit={submit}>
        <img className="logologin" src="/images/logo3.png" alt="" />
        <h2 className="h1_d">Reset Account Code</h2>
        {result === false ? <><p className="failed_email_in_forgot_password">Account credentials are incorrect.</p><br /></> :

          <p className="forgot_pass_message">Enter your credentials here.<br /> You will receive an email containing the new code.</p>
        }
        <div className="input-icon">
          <RiLockPasswordFill className="icon" />
          <input
            className="design_input"
            type="email"
            value={CredentialsUsername}
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>

        <div className="input-icon">
          <ImKey className="icon" />
          <input
            className="design_input"
            type="password"
            value={CredentialsPassword}
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <button className="login_button" type="submit">Submit</button>

        <div className="navigations_login">
          <Link to="/table" className="links">
            cancel
        </Link>
        </div>
      </form>
    </div>

  );
}

export default ResetPass