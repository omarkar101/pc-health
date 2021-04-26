import { Link } from 'react-router-dom'
import React from 'react'
import './HomeStyles.css'


export default function AboutUs() {
  return (
    <div className="aboutus_contain">
      <nav className="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <div className="container-fluid">
          <Link className="navbar-brand"
          >
            PSee
            </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarCollapse"
            aria-controls="navbarCollapse"
            aria-expanded="true"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarCollapse">
            <form className="d-flex">
              <ul className="navbar-nav me-auto mb-2 mb-md-0">
                <li className="nav-item">
                  <Link className="nav-link" to="/">
                    Home
                    </Link>
                </li>
                <li className="nav-item"></li>
              </ul>
            </form>
          </div>
        </div>
      </nav>


      <div className="pics_container">
        <h1>The Team</h1>
        <br />
        <br />
        <div className="pic">
          <img className="aboutuspic" src="/images/mahdi.jpg" alt="" />

          <h3 style={{ color: "black" }}>
            <br />
              Mahdi Matout
            </h3>
        </div>
        <div className="pic">
          <img className="aboutuspic" src="/images/julia.jpeg" alt="" />
          <h3 style={{ color: "black" }}>
            <br />
              Julia Kammouni
            </h3>
        </div>
        <div className="pic">
          <img className="aboutuspic" src="/images/roni.jpg" alt="" />

          <h3 style={{ color: "black" }}>
            <br />
              Rony Allaw
            </h3>
        </div>
        <div className="pic">
          <img className="aboutuspic" src="/images/omar.png" alt="" />
          <h3 style={{ color: "black" }}>
            <br /> Omar Karazoun
            </h3>
        </div>
      </div>
      <hr />
      <div className="par">
        <p><i>Our Mission:</i>
          <br />
        PSee is a project that we started working on as a part of one of our university courses
        <br />
            As we worked more on the project, we realized that collecting diagnostic data can be hard even for experienced developers.
            So we started further developing our idea to make it more accessible and beneficial to users,
            and we are constantly working on improving our ideas and strategy for users to have a better experience.
        <br />
          <br />
        If you have any inquires, contact us at team.mirai101@gmail.com
        </p>
      </div>
    </div>
  );
}