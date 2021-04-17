import { Link , Redirect, useHistory} from "react-router-dom";
import "./NavStyle.css";
import React, { useState, useEffect } from "react";
import Settings from "./Settings";
import Table from "./Table";
import './settings.css'
import ProtectedRoute from "./ProtectedRoute";
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

  const logout = () => {
    localStorage.removeItem("token")
    prop.setToken('')
  }
  
  useEffect(() => {
    const handleInvalidToken = (e) => {
      if (e.key === "token" && e.oldValue && !e.newValue) {
        logout();
      }
    };

    window.addEventListener("storage", handleInvalidToken);

    return function cleanup() {
      window.removeEventListener("storage", handleInvalidToken);
    };
  }, [logout]);

  const handleIntervalChange = (event) => {
    state.input = event.target.value;
    setState(state);
  };

  const handleUnitChange = (event) => {

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


  const changepassword = () => {
    History.push("./ChangePass")
  }

  const resetpassword = () => {
    History.push("./ResetPass")
  }
  let menu;
  if (
    localStorage.getItem("token") === "false" ||
    localStorage.getItem("token") === null ||
    !window.location.pathname === "/table"
  ) {
    return (
      <nav>
        <div className="navigations_login">
            <div className="plinks">Forgot Password?
            <Link to="/ForgetPassword" className="links">
              Click here
            </Link>
            </div>
            <br/>
          <div className="plinks">Don't have an account?
            <Link to="/Register" className="links">
              Register
            </Link>
        
          </div>
          <br/>
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

            <input className="input-interval" type="number" onChange={handleIntervalChange} placeholder="set time interval (in seconds)" />

            <Select className="options" options={options} onChange={handleUnitChange} />
            <br /><br />
            <button className="submit_btn" type="submit">
              Save Changes
            </button>
          </form>
        </Settings>


        <Menu>
          <MenuButton className="menu_nav">
            Account Settings <span aria-hidden>▾</span>
          </MenuButton>
          <MenuList>
            <MenuItem onClick={() => setButtonPopup(true)}>Change Interval</MenuItem>
            <MenuLink onClick={changepassword}>
              Change Password
            </MenuLink>
            <MenuLink onClick={resetpassword}>
              Reset Password
            </MenuLink>
            <MenuLink to="/" onClick={logout}>
              Logout
            </MenuLink>

          </MenuList>

        </Menu>

        <ProtectedRoute exact path="/table" component={Table}>
          <Table i={state}></Table>
        </ProtectedRoute>
      </>
    );
  }
}
