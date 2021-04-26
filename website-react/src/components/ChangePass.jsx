import React, { useState } from 'react'
import { Redirect } from 'react-router-dom'
import { Link } from "react-router-dom";
import { ImKey } from 'react-icons/im'
import { FaUserAlt } from 'react-icons/fa'
import "./RegisterStyle.css"

function ChangePass() {
  const [CredentialUsername, setUsername] = useState("")
  const [OldPassword, setOldPassword] = useState("")
  const [NewPassword, setNew] = useState("")
  const [ConfirmedPassword, setConfirmed] = useState("")
  const [result, setResult] = useState()
  const [validPass, setValidPass] = useState(false);

  const handlePassword = (e) => {
    e.preventDefault();
    var pass = e.target.value;
    setNew(pass);
    var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([!@#\$%\^&\*]*)(?=.{8,})^/;
    var test = reg.test(pass);
    if (test === true) {
      setValidPass(true);
    }
    else { setValidPass(false) }
  };

  const submit = async (e) => {
    e.preventDefault();

    const response =
      await fetch(
        "https://pc-health.azurewebsites.net/Admin/ChangePassword",
        {
          method: "POST",
          headers: { "Content-Type": "application/json", Authorization: `Bearer ${localStorage.getItem("token")}` },
          body: JSON.stringify({
            CredentialUsername,
            OldPassword,
            NewPassword,
          }),
        }
      );

    const res = await response.json()
    setResult(res)
  }
  if (result === true) {
    return <Redirect to='/table' />
  }
  return (
    <div className="rdiv_design">
      <form className="rform_container" onSubmit={submit}>
        <img className="logologin" src="/images/logo3.png" alt="" />
        <h2 className="rh1_d">Change Password</h2>

        {
          NewPassword.length > 0 && NewPassword !== null ? (
            validPass === false ?
              <p className="weak_password">
                Passwords should be at least 8 character long, have at least one
                uppercase and one lowercase character, and must include numbers
          </p> :
              (
                (NewPassword !== ConfirmedPassword && ConfirmedPassword.length > 0) ?
                  <p className="rfailed_register">Passwords do not match</p>
                  : <p className="rfailed_register"> &nbsp; </p>
              )
          )
            : <p className="rfailed_register"> &nbsp; </p>
        }

        <div className="rdiv1">
          <div className="rinput-icon">
            <FaUserAlt className="ricon" />
            <input
              className="rdesign_input"
              type="text"
              placeholder="Email"
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          <div className="rinput-icon">
            <ImKey className="ricon" />
            <input
              className="rdesign_input"
              type="password"
              placeholder="Old Password"
              onChange={(e) => setOldPassword(e.target.value)}
            />
          </div>

        </div>


        <div className="rdiv2">
          <div className="rinput-icon">
            <input
              className="rno_icon_design_input"
              type="password"
              placeholder="New Password"
              onChange={(e) => handlePassword(e)}
            />
          </div>

          <div className="rinput-icon">
            <input
              className="rno_icon_design_input"
              type="password"
              placeholder="Confirm new password"
              onChange={(e) => setConfirmed(e.target.value)}
            />
          </div>
        </div>
        <button className="register_button" type="submit" disabled={NewPassword !== ConfirmedPassword && !validPass}>
          Submit
        </button>
        <hr />

        <div className="rplinks">
          <Link to="/table" className="rlinks">
            cancel
          </Link>
        </div>


      </form>

    </div>
  );
}

export default ChangePass