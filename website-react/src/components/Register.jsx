import React, { useState } from 'react';
import { Redirect } from 'react-router';
import { Link } from "react-router-dom";
import { AiOutlineMail } from 'react-icons/ai'
import { ImKey } from 'react-icons/im'
import { FaUserAlt } from 'react-icons/fa'
import "./RegisterStyle.css"

export default function Register() {
  const [AdminFirstName, setFName] = useState("")
  const [AdminLastName, setLName] = useState("");
  const [CredentialsUsername, setUName] = useState("");
  const [CredentialsPassword, setPassword] = useState("");
  const [account, setAccount] = useState("")
  const [validPass, setValidPass] = useState(false)

  const handlePassword = (e) => {
    e.preventDefault()
    var pass = e.target.value
    setPassword(pass)
    var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([!@#\$%\^&\*]*)(?=.{8,})^/
    var test = reg.test(pass)
    if (test === true) { setValidPass(true); }
    else { setValidPass(false); }
  }


  const submit = async (e) => {
    e.preventDefault();
    const response = await fetch(
      "https://pc-health.azurewebsites.net/Admin/Create",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          AdminFirstName,
          AdminLastName,
          CredentialsUsername,
          CredentialsPassword,
        }),
      }
    );
    const answer = await response.text();
    setAccount(answer)
    setUName("")
    setFName("")
    setLName("")
    setPassword("")

  }

  if (account === "Success") { return <Redirect to="/" />; }
  return (
    <div className="rdiv_design">
      <form className="rform_container" onSubmit={(e) => submit(e)}>
        <img className="logologin" src="/images/logo3.png" alt="" />

        <h2 className="rh1_d">Sign up</h2>
        {CredentialsPassword.length > 0 && CredentialsPassword !== null ?
          (
            validPass === false ?
              <p className="weak_password">
                Passwords should be at least 8 character long, have at least one
                uppercase and one lowercase character, and must include numbers
          </p>
              :
              <p className="rfailed_register"> &nbsp; </p>
          ) :
          (
            account === "Success" || account === "" ? (

              <p className="rfailed_register"> &nbsp; </p>
            ) : (
              <p className="rfailed_register"> {account} </p>
            )
          )
        }

        <div className="rdiv1">
          <div className="rinput-icon">
            <FaUserAlt className="ricon" />
            <input
              type="text"
              placeholder="FirstName"
              value={AdminFirstName}
              className="rdesign_input"
              required
              onChange={(e) => setFName(e.target.value)}
            />
          </div>

          <div className="rinput-icon">
            <input
              type="text"
              placeholder="Last Name"
              value={AdminLastName}
              className="rno_icon_design_input"
              required
              onChange={(e) => setLName(e.target.value)}
            ></input>
          </div>
        </div>

        <div className="rdiv2">
          <div className="rinput-icon">
            <AiOutlineMail className="ricon" />
            <input
              type="email"
              placeholder="Email"
              value={CredentialsUsername}
              className="rdesign_input"
              required
              onChange={(e) => setUName(e.target.value)}
            />
          </div>

          <div className="rinput-icon">
            <ImKey className="ricon" />
            <input
              type="password"
              placeholder="Password"
              value={CredentialsPassword}
              className="rdesign_input"
              required
              onChange={(e) => handlePassword(e)}
            ></input>
          </div>
        </div>

        <button class="register_button" type="submit" disabled={!validPass}>
          Register
        </button>

        <hr />

        <div className="rplinks">
          Already have an account?
          <Link to="/Login" className="rlinks">
            Login
          </Link>
        </div>
      </form>
    </div>
  );
}