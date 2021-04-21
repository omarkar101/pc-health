import React, { useState } from 'react'
import { Redirect } from 'react-router';
import './Login.css';
import { RiLockPasswordFill } from 'react-icons/ri'


function ForgetPassword_ID() {
    const [code, setCode] = useState("");
    const [result, setResult] = useState();
    const submit = async (e) => {
        e.preventDefault();
        const response = await fetch(`http://pc-health.somee.com/Admin/ForgetPasswordUniqueIdCheck?credentialUsername=${localStorage.getItem("Email")}&code=${code}`, { method: "POST" })
        const ans = await response.json()
        setResult(ans)
    }
    if (result) {
        localStorage.setItem("code", code)
        return <Redirect to="/NewPassword" />
    }
    if (result === false) {
        return (
            <div className="div_design">
                <form className="forgot_pass_form_container" onSubmit={submit}>
                    <h2 className="h1_d">Reset Password</h2>
                    <p className="forgot_pass_message">You will receive a verification code by email shortly.
                    <br />Please enter your code below.
                    </p>

                    <div className="input-icon">
                        <RiLockPasswordFill className="icon" />
                        <input
                            type="text"
                            className="design_input"
                            placeholder="Code"
                            required
                            onChange={(e) => setCode(e.target.value)} />
                    </div>
                    <button className="login_button" type="submit">Submit</button>
                    <p className="wrong_code" >The code entered is incorrect!</p>
                </form>
            </div>
        );
    }
    return (
        <div className="div_design">
            <form className="forgot_pass_form_container" onSubmit={submit}>
                <h2 className="h1_d">Reset Password</h2>
                <p className="forgot_pass_message">You will receive a verification code by email shortly.
                    <br />Please enter your code below.
                    </p>

                <div className="input-icon">
                    <RiLockPasswordFill className="icon" />
                    <input
                        type="text"
                        className="design_input"
                        placeholder="Code"
                        required
                        onChange={(e) => setCode(e.target.value)} />
                </div>
                <button className="login_button" type="submit">Submit</button>
            </form>
        </div>
    )
}

export default ForgetPassword_ID