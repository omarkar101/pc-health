import React from "react";
import { Link } from "react-router-dom";
import Nav from "./Nav";




export default function HomeNav(prop: { setToken: (token) => void }) {

    const logout = () => {
        localStorage.removeItem("token")
        prop.setToken('')
    }
    let menu;
    if (
        localStorage.getItem("token") === "false" ||
        localStorage.getItem("token") === null ||
        !window.location.pathname === "/table"
    ) {
        return (
            <div style={{ color: "white" }}>
                login page
            </div>
        );

    } else {
        menu = (
            <ul className="navbar-nav me-auto mb-2 mb-nd-8">
                <li className="navbar-item active">
                    <Link to="/" className="nav-link" onClick={logout}>
                        Logout
          </Link>
                </li>
            </ul>
        );
    }
    return (
        <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
            <div className="container-fluid">
                {/* <div className="navbar-brand"> */}
                    {/* Home */}
                {/* </div> */}
                <div>
                    {menu}
                </div>
            </div>
        </nav>
    );
}


import React from "react";
import { Link } from "react-router-dom";




export default function Nav(prop:{setToken:(token)=>void}) {

  const logout = () => {
    localStorage.removeItem("token")
    prop.setToken('')
  }
  let menu;
  if (
    localStorage.getItem("token") === "false" ||
    localStorage.getItem("token") === null ||
    !window.location.pathname==="/table"
  ) {
    menu = (
      <ul className="navbar-nav me-auto mb-2 mb-nd-8">
        <li className="navbar-item active">
          <Link to="/" className="nav-link">
            Login
          </Link>
        </li>
        <li className="navbar-item active">
          <Link to="/register" className="nav-link">
            Register
          </Link>
        </li>
      </ul>
    );
  } else {
    menu = (
      <ul className="navbar-nav me-auto mb-2 mb-nd-8">
        <li className="navbar-item active">
          <Link to="/" className="nav-link" onClick={logout}>
            Logout
          </Link>
        </li>
      </ul>
    );
  }
  return (
    <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
      <div className="container-fluid">
        <div className="navbar-brand">
          {/* Home */}
        </div>
        <div>
          {menu}
        </div>
      </div>
    </nav>
  );
}