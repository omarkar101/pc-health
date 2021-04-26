import React from "react";
import { Link } from "react-router-dom";
import "./HomeStyles.css";



function Home() {
  return (
    <div className="body_s">
      <nav className="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
        <div className="container-fluid">
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
            <ul className="navbar-nav me-auto mb-2 mb-md-0">
              <li className="nav-item">
                <Link className="nav-link" to="/aboutus">
                  About Us
                </Link>
              </li>
            </ul>
            <form className="d-flex">
              <ul className="navbar-nav me-auto mb-2 mb-md-0">
                <li className="nav-item">
                  <Link className="nav-link" to="/Login">
                    Login
                  </Link>
                </li>
                <li className="nav-item">
                </li>
              </ul>
            </form>
          </div>
        </div>
      </nav>

      <div id="myCarousel" className="carousel slide" data-bs-ride="carousel">
        <div className="carousel-inner">
          <div className="carousel-item active">
            <div className="test">
              <img className="image_display" src="/images/logo_cropped.png" />
            </div>
            <div className="container">
              <div className="carousel-caption text-start">
                <h1 style={{ color: "black" }}>
                  Welcome to P-See
                </h1>
                <p style={{ color: "black" }}>
                  The Ultimate website to easily monitor your owned PCs' healths
                </p>
                <p>
                  <Link className="home_button" to="/register">
                    Sign up today
                  </Link>
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>

      <hr className="featurette-divider1" />
      <div className="container marketing">
        <div className="row">
          <div className="col-lg-4">
            <svg
              className="bd-placeholder-img rounded-circle"
              width="240"
              height="240"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 140x140"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <image
                height="240"
                href="/images/img1.png"
              />
            </svg>

            <h2 style={{ color: "black" }}>
              <br />
              Monitor an unlimited number of PCs all in one place
            </h2>
          </div>
          <div className="col-lg-4">
            <svg
              className="bd-placeholder-img rounded-circle"
              width="240"
              height="240"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 140x140"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <image
                height="240"
                href="/images/img22.png"
              />
            </svg>
            <h2 style={{ color: "black" }}>
              <br />
              Get notified immediately about any PC in danger
            </h2>
          </div>
          <div className="col-lg-4">
            <svg
              className="bd-placeholder-img rounded-circle"
              width="240"
              height="240"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 140x140"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <image
                height="240"
                href="/images/charts2.jpg"
              />
            </svg>

            <h2 style={{ color: "black" }}>
              <br /> View real-time detailed information
            </h2>
          </div>
        </div>

        <hr className="featurette-divider" />
        <h2>Steps to install and configure the application:</h2>
        <br />
        <div className="row featurette">
          <div className="col-md-7">
            <h2 className="featurette-heading">
              Step 1: Installation Process <br />
            </h2>
            <p className="lead">
              First, sign up on our website using your email. Next, install the application using the setup that can be found
              <Link>Here</Link>
            </p>
            <p>
              <i>Note:</i> We <b>recommend</b> installing the application in any
              directory outside <b>Program Files</b>. Installing the application
              in "Program Files" will require you to configure the configuration
              file of the application manually.
            </p>
            <p>
              Next, extract the zip folder, and you will now be ready to proceed to Step 2 - Configuartion.
            </p>
          </div>
          <div className="col-md-5">
            <svg
              className="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto"
              width="500"
              height="500"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 500x500"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <title>Placeholder</title>
              <rect width="100%" height="100%" fill="#eee" />
              <text x="50%" y="50%" fill="#aaa" dy=".3em">
                500x500
              </text>
            </svg>
          </div>
        </div>

        <hr className="featurette-divider" />

        <div className="row featurette">
          <div className="col-md-7 order-md-2">
            <h2 className="featurette-heading">Step 2: Configuring the PC</h2>
            <p className="lead">
              After Installing the application, open the folder, and click on
              PC-Configuration.jar and insert the PC's name and contact info and
              assign an admin email (email used in Step 1) to it. After that, click Apply. After
              configuring all the PCs, run "PC App.exe"
            </p>
            <p>
              <i>Note:</i> to make the app run automatically at startup, please follow the intructions in the following  <Link>
                link
            </Link>
            </p>
          </div>
          <div className="col-md-5 order-md-1">
            <svg
              className="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto"
              width="500"
              height="500"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 500x500"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <title>Placeholder</title>
              <rect width="100%" height="100%" fill="#eee" />
              <text x="50%" y="50%" fill="#aaa" dy=".3em">
                500x500
              </text>
            </svg>
          </div>
        </div>

        <hr className="featurette-divider" />

        <div className="row featurette">
          <div className="col-md-7">
            <h2 className="featurette-heading">Step 3:</h2>
            <p className="lead"> After finishing configuring the PCs, log in to the website, and you will be able to see real-time statistics of all configured PCs.</p>
          </div>
          <div className="col-md-5">
            <svg
              className="bd-placeholder-img bd-placeholder-img-lg featurette-image img-fluid mx-auto"
              width="500"
              height="500"
              xmlns="http://www.w3.org/2000/svg"
              role="img"
              aria-label="Placeholder: 500x500"
              preserveAspectRatio="xMidYMid slice"
              focusable="false"
            >
              <title>Placeholder</title>
              <rect width="100%" height="100%" fill="#eee" />
              <text x="50%" y="50%" fill="#aaa" dy=".3em">
                500x500
              </text>
            </svg>
          </div>
        </div>

        <hr className="featurette-divider" />
      </div>

      <footer className="container">
        <p className="float-end">
          <a href="#">Back to top</a>
        </p>
        <p>
          &copy; 2021 CMPS253, Inc.
        </p>
      </footer>
    </div>
  );
}

export default Home;