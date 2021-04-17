import React, {useState  }from 'react'
import {Redirect} from 'react-router-dom' 
import "./RegisterStyle.css"
import { Link } from "react-router-dom";

function ChangePass() {
    const [CredentialUsername, setUsername] = useState("")
    const [OldPassword, setOldPassword] = useState("")
    const [NewPassword, setNew] = useState("")
    const [ConfirmedPassword,setConfirmed]=useState("")
    const [result,setResult]= useState()
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
            setResult(res.ok);
          });
    }
    if (result){localStorage.removeItem("token")
        return <Redirect to='/' />
    }
    if (result === false || NewPassword!==ConfirmedPassword) {
        return (
      <div className="rdiv_design">
        <form className="rform_container" onSubmit={submit}>
        <h1 className="rh1_d">Change Password</h1>
        <div className="div1">
          <input
          className="rdesign_input"
            type="email"
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
            onChange={(e) => setConfirmed(e.target.value)}
                />
                </div>
                <button className="rlogin_button" type="submit">Submit</button>
            <p className="failed_login">Email/Old-Password is wrong or confirmed password do not match new password</p>
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
    );}
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
            onChange={(e) => setConfirmed(e.target.value)}
          />
          </div>
          <button
          className="rlogin_button"
            type="submit">
            Submit
          </button>
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