import React, { useState } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import { AiOutlineMail } from 'react-icons/ai';
import "./Login.css";

export default function ForgetPassword() {
  const [credentialUsername, setUsername] = useState("");
  const [result, setResult] = useState()
  const submit = async (e) => {
    e.preventDefault()
    const params = decodeURIComponent(new URLSearchParams({
      credentialUsername,
    }));
    const response = await fetch(
      `https://pc-health.azurewebsites.net/Admin/ForgetPasswordUsername?${params}`,
      {
        method: "POST",
      }
    )
    const ans = await response.json()
    setResult(ans)
    setUsername("")
  };
  if (result === true) {
    localStorage.setItem("Email", credentialUsername);
    return <Redirect to="/forgotpassword/Code" />
  }
  return (
    <div className="div_design">
      <form onSubmit={submit} className="forgot_pass_form_container">
        <h2 className="h1_drp">Reset Password</h2>


        {result === false ? <p className="failed_email_in_forgot_password">
          The email you entered does not exist
          </p> : <p className="forgot_pass_message">
          Verify your identity using your Email Address.
        </p>}
        <div className="input-icon">
          <AiOutlineMail className="icon" />
          <input
            type="email"
            className="design_input"
            value={credentialUsername}
            placeholder="Email"
            required
            onChange={(e) => setUsername(e.target.value)}>
          </input>
        </div>

        <button className="login_button" type="submit">Submit</button>
        <div className="navigations_login">
          <Link to="/Login" className="links">
            cancel
            </Link>
        </div>
      </form>
    </div>
  );
}