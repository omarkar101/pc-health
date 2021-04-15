import React, { useState } from 'react'
import { Redirect } from 'react-router';
// import {DecodeURIComponent} from 'react';
import './Login.css';

function ForgetPassword_ID() {
    const [code, setCode] = useState("");
    const [result, setResult] = useState();
    const submit = async (e) => {
        e.preventDefault();
        // const response = await fetch(`http://pc-health.somee.com/Admin/ForgetPasswordUniqueIdCheck?credentialUsername=${localStorage.getItem("Email")}&code=${code}`, { method: "POST" })
        const response = await fetch(`http://pc-health.somee.com/Admin/ForgetPasswordUniqueIdCheck?credentialUsername=${localStorage.getItem("Email")}&code=${code}`, { method: "POST" })
        // const response = await fetch(`http://pc-health.somee.com/Admin/ForgetPasswordUniqueIdCheck?credentialUsername=${decodeURIComponent(localStorage.getItem("email"))}&code=${decodeURIComponent(code)}`, { method: "POST" })
        const ans = await response.json()
        setResult(ans)
    }
    if (result) {
        localStorage.setItem("code", code)
        return <Redirect to="/NewPassword" />
    }
    if (result === false) {
        return (
            <div className="div_design forgot_page">
                <form className="form_container" onSubmit={submit}>
                    <p className="forgot_pass">You will receive a unique code by email shortly.</p>
                    <p style={{ color: "white" }}>Please enter your code below</p>
                    <input
                        className="design_input"
                        type="text"
                        placeholder="Code"
                        onChange={(e) => setCode(e.target.value)}
                    />
                    <button className="login_button" type="submit">Submit</button>
                    <p style={{ color: "indianred" }}>The code entered is incorrect</p>
                </form>
            </div>
        );
    }
    return (
        <div className="div_design forgot_page">
            <form className="form_container" onSubmit={submit}>
                <p className="forgot_pass">You will receive a unique code to your email shortly</p>
                <p style={{ color: "white" }}>Please enter your code below</p>
                <input type="text"
                    className="design_input"
                    placeholder="Unique Code" onChange={e => setCode(e.target.value)} />
                <button className="login_button" type="submit">Submit</button>
            </form>
        </div>
    )
}

export default ForgetPassword_ID