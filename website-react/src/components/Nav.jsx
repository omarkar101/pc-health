import React from "react";
import { Link } from "react-router-dom";
export default function Nav() {
  return (
    <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
      <div className="container-fluid">
        <Link to="/" className="navbar-brand">
          Home
        </Link>
        <div>
          <ul className="navbar-nav me-auto mb-2 mb-nd-8">
            <li className="navbar-item active">
              <Link to="/login" className="nav-link">
                Login
              </Link>
            </li>
            <li className="navbar-item active">
              <Link to="/register" className="nav-link">
                Register
              </Link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
}
