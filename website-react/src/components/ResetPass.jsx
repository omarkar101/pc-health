import React, { useState } from 'react'
import { Redirect } from 'react-router'
import "./Login.css";
import "./RegisterStyle.css"
import { Link } from "react-router-dom";

function ResetPass() {
    const [CredentialUsername, setEmail] = useState("")
    const [CredentialPassword, setPassword] = useState("")
    const [result,setResult] = useState()
    const submit = async (e) => {
        e.preventDefault()
        const response = await fetch(
            "http://pc-health.somee.com/Admin/ResetPcCredentialPassword", {
                method: "POST",
                headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
                body: JSON.stringify({
                    CredentialUsername,
                    CredentialPassword
                })
          }
        )
        const ans = await response.json()
        setResult(ans)
    }
    if (result === true) { return <Redirect to="/" />; }
    if (result === false) {
        return (
          <div className="rdiv_design">
            <form className="form_container" onSubmit={submit}>
            <h1 className="h1_d">Please insert your email and password</h1>
            <input
            className="design_input"
                type="email"
                placeholder="Email"
                onChange={(e) => setEmail(e.target.value)}
              />
              <input
              className="design_input"
                type="password"
                placeholder="Password"
                onChange={(e) => setPassword(e.target.value)}
                    />
                    <button className="login_button" type="submit">Submit</button>
                    <p className="failed_login">Account credentials are incorrect</p>
                </form>
         </div>
        );}
    return (
      <div className="rdiv_design">
        <form className="form_container" onSubmit={submit}>
        <h1 className="h1_d">Please insert your email and password</h1>
          <input
          className="design_input"
            type="email"
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
          className="design_input"
            type="password"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
          />
          <button className="login_button" type="submit">Submit</button>
        </form>
        <div className="navigations_login">
        {/* <ul> */}
        {/* <div className="plinks">Already have an account? */}
  <Link to="/table" className="links">
            cancel
  </Link>
        {/* </div> */}
      </div>
      </div>
    );
}

export default ResetPass