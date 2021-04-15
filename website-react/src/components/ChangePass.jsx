import React, {useState  }from 'react'
import {Redirect} from 'react-router-dom' 

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
      <div>
        <h1>Change Password</h1>
        <form onSubmit={submit}>
          <input
            type="email"
            placeholder="Email"
            onChange={(e) => setUsername(e.target.value)}
          />
          <input
            type="password"
            placeholder="Old Password"
            onChange={(e) => setOldPassword(e.target.value)}
          />
          <input
            type="password"
            placeholder="New Password"
            onChange={(e) => setNew(e.target.value)}
          />
          <input
            type="password"
                    placeholder="Confirm new password"
            onChange={(e) => setConfirmed(e.target.value)}
                />
                <button type="submit">Submit</button>
            </form>
            <p>Email/Old-Password is wrong or confirmed password do not match new password</p>
      </div>
    );}
    return (
      <div>
        <h1>Change Password</h1>
        <form onSubmit={submit}>
          <input
            type="text"
            placeholder="Email"
            onChange={(e) => setUsername(e.target.value)}
          />
          <input
            type="password"
            placeholder="Old Password"
            onChange={(e) => setOldPassword(e.target.value)}
          />
          <input
            type="password"
            placeholder="New Password"
            onChange={(e) => setNew(e.target.value)}
          />
          <input
            type="password"
            placeholder="Confirm new password"
            onChange={(e) => setConfirmed(e.target.value)}
          />
          <button
            type="submit">
            Submit
          </button>
        </form>
      </div>
    );
}

export default ChangePass