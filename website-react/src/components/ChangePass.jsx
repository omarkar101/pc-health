import React, {useState  }from 'react'
import {Redirect} from 'react-router-dom' 
import "./RegisterStyle.css"
import { Link } from "react-router-dom";

function ChangePass() {
    const [CredentialUsername, setUsername] = useState("")
    const [OldPassword, setOldPassword] = useState("")
    const [NewPassword, setNew] = useState("")
    const [ConfirmedPassword,setConfirmed]=useState("")
  const [result, setResult] = useState()
  const [error,setError]=useState("")
    const submit = async (e) => {
        e.preventDefault()
        await fetch(
            "http://pc-health.somee.com/Admin/ChangePassword", //You receive true or false, delete token when true and logout
            {
            method:"POST",
              headers: {"Content-Type":"application/json",Authorization: `Bearer ${localStorage.getItem("token")}`},
              body: JSON.stringify({
                CredentialUsername,
                OldPassword,
                NewPassword,
              }),
            }
          )
          .then((res) => {
            setResult(res.redirected);
          });
  }
  const checkValidation = (e) => {
    setConfirmed(e.target.value)
    if (NewPassword !== ConfirmedPassword) {
      setError("Confirm password should match the new password"); setResult(false)
    } else { setError("") }
  }
    if (result===true){
        return <Redirect to='/table'/>
    }
    return (
      <div className="rdiv_design">
        <form className="rform_container" onSubmit={submit}>
          <h1 className="rh1_d">Change Password</h1>
          <div className="div1">
            <input
              className="rdesign_input"
              type="text"
              placeholder="Email"
              onChange={(e) => setUsername(e.target.value)}
            />
            <input
              className="rdesign_input2"
              type="password"
              placeholder="Old Password"
              onChange={(e) => setOldPassword(e.target.value)}
            />
          </div>
          <div className="div2">
            <input
              className="rdesign_input"
              type="password"
              placeholder="New Password"
              onChange={(e) => setNew(e.target.value)}
            />
            <input
              className="rdesign_input2"
              type="password"
              placeholder="Confirm new password"
              onChange={(e) => checkValidation(e)}
            />
          </div>
          <button className="rlogin_button" type="submit">
            Submit
          </button>
          {(result === false) ? <p style={{ "color": "white" }}>Email and old password do not match</p> : ""}
          <p style={{ "color": "white" }}>{error}</p>
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

export default ChangePass