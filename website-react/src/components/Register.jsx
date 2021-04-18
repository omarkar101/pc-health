import React, { useState } from 'react';
import { Redirect } from 'react-router';
import "./RegisterStyle.css"
import { Link } from "react-router-dom";

export default function Register() {
  const [AdminFirstName, setFName] = useState("")
  const [AdminLastName, setLName] = useState("");
  const [CredentialsUsername, setUName] = useState("");
  // const [email, setEmail] = useState("");
  const [CredentialsPassword, setPassword] = useState("");
  const [account, setAccount] = useState()



  const submit = async (e) => {
    e.preventDefault();
    const response = await fetch(
      "http://pc-health.somee.com/Admin/Create",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        // credentials:'true',
        body: JSON.stringify({
          AdminFirstName,
          AdminLastName,
          CredentialsUsername,
          CredentialsPassword,
        }),
      }
    );
    const answer = await response.json();
    setAccount(answer)


  }
  if (account) { return <Redirect to="/" />; }
  if (account === false) {
    return (
      <div className="rdiv_design">
        <form className="rform_container" onSubmit={(e) => submit(e)}>
          <h1 className="rh1_d">Please Register</h1>
          <div className="div1">
          <input
            type="text"
            placeholder="FirstName"
            className="rdesign_input"
            required
            onChange={(e) => setFName(e.target.value)}
          />
          <input
            type="text"
            placeholder="Last Name"
            className="rdesign_input2"
            required
            onChange={(e) => setLName(e.target.value)}
          ></input>
          </div>
          <div className="div2">
          <input
            type="email"
            placeholder="Email"
            className="rdesign_input"
            required
            onChange={(e) => setUName(e.target.value)}
          />
          <input
            type="password"
            placeholder="Password"
            className="rdesign_input2"
            required
            onChange={(e) => setPassword(e.target.value)}
          ></input>
          </div>
          <button class="rlogin_button" type="submit">
            Register
          </button>
        <p className="failed_register">An account with this email already exists!</p>
        </form>
        <div className="navigations_login">
        {/* <ul> */}
        <div className="plinks">Already have an account?
  <Link to="/" className="links">
            Login
  </Link>
        </div>
      </div>
      </div>
    );
  }
  return (
    <div className="rdiv_design">
      <form className="rform_container" onSubmit={(e) => submit(e)}>
        <h1 className="rh1_d">Please Register</h1>
        <div className="div1">
          <input
            type="text"
            placeholder="FirstName"
            className="rdesign_input"
            required
            onChange={(e) => setFName(e.target.value)}
          />
          <input
            type="text"
            placeholder="Last Name"
            className="rdesign_input2"
            required
            onChange={(e) => setLName(e.target.value)}
          ></input>
        </div>
        <div className="div2">
          <input
            type="text"
            placeholder="Username"
            className="rdesign_input"
            required
            onChange={(e) => setUName(e.target.value)}
          />
          {/* <input type="email" platextceholder="Email Address" required onChange={e=>setEmail(e.target.value)  }/> */}
          <input
            type="password"
            placeholder="Password"
            className="rdesign_input2"
            required
            onChange={(e) => setPassword(e.target.value)}
          ></input>
        </div>
        <button class="rlogin_button" type="submit">
          Register
            </button>
      </form>
      <div className="navigations_login">
        {/* <ul> */}
        <div className="plinks">Already have an account?
  <Link to="/" className="links">
            Login
  </Link>
        </div>
      </div>
    </div>
  );
}
