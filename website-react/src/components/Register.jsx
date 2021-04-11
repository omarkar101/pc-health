import React, {useState} from 'react';
import { Redirect } from 'react-router';

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
          "http://omarkar1011-001-site1.dtempurl.com/Admin/Create",
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
  if (account===false){return (
      <div>
        <form onSubmit={(e) => submit(e)}>
          <h1>Please Register</h1>
          <input
            type="text"
            placeholder="FirstName"
            required
            onChange={(e) => setFName(e.target.value)}
          />
          <input
            type="text"
            placeholder="Last Name"
            required
            onChange={(e) => setLName(e.target.value)}
          ></input>
          <input
            type="text"
            placeholder="Username"
            required
            onChange={(e) => setUName(e.target.value)}
          />
          <input
            type="password"
            placeholder="Password"
            required
            onChange={(e) => setPassword(e.target.value)}
          ></input>
          <button className="w-100 btn btn-lg btn-primary" type="submit">
            Register
          </button>
        </form>
        <p>The username already exists</p>
      </div>
    );}
        return (
          <form onSubmit={(e) => submit(e)}>
            <h1>Please Register</h1>
            <input
              type="text"
              placeholder="FirstName"
              required
              onChange={(e) => setFName(e.target.value)}
            />
            <input
              type="text"
              placeholder="Last Name"
              required
              onChange={(e) => setLName(e.target.value)}
            ></input>
            <input
              type="text"
              placeholder="Username"
              required
              onChange={(e) => setUName(e.target.value)}
            />
            {/* <input type="email" platextceholder="Email Address" required onChange={e=>setEmail(e.target.value)  }/> */}
            <input
              type="password"
              placeholder="Password"
              required
              onChange={(e) => setPassword(e.target.value)}
            ></input>
            <button className="w-100 btn btn-lg btn-primary" type="submit">
              Register
            </button>
          </form>
        );
}
