import { Link, Redirect, useHistory } from "react-router-dom";
import "./NavStyle.css";
import React, { useState, useEffect } from "react";
import Settings from "./Settings";
import {
  Menu,
  MenuList,
  MenuButton,
  MenuItem,
  MenuLink,
} from "@reach/menu-button";
import "@reach/menu-button/styles.css";
import Select from 'react-select'


export default function Nav() {
  const [buttonPopup, setButtonPopup] = useState(false);
  const [state, setState] = useState({
    interval: 3,
    input: -1,
  });
  const History = useHistory();
  const [unit, setUnit] = useState('');

  const options = [
    { value: 'Seconds', label: 'Seconds' },
    { value: 'Minutes', label: 'Minutes' },
    { value: 'Hours', label: 'Hours' }
  ]

  const logout = () => {
    localStorage.clear()
    History.push("./")
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
      if (unit === "Seconds") {
        state.interval = state.input * 1;
        setState(state);
        localStorage.setItem("interval", state.interval)
      }
      else if (unit === "Minutes") {
        state.interval = state.input * 60;
        setState(state);
        localStorage.setItem("interval", state.interval);
      }
      else if (unit === "Hours") {
        state.interval = state.input * 3600;
        setState(state);
        localStorage.setItem("interval", state.interval);
      }
      state.input = -1;
      console.log(state);
      setButtonPopup(false);
    }
  };


  const changepassword = () => {
    History.push("./ChangePass")
    // window.location.reload()
  }

  const resetpassword = () => {
    History.push("./ResetPass")
  }
  let menu;




  return (
    <>
      <Settings trigger={buttonPopup} setTrigger={setButtonPopup}>
        <form onSubmit={handleSubmit}>

          <p className="message"> Enter the time interval in which you want the information to be updated</p>

          {/* <div className="input_div"> */}

            <input className="input-interval" type="number" onChange={handleIntervalChange} placeholder="Duration" />

            <Select className="options" options={options} placeholder="Time unit" onChange={handleUnitChange} />

          {/* </div> */}
            <button className="save_button" type="submit">
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
            Reset Account Code
            </MenuLink>
          <MenuLink to="/" onClick={logout}>
            Logout
            </MenuLink>

        </MenuList>

      </Menu>
    </>
  );
}