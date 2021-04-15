import axios from "axios";
import React, { useState } from "react";
import { Redirect } from "react-router";
import { Link} from "react-router-dom";
import "./Login.css"

export default function ForgetPassword() {
  const [credentialUsername, setUsername] = useState("");
  const [result,setResult]=useState()
  const submit = async (e) => {
    e.preventDefault()
    const params = decodeURIComponent(new URLSearchParams({
      credentialUsername,
    }));
    const response = await fetch(
      `http://pc-health.somee.com/Admin/ForgetPasswordUsername?${params}`,
      {method:"POST",
      // params: {"credentialUsername": credentialUsername}
      }
    )
    const ans = await response.json()
    setResult(ans)
  };
  // const submit = () => {
  //   // localStorage.setItem("Email",credentialUsername)
  //   axios.post(
  //     `http://omarkar1011-001-site1.dtempurl.com/Admin/ForgetPasswordUsername?credentialUsername=${credentialUsername}`
  //   ).then(res=>console.log(res))
  // }
  if (result) {
    localStorage.setItem("Email", credentialUsername);
    return <Redirect to="/forgotpassword/Code"/>
  }
  return (
    <div className="div_design forgot_page">
      <form onSubmit={submit} className="form_container">
      <p className="forgot_pass">Enter your email:</p>
        <input className="design_input"
          type="email"
          placeholder="Email"
          onChange={(e) => setUsername(e.target.value)}
        />
        <button className="login_button" type="submit">Submit</button>
      </form>
      <p> <Link to="/Login" className="links">
              cancel
          </Link> </p>
    </div>
  );
}