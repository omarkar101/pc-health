import React, { useState } from 'react'
import { Redirect } from 'react-router';
import "./Login.css"

export default function NewPassword() {
  const [newPass, setNewPass] = useState("")
  const [ConfirmedPassword, setConfirmed] = useState("");
  const [result, setResult] = useState()
  const submit = async (e) => {
    e.preventDefault()
    const response = await fetch(
      `http://pc-health.somee.com/Admin/ForgetPasswordChange?credentialUsername=${localStorage.getItem("Email")}&code=${localStorage.getItem("code")}&newPassword=${newPass}`, {
      method: "POST"
    }
    );
    const ans = await response.json();
    console.log(ans)
    setResult(ans)
  }
  if (result === true) {
    localStorage.removeItem("Email")
    localStorage.removeItem("code")
    localStorage.removeItem("token")
    return <Redirect to='/' />
  }
  if (result === false || newPass !== ConfirmedPassword) {
    return (
      <div className="div_design">
        <form className="forgot_pass_form_container" onSubmit={submit}>
          <h2 className="h1_d">Reset Password</h2>

          <p className="new_pass_message">
            Enter your new password:
        </p>

          <div className="input-icon">
            <input
              className="no_icon_design_input"
              type="password"
              placeholder="New Password"
              onChange={(e) => setNewPass(e.target.value)}
            />
          </div>

          <div className="input-icon">
            <input
              className="no_icon_design_input"
              type="password"
              placeholder="Confirm Password"
              onChange={(e) => setConfirmed(e.target.value)}
            />
          </div>
          <button className="login_button" type="submit">Submit</button>
          <p className="no_match">Passwords do not match</p>
        </form>

      </div>
    );
  }
  return (
    <div className="div_design">
      <form className="forgot_pass_form_container" onSubmit={submit}>
        <h2 className="h1_d">Reset Password</h2>

        <p className="new_pass_message">
          Enter your new password:
        </p>

        <div className="input-icon">
          <input
            className="no_icon_design_input"
            type="password"
            placeholder="New Password"
            onChange={(e) => setNewPass(e.target.value)}
          />
        </div>
        <div className="input-icon">
          <input
            className="no_icon_design_input"
            type="password"
            placeholder="Confirm Password"
            onChange={(e) => setConfirmed(e.target.value)}
          />
        </div>
        <button className="login_button" type="submit">Submit</button>
      </form>
    </div>
  );
}