import React, { useState } from 'react'
import { Redirect } from 'react-router';
import "./Login.css"

export default function NewPassword() {
  const [newPass, setNewPass] = useState("")
  const [ConfirmedPassword, setConfirmed] = useState("");
  const [result, setResult] = useState()
const [valid,setValid]=useState()

 const handlePassword = (e) => {
   e.preventDefault();
   var pass = e.target.value;
   setNewPass(pass);
   var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([!@#\$%\^&\*]*)(?=.{8,})^/;
   var test = reg.test(pass);
   if (test === true) {
     setValid(true); 
   }
   else{setValid(false)}
 };



  const submit = async (e) => {
    e.preventDefault()
    const response = await fetch(
      `https://pc-health.azurewebsites.net/Admin/ForgetPasswordChange?credentialUsername=${localStorage.getItem("Email")}&code=${localStorage.getItem("code")}&newPassword=${newPass}`, {
      method: "POST"
    }
    );
    const ans = await response.json();
    setResult(ans)
    setNewPass("")
    setConfirmed("")
  }
  if (result === true) {
    localStorage.removeItem("Email")
    localStorage.removeItem("code")
    localStorage.removeItem("token")
    return <Redirect to='/' />
  }
  return (
    <div className="div_design">
      <form className="forgot_pass_form_container" onSubmit={submit}>
        <h2 className="h1_d">Reset Password</h2>
        {newPass.length === 0 ? (
          <p className="weak_password">
            Passwords should be at least 8 character long, have at least one
            uppercase and one lowercase character, and must include numbers. You can't submit if your password does not follow these rules.
          </p>
        ) : (newPass !== ConfirmedPassword && newPass.length > 0) ? (
          <p className="rfailed_register">Passwords do not match</p>
        ) : (
          <>
            <br />
            <br />
          </>
        )}
        <p className="new_pass_message">Enter your new password:</p>

        <div className="input-icon">
          <input
            className="no_icon_design_input"
            type="password"
            value={newPass}
            placeholder="New Password"
            onChange={(e) => handlePassword(e)}
          />
        </div>
        <div className="input-icon">
          <input
            className="no_icon_design_input"
            type="password"
            value={ConfirmedPassword}
            placeholder="Confirm Password"
            onChange={(e) => setConfirmed(e.target.value)}
          />
        </div>
        <button className="login_button" type="submit" disabled={ConfirmedPassword!==newPass && !valid || newPass.length===0}>
          Submit
        </button>
      </form>
    </div>
  );
}