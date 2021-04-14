// import Redirect from "react";
import { Component } from 'react'
import { Link , Redirect, useHistory} from "react-router-dom";
import "./NavStyle.css";
import React, { useState, useEffect } from "react";
import Settings from "./Settings";
import Table from "./Table";
import './settings.css'
import ProtectedRoute from "./ProtectedRoute";

// import ChangePass from "./ChangePass";
// import './style.css'
// import {
//   Menu,
//   MenuItem,
//   MenuButton,
//   SubMenu,
//   MenuDivider,
//   MenuHeader
// } from '@szhsin/react-menu';
// import '@szhsin/react-menu/dist/index.css';
import {
  Menu,
  MenuList,
  MenuButton,
  MenuItem,
  MenuItems,
  MenuPopover,
  MenuLink,
} from "@reach/menu-button";
import "@reach/menu-button/styles.css";
import Select from 'react-select'


export default function Nav(prop: { setToken: (token) => void }) {
  const [buttonPopup, setButtonPopup] = useState(false);
  const [state, setState] = useState({
    interval: 3,
    input: -1,
  });
  const History = useHistory();
  const [unit, setUnit] = useState('');

  const options = [
    { value: 'seconds', label: 'seconds' },
    { value: 'minutes', label: 'minutes' },
    { value: 'hours', label: 'hours' }
  ]

  const handleIntervalChange = (event) => {
    // console.log(event.target.value);
    state.input = event.target.value;
    setState(state);
    // console.log(state);
    // setState({ interval: event.target.value })
  };

  const handleUnitChange = (event) => {
    // state.interval = event.target.value;
    // setState(state);
    // console.log(event.value);
    setUnit(event.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(state);
    if (state.input === -1) {
      alert("please provide a time!");
    }
    else if (unit === '') {
      alert("please provide a unit of time!");
    }
    else if (state.input < 0) {
      alert("time interval cannot be negative!");
    }
    else if (state.input < 3 && unit === "seconds") {
      alert("time interval has to be 3 seconds or longer!");
    }
    else {
      if (unit === "seconds") {
        state.interval = state.input * 1;
        setState(state);
      }
      else if (unit === "minutes") {
        state.interval = state.input * 60;
        setState(state);
      }
      else if (unit === "hours") {
        state.interval = state.input * 3600;
        setState(state);
      }
      state.input = -1;
      console.log(state);
      setButtonPopup(false);
    }
  };
  // const Toggle = () => {
  //   const [show, toggleShow] = React.useState(true);
  //   return (
  //     <div>
  //       <button
  //         onClick={() => toggleShow(!show)}
  //       >
  //         toggle: {show ? 'show' : 'hide'}
  //       </button>    
  //       {show && <div>Hi there</div>}
  //     </div>
  //   )
  // }

  const logout = () => {
    localStorage.removeItem("token")
    prop.setToken('')
  }

  const changepassword = () => {
    History.push("./ChangePass")
  }
  let menu;
  // if (window.location.pathname.find("/Services")){
  //   return (null);
  // }
  // else
  if (
    localStorage.getItem("token") === "false" ||
    localStorage.getItem("token") === null ||
    !window.location.pathname === "/table"
  ) {
    return (
      <nav>
        <div className="navigations_login">
          {/* <ul> */}
          {/* <div className="plinks">Already have an account?
            <Link to="/" className="links">
              Login
            </Link>
            </div> */}
            <Link to="/ForgetPassword" className="links">
            Forgot Password?
            </Link>
            <br/>
          <div className="plinks">Don't have an account?
            <Link to="/register" className="links">
              Register
            </Link>
        
          </div>
          {/* </ul> */}
        </div>
      </nav>
    );
  }
  else {
    return (
      <>
        <Settings trigger={buttonPopup} setTrigger={setButtonPopup}>
          <form onSubmit={handleSubmit}>
            {/* <label> Time Interval
            </label> */}
            {/* <div className="input_div"> */}

            <input className="input-interval" type="number" onChange={handleIntervalChange} placeholder="set time interval (in seconds)" />

            <Select className="options" options={options} onChange={handleUnitChange} />
            {/* </div> */}
            {/* <div className="submit_div"> */}
            <br /><br />
            <button className="submit_btn" type="submit">
              Save Changes
            </button>
            {/* </div> */}
          </form>
        </Settings>


        {/* <nav className="nav"> */}
        {/* <div className="nav_div"> */}
        {/* <button className="SettingsButton" onClick={() => setButtonPopup(true)}> Settings </button> */}
        {/* <Menu menuButton={<MenuButton className="menu_nav">Settings</MenuButton>}>
            <MenuItem onClick={() => setButtonPopup(true)}>Chage Interval</MenuItem>
            <MenuItem>Change Password</MenuItem>
          </Menu> */}
        <Menu>
          <MenuButton className="menu_nav">
            Account Settings <span aria-hidden>▾</span>
          </MenuButton>
          <MenuList>
            <MenuItem onClick={() => setButtonPopup(true)}>Change Interval</MenuItem>
            <MenuLink onClick={changepassword}>
              Change Password
            </MenuLink>
            <MenuLink to="/" onClick={logout}>
              Logout
            </MenuLink>

          </MenuList>

        </Menu>

        {/* <Link to="/" className="logout" onClick={logout}>
            Logout
              </Link> */}
        {/* </div> */}
        {/* </nav> */}
        <ProtectedRoute exact path="/table" component={Table}>
          <Table i={state}></Table>
        </ProtectedRoute>
      </>
    );
  }
}
