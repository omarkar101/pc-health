import React, { useState } from 'react';
import { Redirect } from 'react-router';
import "./RegisterStyle.css"
import { Link } from "react-router-dom";
import { AiOutlineMail } from 'react-icons/ai'
import { RiLockPasswordFill } from 'react-icons/ri'
import { ImKey } from 'react-icons/im'
import { FaUserAlt } from 'react-icons/fa'

export default function Register() {
  const [AdminFirstName, setFName] = useState("")
  const [AdminLastName, setLName] = useState("");
  const [CredentialsUsername, setUName] = useState("");
  // const [email, setEmail] = useState("");
  const [CredentialsPassword, setPassword] = useState("");
  const [account, setAccount] = useState()
  const [validPass, setValidPass] = useState(false)

  const handlePassword = (e) => {
    e.preventDefault()
    var pass = e.target.value
    var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([!@#\$%\^&\*]*)(?=.{8,})^/
    var test = reg.test(pass)
    if (test === true) { setPassword(pass); setValidPass(true); console.log(validPass) }
    else { setValidPass(false); console.log(validPass) }
  }



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
  // if (account === false) {}
  return (
    <div className="rdiv_design">
      <form className="rform_container" onSubmit={(e) => submit(e)}>
        <h2 className="rh1_d">Sign up</h2>
        {/* <p className="rfailed_register">An account with this email already exists!</p> */}

        {(validPass === true || validPass === null) ? (
        account !== false ? (
          <><br/><br/></>
        ) : (
          <p className="rfailed_register">
            An account with this email already exists!
          </p>
        )
        )
         : (
          <p className="weak_password">
            Passwords should be at least 8 character long, have at least one uppercase and one lowercase character, and must include numbers
          </p>
        )}
        {/* {account !== false ? (
          ""
        ) : (
          <p className="rfailed_register">
            An account with this email already exists!
          </p>
        )} */}

        <div className="rdiv1">
          <div className="rinput-icon">
            <FaUserAlt className="ricon" />
            <input
              type="text"
              placeholder="FirstName"
              className="rdesign_input"
              required
              onChange={(e) => setFName(e.target.value)}
            />
          </div>

          <div className="rinput-icon">
            <input
              type="text"
              placeholder="Last Name"
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
              className="rdesign_input"
              required
              onChange={(e) => setUName(e.target.value)}
            />
          </div>

          {/* <input type="email" platextceholder="Email Address" required onChange={e=>setEmail(e.target.value)  }/> */}

          <div className="rinput-icon">
            <ImKey className="ricon" />
            <input
              type="password"
              placeholder="Password"
              className="rdesign_input"
              required
              onChange={(e) => handlePassword(e)}
            ></input>
          </div>
        </div>

        <button class="rlogin_button" type="submit" disabled={!validPass}>
          Register
        </button>

        <hr />

        {/* <div className="navigations_login"></div> */}

        <div className="rplinks">Already have an account?
            <Link to="/" className="rlinks">
            Login
            </Link>
        </div>
        {/* </div> */}
      </form>
    </div>
  );
}