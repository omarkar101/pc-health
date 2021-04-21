import React, { useState } from 'react'
import { Redirect } from 'react-router'
import "./Login.css";
import { Link } from "react-router-dom";
import { AiOutlineMail } from 'react-icons/ai'
import { RiLockPasswordFill } from 'react-icons/ri'
import { ImKey } from 'react-icons/im'

function ResetPass() {
  // const [CredentialUsername, setEmail] = useState("")
  // const [CredentialPassword, setPassword] = useState("")
  const [result, setResult] = useState()
  const [CredentialsUsername, setEmail] = useState("")
  const [CredentialsPassword, setPassword] = useState("")

  async function submit(e) {
    e.preventDefault();
    const response = await fetch(
      "http://pc-health.somee.com/Admin/ResetPcCredentialPassword",
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
  }

  if (result === true) { return <Redirect to="/table" />; }
  if (result === false) {
    return (

      <div className="div_design">
        <form className="form_container" onSubmit={submit}>
          <h2 className="h1_d">Reset Password</h2>
          {/* <p className="forgot_pass_message">Enter your credentials here </p> */}
          <p className="failed_login">Account credentials are incorrect</p>

          <div className="input-icon">
            <RiLockPasswordFill className="icon" />
            <input
              className="design_input"
              type="email"
              placeholder="Email"
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>

          <div className="input-icon">
            <ImKey className="icon" />
            <input
              className="design_input"
              type="password"
              placeholder="Password"
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>

          <button className="login_button" type="submit">Submit</button>
          {/* <p style={{ color: "red" }}>Account credentials are incorrect</p> */}



          <div className="navigations_login">
            <Link to="/table" className="links">
              cancel
            </Link>
          </div>
        </form>
      </div>

    );
  }
  return (
    <div className="div_design">
      <form className="form_container" onSubmit={submit}>
        <h2 className="h1_d">Reset Password</h2>
        <p className="forgot_pass_message">Enter your credentials here </p>

        <div className="input-icon">
          <RiLockPasswordFill className="icon" />
          <input
            className="design_input"
            type="email"
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>

        <div className="input-icon">
          <ImKey className="icon" />
          <input
            className="design_input"
            type="password"
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