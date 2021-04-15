import React, { useState } from 'react'
import { Redirect } from 'react-router'

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
          <>
            <h1>Please insert your email and password</h1>
            <form onSubmit={submit}>
              <input
                type="email"
                placeholder="Email"
                onChange={(e) => setEmail(e.target.value)}
              />
              <input
                type="password"
                placeholder="Password"
                onChange={(e) => setPassword(e.target.value)}
                    />
                    <button type="submit">Submit</button>
                </form>
                <p>Account credentials are incorrect</p>
          </>
        );}
    return (
      <>
        <h1>Please insert your email and password</h1>
        <form onSubmit={submit}>
          <input
            type="email"
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
            type="password"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
          />
          <button type="submit">Submit</button>
        </form>
      </>
    );
}

export default ResetPass