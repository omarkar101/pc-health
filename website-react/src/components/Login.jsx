import React, { useState,SyntheticEvent, useEffect} from "react";
import "./Login.css";
import { Redirect } from "react-router";
function Login() {
    const [CredentialsUsername, setUname] = useState("");
    const [CredentialsPassword, setPassword] = useState("");
    const [redirect, setRedirect] = useState("");
    // function validateForm() {
    //     return CredentialsUsername.length > 0 && CredentialsPassword.length > 0;
    // }
    const submit = async (e) => {
        e.preventDefault();
        const response = await fetch(
          "http://omarkar1011-001-site1.dtempurl.com/api/Post/PostLogin",
          {
            method: "POST",
            // authorization: "Bearer Token",
            headers: { "Content-Type": "application/json" },
            // credentials: "include",
            body: JSON.stringify({
              CredentialsUsername,
              CredentialsPassword,
            }),
          }
        );
        const token = await response.text();
        localStorage.setItem("token", token)
        console.log(CredentialsUsername)
        console.log(CredentialsPassword);
        console.log(token);
        setRedirect(token);
        // console.log(token)
        // const ans = await response
        //  setRedirect(ans);
        
    }
    // useEffect(() => {
    //     submit();
    // });
    //if (redirect != "false") { return <Redirect to="/" /> }
    return (
        <form onSubmit={submit}>
            <h1 className="h3 mb-3 fw-normal">Please Login</h1>
            <input type="text" className="form-control" placeholder="Username" required
            onChange={(e)=>setUname(e.target.value)}></input>
            <input type="password" className="form-control" placeholder="Password" required onChange={(e)=>setPassword(e.target.value)}/>
            <button className="w-100 btn btn-lg btn-primary" type = "submit">Log in</button>
        </form>)
}
export default Login